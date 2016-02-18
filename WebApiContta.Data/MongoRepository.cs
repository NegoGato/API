
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiContta.Data
{
    public class MongoRepository<T>
    {
        private static Dictionary<string, MongoCollection<T>> collections = new Dictionary<string, MongoCollection<T>>();
        private static volatile MongoRepository<T> _repositoryInstance;

        public static MongoCollection<T> Instance
        {
            get
            {
                lock (typeof(T))
                {
                    if (_repositoryInstance == null)
                    {
                        _repositoryInstance = new MongoRepository<T>();
                    }

                    MongoCollection<T> instance;

                    if (!collections.TryGetValue(typeof(T).Name, out instance))
                    {
                        instance = _repositoryInstance.DB.GetCollection<T>(typeof(T).Name);
                        collections.Add(typeof(T).Name, instance);
                    }

                    return instance;
                }
            }
        }

        private MongoDatabase DB { get; set; }

        private MongoRepository()
        {
            string ip = "localhost",
                   database = "local";

            

            int port = 27017;
            this.DB = new MongoClient("mongodb://" + ip + ":" + port).GetServer().GetDatabase(database);
        }
    }
}
