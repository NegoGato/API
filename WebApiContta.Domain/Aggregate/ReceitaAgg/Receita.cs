using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace WebApiContta.Domain.Aggregate.ReceitaAgg
{
    public class Receita
    {

        public Receita()
        {
            ListaDeCnaes = new List<CnaeSecundario>();  
        }
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string NumeroInscricao { get; set; }
        public string DataDeAbertura { get; set; }
        public string NomeEmpresarial { get; set; }
        public string TituloDoEstabelecimento { get; set; }
        public string Cnae { get; set; }
        public string CodigoDescricaoNaturezaJuridica { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string SituacaoCadastral { get; set; }
        public string DataDaSituacaoCadastral { get; set; }
        public string MotivoDeSituacaoCadastral { get; set; }
        public string SituacaoEspecial { get; set; }
        public string DataSituacaoEspecial { get; set; }
        public string UltimaAtualizacao { get; set; }
        public bool Ativo { get; set; }
        public virtual ICollection<CnaeSecundario> ListaDeCnaes { get; private set; }
    }
}
