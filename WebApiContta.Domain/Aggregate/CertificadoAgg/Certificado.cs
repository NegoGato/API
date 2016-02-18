using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiContta.Domain.Aggregate.CertificadoAgg
{
    public class Certificado
    {
        public ObjectId Id { get; set; }
        public string Cnpj { get; set; }
        public string DataValidade { get; set; }

    }
}
