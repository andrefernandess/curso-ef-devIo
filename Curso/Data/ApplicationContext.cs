using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curso_console.Data.Configurations;
using curso_console.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace curso_console.Data
{
    public class ApplicationContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p=>p.AddConsole());
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Data source=localhost; Initial Catalog=CursoEfCore;User Id=sa;Password=Karate@12",
                    p=> p.EnableRetryOnFailure(
                    maxRetryCount: 2, 
                    maxRetryDelay: TimeSpan.FromSeconds(5), 
                    errorNumbersToAdd: null).MigrationsHistoryTable("curso_ef_core"));
        } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //da forma abaixo, o builder busca todos os arquivos que  implementam a classe applicationcontext e executa
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            MapearPropriedadesEsquecidas(modelBuilder);

            // na forma abaixo, e necessario setar cada arquivo de configuração
            //modelBuilder.ApplyConfiguration(new ClienteConfiguration());


            //Abaixo, forma mais simples , nao recomendado para aplicações grandes
            // modelBuilder.Entity<Cliente>(c => 
            // {
            //     c.ToTable("Clientes");
            //     c.HasKey(c => c.Id);
            //     c.Property(c=>c.Nome).HasColumnType("VARCHAR(80)").IsRequired();
            //     c.Property(c=>c.Telefone).HasColumnType("VARCHAR(11)").IsRequired();
            //     c.Property(c=>c.CEP).HasColumnType("VARCHAR(8)").IsRequired();
            //     c.Property(c=>c.Estado).HasColumnType("VARCHAR(80)").IsRequired();
            //     c.Property(c=>c.Cidade).HasColumnType("VARCHAR(80)").IsRequired();

            //     c.HasIndex(c=>c.Telefone).HasName("idx_cliente_telefone");
            // });

            // modelBuilder.Entity<Produto>(p =>
            // {
            //     p.ToTable("Produtos");
            //     p.HasKey(p => p.Id);
            //     p.Property(p=>p.CodigoBarras).HasColumnType("VARCHAR(80)").IsRequired();
            //     p.Property(p=>p.Descricao).HasColumnType("VARCHAR(80)").IsRequired();
            //     p.Property(p=>p.Valor).IsRequired();
            //     p.Property(p=>p.TipoProduto).HasConversion<string>();
            // });

            // modelBuilder.Entity<Pedido>(p => 
            // {
            //     p.ToTable("Pedidos");
            //     p.HasKey(p => p.Id);
            //     p.Property(p => p.IniciadoEm).HasDefaultValue("GETDATE()").ValueGeneratedOnAdd();
            //     p.Property(p => p.Status).HasConversion<string>();
            //     p.Property(p => p.TipoFrete).HasConversion<int>();
            //     p.Property(p => p.Observacao).HasColumnType("VARCHAR(512)");

            //     p.HasMany(p => p.Itens)
            //         .WithOne(pi => pi.Pedido)
            //         .OnDelete(DeleteBehavior.Cascade);
            // });

            // modelBuilder.Entity<PedidoItem>(pi =>
            // {
            //     pi.ToTable("PedidoItens");
            //     pi.HasKey(pi => pi.Id);
            //     pi.Property(pi => pi.Quantidade).HasDefaultValue(1).IsRequired();
            //     pi.Property(pi => pi.Valor).IsRequired();
            //     pi.Property(pi => pi.Desconto).IsRequired();
            // });
        }

        private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where(p=>p.ClrType == typeof(string));

                foreach(var property in properties)
                {
                    if(string.IsNullOrEmpty(property.GetColumnType())
                        && !property.GetMaxLength().HasValue)
                        {
                            //property.SetMaxLength(100);
                            property.SetColumnType("VARCHAR(100)");
                        }
                }
            }
        }
    }
}