using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curso_console.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace curso_console.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
                builder.ToTable("Produtos");
                builder.HasKey(p => p.Id);
                builder.Property(p=>p.CodigoBarras).HasColumnType("VARCHAR(80)").IsRequired();
                builder.Property(p=>p.Descricao).HasColumnType("VARCHAR(80)").IsRequired();
                builder.Property(p=>p.Valor).IsRequired();
                builder.Property(p=>p.TipoProduto).HasConversion<string>();
        }
    }
}