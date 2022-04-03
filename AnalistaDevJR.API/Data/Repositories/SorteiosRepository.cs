using AnalistaDevJR.API.Data.Configurations;
using AnalistaDevJR.API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace AnalistaDevJR.API.Data.Repositories
{
    public class SorteiosRepository : ISorteiosRepository
    {
        private readonly IMongoCollection<Sorteio> _sorteios;

        public SorteiosRepository(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            var databse = client.GetDatabase(databaseConfig.DatabaseName);

            _sorteios = databse.GetCollection<Sorteio>("sorteios");
        }       

        public void Adicionar(Sorteio sorteio)
        {
            _sorteios.InsertOne(sorteio);
        }

        public void Atualizar(string id, Sorteio sorteioAtualizado)
        {
            _sorteios.ReplaceOne(s => s.Id == id, sorteioAtualizado);
        }

        public IEnumerable<Sorteio> Buscar()
        {
            return _sorteios.Find(s => true).ToList();
        }

        public Sorteio Buscar(string id)
        {
            return _sorteios.Find(s => s.Id == id).FirstOrDefault();
        }

        public Sorteio BuscarNumSorte(int numSorte)
        {
            return _sorteios.Find(s => s.NumSorte == numSorte).FirstOrDefault();
        }
        public void Remover(string id)
        {
            _sorteios.DeleteOne(s => s.Id == id);
        }
    }
}


