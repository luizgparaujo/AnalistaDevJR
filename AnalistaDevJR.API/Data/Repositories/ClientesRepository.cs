using AnalistaDevJR.API.Data.Configurations;
using AnalistaDevJR.API.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace AnalistaDevJR.API.Data.Repositories
{
    public class ClientesRepository : IClientesRepository
    {
        private readonly IMongoCollection<Cliente> _clientes;

        public ClientesRepository(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);

            _clientes = database.GetCollection<Cliente>("clientes");
        }

        public void Adicionar(Cliente cliente)
        {
            _clientes.InsertOne(cliente);
        }

        public void Atualizar(string id, Cliente clienteAtualizado)
        {
            _clientes.ReplaceOne(c => c.Id == id, clienteAtualizado);
        }

        public IEnumerable<Cliente> Buscar()
        {
            return _clientes.Find(c => true).ToList();
        }

        public Cliente Buscar(string id)
        {
            return _clientes.Find(c => c.Id == id).FirstOrDefault();
        }

        public Cliente BuscarEmail(string email)
        {
            return _clientes.Find(c => c.Email == email).FirstOrDefault();
        }

        public void Remover(string id)
        {
            _clientes.DeleteOne(c => c.Id == id);
        }
    }
}
