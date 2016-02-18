using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web.Http;
using WebApi.OutputCache.V2;
using WebApi.OutputCache.V2.TimeAttributes;
using WebApiContta.Atribute;
using WebApiContta.Data;
using WebApiContta.Domain.Aggregate.ReceitaAgg;
using WebApiContta.Domain.Aggregate.UsuarioAgg;

namespace WebApiContta.Controllers
{
    public class ReceitaController : ApiController
    {
        
        [DeflateCompression]
        [ActionName("ListarReceita")]
        [HttpPost]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IEnumerable<Receita> Get([FromBody]Usuario usuario)
        {
            try
            {
                var autenticaUsuario = new UsuarioRepository();
                if (autenticaUsuario.Autentica(usuario))
                {
                    var listaReceita = new ReceitaRepository();
                    return listaReceita.Get();
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
        [ActionName("ListarReceitaPorCnpj")]
        [HttpPost]
        public IEnumerable<Receita> Get([FromUri]string cnpj, [FromBody] Usuario usuario)
        {
            try
            {
                var autenticaUsuario = new UsuarioRepository();
                if (autenticaUsuario.Autentica(usuario))
                {
                    var listaReceita = new ReceitaRepository();
                    return listaReceita.Get(cnpj);
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
