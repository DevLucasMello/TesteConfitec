using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TC.Usuarios.Domain;

namespace TC.Usuarios.Infra.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.OwnsOne(c => c.Nome, n =>
            {
                n.Property(c => c.PrimeiroNome)
                .IsRequired()
                .HasColumnName("PrimeiroNome")
                .HasColumnType("varchar(50)");

                n.Property(c => c.UltimoNome)
                .IsRequired()
                .HasColumnName("UltimoNome")
                .HasColumnType("varchar(50)");
            });

            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("varchar(100)");

            builder.Property(c => c.DataNascimento)
                .IsRequired()
                .HasColumnName("DataNascimento")
                .HasColumnType("datetime");

            builder.Property(c => c.Escolaridade)
                .IsRequired()
                .HasColumnName("Escolaridade")
                .HasColumnType("int");

            builder.ToTable("Usuario");
        }
    }
}