using ANSYS.Domain.Global.PedidoItens.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ANSYS.Infrastructure.Global.PedidoItens.Configuration
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidoItens");

            //Relacionamentos...
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Pedido)
                   .WithMany(u => u.Itens)
                   .HasForeignKey(p => p.PedidoId);

            //Propriedades...
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Quantidade)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(e => e.Subtotal)
                .IsRequired();

            builder.Property(e => e.Taxa)
                .IsRequired();

            builder.Property(e => e.Desconto)
                .IsRequired();
        }
    }
}
