using System;

namespace AnalistaDevJR.API.Models
{
    public class Sorteio
    {
        /*
        public int numMin = 0;
        public int numMax = 5;
        */
        public Sorteio(string id_cliente)
        {
            Id = Guid.NewGuid().ToString();
            Id_Cliente = id_cliente;
            DataCompra = DateTime.Now;

            Random numAleatorio = new Random();
            NumSorte = numAleatorio.Next(0, 100000);

            // tinha feito dessa forma, porém ao executar a API, na collection "sorteios" estava salvando as variáveis numMin e NumMax;
            // NumSorte = numAleatorio.Next(numMin, numMax);
        }

        public string Id { get; private set; }

        public string Id_Cliente { get; private set; }

        public DateTime DataCompra { get; private set; }

        public int NumSorte { get; private set; }

    }
}
