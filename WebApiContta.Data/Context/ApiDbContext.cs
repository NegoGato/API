using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiContta.Data.Context.EntityConfiguration;
using WebApiContta.Domain.Aggregate.ClienteAgg;
using WebApiContta.Domain.Aggregate.UsuarioAgg;
using WebApiContta.Domain.Aggregate.ReceitaAgg;

namespace WebApiContta.Data.Context
{
    public class ApiDbContext:DbContext
    {

        public ApiDbContext()
            : base("WebApiContta")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CnaeSecundario> CnaeSecundarios { get; set; }
        public DbSet<Receita> Recitas { get; set; }
        
  
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClienteEntityConfiguration());
            modelBuilder.Configurations.Add(new UsuarioEntityConfiguration());
            modelBuilder.Configurations.Add(new CnaeSecundarioConfiguration());
            modelBuilder.Configurations.Add(new ReceitaConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
