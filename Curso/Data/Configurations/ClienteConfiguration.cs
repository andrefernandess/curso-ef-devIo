using curso_console.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace curso_console.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(c => c.Id);
            builder.Property(c=>c.Nome).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(c=>c.Telefone).HasColumnType("VARCHAR(11)").IsRequired();
            builder.Property(c=>c.CEP).HasColumnType("VARCHAR(8)").IsRequired();
            builder.Property(c=>c.Estado).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(c=>c.Cidade).HasColumnType("VARCHAR(80)").IsRequired();

            builder.HasIndex(c=>c.Telefone).HasName("idx_cliente_telefone");
        }
    }
}