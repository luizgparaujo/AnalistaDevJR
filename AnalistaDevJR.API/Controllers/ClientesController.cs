using AnalistaDevJR.API.Data.Repositories;
using AnalistaDevJR.API.Models;
using AnalistaDevJR.API.Models.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace AnalistaDevJR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private IClientesRepository _clientesRepository;

        public ClientesController(IClientesRepository clientesRepository)
        {
            _clientesRepository = clientesRepository;
        }

        // GET: api/clientes
        [HttpGet]
        public IActionResult Get()
        {
            var clientes = _clientesRepository.Buscar();
            return Ok(clientes);
        }

        // GET api/clientes/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var cliente = _clientesRepository.Buscar(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        // POST api/clientes
        [HttpPost]
        public IActionResult Post([FromBody] ClienteInputModel novoCliente)
        {
            var cliente = new Cliente(novoCliente.Nome, novoCliente.Telefone, novoCliente.CPF, novoCliente.Email);

            // Verifica se existe algum cliente com o e-mail informado
            var email = _clientesRepository.BuscarEmail(novoCliente.Email);

            if (email == null)
            {
                _clientesRepository.Adicionar(cliente);

                return Created("Cadastro realizado com sucesso!", cliente);
            }
            else
            {
                return NotFound("E-mail já cadastrado!");
            }
        }

        // PUT api/clientes/{id}
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ClienteInputModel atualizarCliente)
        {
            var cliente = _clientesRepository.Buscar(id);

            if (cliente == null)
                return NotFound();

            cliente.AtualizarCliente(atualizarCliente.Nome, atualizarCliente.Telefone, atualizarCliente.CPF, atualizarCliente.Email);

            // Verifica se existe algum cliente com o e-mail informado
            var email = _clientesRepository.BuscarEmail(atualizarCliente.Email);

            if (email == null)
            {
                _clientesRepository.Atualizar(id, cliente);

                return Ok(cliente);
            }
            else
            {
                return NotFound("E-mail já cadastrado!");
            }
        }

        // DELETE api/clientes/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var cliente = _clientesRepository.Buscar(id);

            if (cliente == null)
                return NotFound();

            _clientesRepository.Remover(id);

            return NoContent();
        }
    }
}
