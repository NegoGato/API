using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web.Caching;
using System.Web.Http;
using WebApiContta.Atribute;
using WebApiContta.Data;
using WebApiContta.Data.Context;
using WebApiContta.Domain.Aggregate.ClienteAgg;
using WebApiContta.Domain.Aggregate.UsuarioAgg;

namespace WebApiContta.Controllers
{
    public class ClientesController : ApiController
    {    
        [DeflateCompression]
        [ActionName("ListarClientes")]
        [HttpPost]
        public IEnumerable<Cliente> Get([FromBody]Usuario usuario)
        {
            try
            {
                var autenticaUsuario = new UsuarioRepository();
                if (autenticaUsuario.Autentica(usuario))
                {
                    var listaCliente = new ClienteRepository();
                    //var lista = listaCliente.Get().ToList();
                    return listaCliente.Get();
                }
                else
                    throw new Exception("Usuário não encontrado");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [DeflateCompression]
        [ActionName("ListarClientesPorCnpj")]
        [HttpPost]
        public IEnumerable<Cliente> Get([FromUri]string cnpj, [FromBody] Usuario usuario)
        {
            try
            {
                var autenticaUsuario = new UsuarioRepository();
                if (autenticaUsuario.Autentica(usuario))
                {
                    var listaCliente = new ClienteRepository();
                    return listaCliente.Get(cnpj);
                }
                else
                    throw new Exception("Usuário não encontrado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
