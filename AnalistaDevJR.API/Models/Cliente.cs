using System;

namespace AnalistaDevJR.API.Models
{
    public class Cliente
    {
        // Construtor
        public Cliente(string nome, string telefone, string cpf, string email)
        {
            Id = Guid.NewGuid().ToString();
            Nome = nome;
            Telefone = telefone;
            CPF = cpf.Replace(".", "").Replace("-", "");
            Email = email.Trim();
        }

        public string Id { get; private set; }

        public string Nome { get; private set; }

        public string Telefone { get; private set; }

        public string CPF { get; private set; }

        public string Email { get; private set; }

        public void AtualizarCliente(string nome, string telefone, string cpf, string email)
        {
            Nome = nome;
            Telefone = telefone;
            CPF = cpf.Replace(".", "").Replace("-", "");
            Email = email.Trim();
        }

    }
}
