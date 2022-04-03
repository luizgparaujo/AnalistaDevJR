using AnalistaDevJR.API.Models;
using System.Collections.Generic;

namespace AnalistaDevJR.API.Data.Repositories
{
    public interface IClientesRepository
    {
        void Adicionar(Cliente cliente);

        void Atualizar(string id, Cliente clienteAtualizado);

        IEnumerable<Cliente> Buscar();

        Cliente Buscar(string id);

        Cliente BuscarEmail(string email);

        void Remover(string id);
    }
}
