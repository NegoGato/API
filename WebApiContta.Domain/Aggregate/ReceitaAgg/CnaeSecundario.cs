using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiContta.Domain.Aggregate.ReceitaAgg
{
    public class CnaeSecundario
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public virtual Receita Receita { get; set; }
        public int RecitaId { get; set; }

    }
}
