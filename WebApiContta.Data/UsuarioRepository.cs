using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApiContta.Data.Context;
using WebApiContta.Data.Services.Interface;
using WebApiContta.Domain.Aggregate.UsuarioAgg;

namespace WebApiContta.Data
{
    public class UsuarioRepository : IRepositoryUsuario
    {



        public bool Autentica(Usuario obj)
        {
            try
            {

                using (var repository = new ApiDbContext())
                {

                    MD5 md5Hash = MD5.Create();
                    var senhaCripotografada = GetMd5Hash(md5Hash, obj.Senha);
                    var existeNoBanco = repository.Usuarios.Where(c => c.Login.Equals(obj.Login) && c.Senha.Equals(senhaCripotografada)).SingleOrDefault();
                    if (existeNoBanco != null && existeNoBanco.Ativo)
                    {
                        return true;
                    }

                    return false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        static string GetMd5Hash(MD5 md5Hash, string senha)
        {

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
