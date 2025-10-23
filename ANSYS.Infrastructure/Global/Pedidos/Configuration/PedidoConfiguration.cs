using ANSYS.Domain.Global.Pedidos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ANSYS.Infrastructure.Global.Pedidos.Configuration
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");

            //Relacionamentos...
            builder.HasKey(e => e.Id);

            builder.HasOne(p => p.Cliente)
                   .WithMany(u => u.Pedidos)
                   .HasForeignKey(p => p.ClienteId);

            //Propriedades...
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Subtotal)
                .IsRequired();

            builder.Property(e => e.Taxa)
                .IsRequired();

            builder.Property(e => e.Desconto)
                .IsRequired();

            builder.Property(e => e.Total)
                .IsRequired();

        }
    }
}
