using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiContta.Domain.Aggregate.ClienteAgg;

namespace WebApiContta.Data.Services.Interface
{
    interface IRepository:IDisposable
    {

        List<Cliente> Get();

        List<Cliente> Get(string cnpj);

    }
}
