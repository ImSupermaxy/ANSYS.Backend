using ANSYS.Application.Global.Usuarios.Commands;
using ANSYS.Domain.Global.Usuarios.Entities;
using ANSYS.Domain.Global.Usuarios.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ANSYS.Infrastructure.Global.Usuarios.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.Perfil)
                .IsRequired()
                .HasDefaultValue(EPerfilUsuario.Cliente);

            builder.HasData(
                new Usuario(1, "Master", "master@ansys.com", Domain.Global.Usuarios.Enums.EPerfilUsuario.Administrador),
                new Usuario(2, "Administrador", "admin@ansys.com", Domain.Global.Usuarios.Enums.EPerfilUsuario.Administrador)
            );
        }
    }
}
