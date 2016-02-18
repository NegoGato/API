using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiContta.Domain.Aggregate.UsuarioAgg;

namespace WebApiContta.Data.Context.EntityConfiguration
{
    public class UsuarioEntityConfiguration:EntityTypeConfiguration<Usuario>
    {

        public UsuarioEntityConfiguration()
        {
            this.HasKey(c => c.Id);

            this.Property(c => c.Id)
                .HasColumnName("ID_LOGI")
                .IsRequired();

            this.Property(c => c.Login)
                .HasColumnName("USUARIO_LOGI")
                .IsRequired();

            this.Property(c => c.Senha)
                .HasColumnName("SENHA_LOGI")
                .IsRequired();

            this.Property(c => c.Ativo)
                .HasColumnName("ATIVO_LOGI")
                .IsRequired();

            base.ToTable("loginapi");
        }

    }
}
