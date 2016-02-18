using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebApiContta.Domain.Aggregate.UsuarioAgg;

namespace WebApiContta.Data.Services.Interface
{
    public interface IRepositoryUsuario:IDisposable
    {

        bool Autentica(Usuario obj);

    }
}
