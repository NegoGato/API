using System;
using System.Collections.Generic;
using WebApiContta.Domain.Aggregate.ReceitaAgg;

namespace WebApiContta.Data.Services.Interface
{
    public interface IRepositoryReceita:IDisposable
    {

        List<Receita> Get();

        List<Receita> Get(string cnpj);

    }
}
