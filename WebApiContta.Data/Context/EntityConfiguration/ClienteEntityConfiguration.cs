using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiContta.Domain.Aggregate.ClienteAgg;

namespace WebApiContta.Data.Context.EntityConfiguration
{
     public class ClienteEntityConfiguration:EntityTypeConfiguration<Cliente>
     {

         public ClienteEntityConfiguration()
         {
             this.HasKey(c => c.Id);

             this.Property(c => c.Id)
                 .HasColumnName("ID_CLIT")
                 .IsRequired();


             this.Property(c => c.Cnpj)
                 .HasColumnName("CNPJ_CLIT")
                 .IsRequired();

             this.Property(c => c.InscricaoEstadual)
                 .HasColumnName("IE_CLIT")
                 .IsRequired();

             this.Property(c => c.NomeFantasia)
                 .HasColumnName("NOMEFANTASIA_CLIT")
                 .IsRequired();

             this.Property(c => c.RazaoSocial)
                 .HasColumnName("RAZAOSOCIAL_CLIT")
                 .IsRequired();

             this.Property(c => c.AtividadeEconomica)
                 .HasColumnName("ATIVIDADEECONOMICA_CLIT")
                 .IsRequired();

             this.Property(c => c.RegimeDeApuracao)
                 .HasColumnName("REGIMEDEAPURACAO_CLIT")
                 .IsRequired();

             this.Property(c => c.SituacaoCadastralVigente)
                 .HasColumnName("SITUACAOCADASTRALVIGENTE_CLIT")
                 .IsRequired();

             this.Property(c => c.DataDestaSituacaoCadastral)
                 .HasColumnName("DATADESTASITUACAOCADASTRAL_CLIT")
                 .IsRequired();

             this.Property(c => c.DataDeCadastramento)
                 .HasColumnName("DATADECADASTRAMENTO_CLIT")
                 .IsRequired();

             this.Property(c => c.Telefone)
                 .HasColumnName("TELEFONE_CLIT")
                 .IsRequired();

             this.Property(c => c.Logradouro)
                 .HasColumnName("LOGRADOURO_CLIT")
                 .IsRequired();

             this.Property(c => c.Numero)
                 .HasColumnName("NUMERO_CLIT")
                 .IsRequired();

             this.Property(c => c.Complemento)
                 .HasColumnName("COMPLEMENTO_CLIT")
                 .IsRequired();

             this.Property(c => c.Bairro)
                 .HasColumnName("BAIRRO_CLIT")
                 .IsRequired();

             this.Property(c => c.Municipio)
                 .HasColumnName("MUNICIPIO_CLIT")
                 .IsRequired();

             this.Property(c => c.Uf)
                 .HasColumnName("UF_CLIT")
                 .IsRequired();

             this.Property(c => c.Cep)
                 .HasColumnName("CEP_CLIT")
                 .IsRequired();

             this.Property(c => c.Contribuinte)
                 .HasColumnName("CONTRIBUINTE_CLIT")
                 .IsRequired();

             this.Property(c => c.UltimaAtualizacao)
                 .HasColumnName("ULTIMAATUALIZACAO_CLIT")
                 .IsRequired();


             this.Property(c => c.Ativo)
                 .HasColumnName("ATIVO_CLIT")
                 .IsRequired();


             this.ToTable("baseclientesintegra");


         }


     }
}
