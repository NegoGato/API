using MongoDB.Bson;
using System;

namespace WebApiContta.Domain.Aggregate.XmlNfeAgg
{
    public class XmlNfe
    {
        public ObjectId Id { get; set; }
        public string Chave { get; set; }
        public string Cte { get; set; }
        public string Xml { get; set; }
        public DateTime DtEmissao { get; set; }
        public string CnpjEmitente { get; set; }
        public string CnpjDest { get; set; }
        public string CnpjRemetente { get; set; }
        public int MesEmissao { get; set; }
        public int AnoEmissao { get; set; }
        public bool JaFoiEnviadoParaOhConttaAntigo { get; set; }
        public string Tipo { get; set; }
        public bool VirouDocumento { get; set; }
        

    }
}