using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arquitetura.Web.Helpers
{
    public class Proc
    {
        public ObjectId Id { get; set; }
        public string ChaveNfe { get; set; }
        public string IdEvento { get; set; }
        public string Xml { get; set; }

    }
}
