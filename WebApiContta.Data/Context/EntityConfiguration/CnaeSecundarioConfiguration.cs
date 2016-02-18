using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebApiContta.Domain.Aggregate.ReceitaAgg;

namespace WebApiContta.Data.Context.EntityConfiguration
{
    public class CnaeSecundarioConfiguration:EntityTypeConfiguration<CnaeSecundario>
    {

        public CnaeSecundarioConfiguration()
        {
            this.HasKey(c => c.Id);

            this.Property(c => c.Id)
                .HasColumnName("ID_CNAE")
                .IsRequired();

            this.Property(c => c.RecitaId)
               .HasColumnName("ID_RECE")
               .IsRequired();

            this.Property(c => c.Codigo)
              .HasColumnName("CODIGO_CNAE")
              .IsRequired();

            this.Property(c => c.Descricao)
               .HasColumnName("DESCRICAO_CNAE")
               .IsRequired();

            this.HasRequired(c => c.Receita)
                .WithMany(c => c.ListaDeCnaes)
                .HasForeignKey(c=>c.RecitaId)
                .WillCascadeOnDelete(false);
            
            base.ToTable("cnaesecundario");
        }

    }
}
