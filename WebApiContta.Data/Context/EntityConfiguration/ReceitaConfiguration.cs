
using System.Data.Entity.ModelConfiguration;
using WebApiContta.Domain.Aggregate.ReceitaAgg;


namespace WebApiContta.Data.Context.EntityConfiguration
{
    public class ReceitaConfiguration:EntityTypeConfiguration<Receita>
    {

        public ReceitaConfiguration()
        {

            this.HasKey(c => c.Id);
            this.Property(c => c.Id)
                .HasColumnName("ID_RECE")
                .IsRequired();
            this.Property(c => c.Cnpj)
                .HasColumnName("CNPJ_RECE")
                .IsRequired();
            this.Property(c => c.NumeroInscricao)
                .HasColumnName("NUMEROINSCRICAO_RECE")
                .IsRequired();
            this.Property(c => c.DataDeAbertura)
                .HasColumnName("DATADEABERTURA_RECE")
                .IsRequired();
            this.Property(c => c.NomeEmpresarial)
                .HasColumnName("NOMEEMPRESARIAL_RECE")
                .IsRequired();
            this.Property(c => c.TituloDoEstabelecimento)
                .HasColumnName("TITULODOESTABELECIMENTO_RECE")
                .IsRequired();
            this.Property(c => c.Cnae)
                .HasColumnName("CNAE_RECE")
                .IsRequired();
            this.Property(c => c.CodigoDescricaoNaturezaJuridica)
                .HasColumnName("CODIGODESCRICAONATUREZAJURIDICA_RECE")
                .IsRequired();
            this.Property(c => c.Logradouro)
                .HasColumnName("LOGRADOURO_RECE")
                .IsRequired();
            this.Property(c => c.Numero)
                .HasColumnName("NUMERO_RECE")
                .IsRequired();
            this.Property(c => c.Complemento)
                .HasColumnName("COMPLEMENTO_RECE")
                .IsRequired();
            this.Property(c => c.Cep)
                .HasColumnName("CEP_RECE")
                .IsRequired();
            this.Property(c => c.Bairro)
                .HasColumnName("BAIRRO_RECE")
                .IsRequired();
            this.Property(c => c.Municipio)
                .HasColumnName("MUNICIPIO_RECE")
                .IsRequired();
            this.Property(c => c.Uf)
                .HasColumnName("UF_RECE")
                .IsRequired();
            this.Property(c => c.SituacaoCadastral)
                .HasColumnName("SITUACAOCADASTRAL_RECE")
                .IsRequired();
            this.Property(c => c.DataDaSituacaoCadastral)
               .HasColumnName("DATADASITUACAOCADASTRAL_RECE")
               .IsRequired();
            this.Property(c => c.MotivoDeSituacaoCadastral)
               .HasColumnName("MOTIVODESITUACAOCADASTRAL_RECE")
               .IsRequired();
            this.Property(c => c.SituacaoEspecial)
                .HasColumnName("SITUACAOESPECIAL_RECE");



            this.Property(c => c.DataSituacaoEspecial)
                .HasColumnName("DATADASITUACAOESPECIAL_RECE");
               
                 
            this.Property(c => c.UltimaAtualizacao)
                .HasColumnName("ULTIMAATUALIZACAO_RECE");
               
            this.Property(c => c.Ativo)
               .HasColumnName("ATIVO_RECE")
               .IsRequired();

            base.ToTable("cadastroreceita");


        }

    }
}
