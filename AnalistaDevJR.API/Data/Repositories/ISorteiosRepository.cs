using AnalistaDevJR.API.Models;
using System.Collections.Generic;

namespace AnalistaDevJR.API.Data.Repositories
{
    public interface ISorteiosRepository
    {
        void Adicionar(Sorteio sorteio);

        void Atualizar(string id, Sorteio sorteioAtualizado);

        IEnumerable<Sorteio> Buscar();

        Sorteio Buscar(string id);

        Sorteio BuscarNumSorte(int numSorte);                

        void Remover(string id);
    }
}
