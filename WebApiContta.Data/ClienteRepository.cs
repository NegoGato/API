
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApiContta.Data.Context;
using WebApiContta.Data.Services.Interface;
using WebApiContta.Domain.Aggregate.ClienteAgg;

namespace WebApiContta.Data
{
    public class ClienteRepository : IRepository
    {
        
        public List<Cliente> Get()
        {
            try
            {
                using ( var repository = new ApiDbContext())
                {
                    return repository.Clientes.ToList();
                }
                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
         
        }

        public List<Cliente> Get(string cnpj)
        {
            try
            {

                using (var repository = new ApiDbContext())
                {
                    return repository.Clientes.Where(c => c.Cnpj.Equals(cnpj)).ToList();
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
