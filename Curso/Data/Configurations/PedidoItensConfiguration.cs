using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curso_console.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace curso_console.Data.Configurations
{
    public class PedidoItensConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
                builder.ToTable("PedidoItens");
                builder.HasKey(pi => pi.Id);
                builder.Property(pi => pi.Quantidade).HasDefaultValue(1).IsRequired();
                builder.Property(pi => pi.Valor).IsRequired();
                builder.Property(pi => pi.Desconto).IsRequired();
        }
    }
}