using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiContta.Data.Context;
using WebApiContta.Data.Services.Interface;
using WebApiContta.Domain.Aggregate.ReceitaAgg;

namespace WebApiContta.Data
{
    public class ReceitaRepository:IRepositoryReceita
    {

        public List<Receita> Get()
        {
            try
            {
                using (var repository = new ApiDbContext())
                {
                    return repository.Recitas.Include(c=>c.ListaDeCnaes).ToList();
                    
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Receita> Get(string cnpj)
        {
            try
            {
                using (var repository = new ApiDbContext())
                {
                   return  repository.Recitas.Where(c => c.Cnpj.Equals(cnpj)).Include(c=>c.ListaDeCnaes).ToList();
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
