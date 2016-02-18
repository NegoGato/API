using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using WebApiContta.Domain.Aggregate.ReceitaAgg;

namespace WebApiContta.Domain.Aggregate.ClienteAgg
{
    public class Cliente
    {
      
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string AtividadeEconomica { get; set; }
        public string RegimeDeApuracao { get; set; }
        public string SituacaoCadastralVigente { get; set; }
        public string DataDestaSituacaoCadastral { get; set; }
        public string DataDeCadastramento { get; set; }
        public string Telefone { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string UltimaAtualizacao { get; set; }
        public bool Ativo { get; set; }
        public bool Contribuinte { get; set; }
       
        

    }
}
