using AnalistaDevJR.API.Data.Repositories;
using AnalistaDevJR.API.Models;
using AnalistaDevJR.API.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace AnalistaDevJR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SorteiosController : ControllerBase
    {
        private ISorteiosRepository _sorteiosRepository;

        public SorteiosController(ISorteiosRepository sorteiosRepository)
        {
            _sorteiosRepository = sorteiosRepository;
        }

        // GET: api/sorteios
        [HttpGet]
        public IActionResult Get()
        {
            var sorteios = _sorteiosRepository.Buscar();
            return Ok(sorteios);
        }

        // GET api/sorteios/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var sorteio = _sorteiosRepository.Buscar(id);

            if (sorteio == null)
                return NotFound();

            return Ok(sorteio);
        }

        // POST api/sorteios
        [HttpPost]
        public IActionResult Post([FromBody] SorteioInputModel novoSorteio)
        {
            var sorteio = new Sorteio(novoSorteio.Id_Cliente);

            // verifica a quantide de números vendidos
            var qtdeVendida = _sorteiosRepository.Buscar();

            // atribui na qtdeVd o total de números já vendidos             
            int qtdeVd = qtdeVendida.Count();

            // se quantidade vendida for igual ao número máximo permitido
            if (qtdeVd == 100000) // deixei o total de números direto na condição, sei que não é o ideal, expliquei no Model "Sorteio"
            {
                return NotFound("Números esgotados!");
            }
            else
            {
                // número sorteado na compra
                int numSorteado = sorteio.NumSorte;

                // verifica se o número sorteado já foi vendido
                var numVendido = _sorteiosRepository.BuscarNumSorte(sorteio.NumSorte);

                // se não encontrou, realiza a venda
                if (numVendido == null)
                {
                    _sorteiosRepository.Adicionar(sorteio);

                    // diretório onde será salvo o comprovante de compra
                    StreamWriter sw = new StreamWriter("C:\\temp\\" + sorteio.NumSorte + ".txt");
                    sw.WriteLine("ID da compra: " + sorteio.Id);
                    sw.WriteLine("ID do cliente: " + sorteio.Id_Cliente);
                    sw.WriteLine("Data da compra: " + sorteio.DataCompra);
                    sw.WriteLine("Número da sorte: " + sorteio.NumSorte);
                    sw.Close();

                    return Created("Compra realizada com sucesso!", sorteio);
                }
                else
                {
                    // atribui o valor do número vendido
                    int xnumVd = numVendido.NumSorte;

                    while (numSorteado == xnumVd)
                    {
                        var newSorteio = new Sorteio(novoSorteio.Id_Cliente);

                        // novo número sorteado na compra
                        int numSt = newSorteio.NumSorte;

                        // verifica se o número sorteado já foi vendido
                        var numVdo = _sorteiosRepository.BuscarNumSorte(newSorteio.NumSorte);

                        // se não encontrou, realiza a venda
                        if (numVdo == null)
                        {
                            _sorteiosRepository.Adicionar(sorteio);

                            // diretório onde será salvo o comprovante de compra
                            StreamWriter sw = new StreamWriter("C:\\temp\\" + sorteio.NumSorte + ".txt");
                            sw.WriteLine("ID da compra: " + sorteio.Id);
                            sw.WriteLine("ID do cliente: " + sorteio.Id_Cliente);
                            sw.WriteLine("Data da compra: " + sorteio.DataCompra);
                            sw.WriteLine("Número da sorte: " + sorteio.NumSorte);
                            sw.Close();
                        }
                        else
                        {
                            int ynumVdo = numVdo.NumSorte;

                            numSorteado = numSt;
                            xnumVd = ynumVdo;
                        }
                    }

                    return Created("Compra realizada com sucesso!", sorteio);
                }
            }
        }

        /*
            No meu ponto de vista, após o cliente comprar um número da sorte, não seria justo deixar uma opção para
            alterar os dados da compra. Por esse motivo, não implementei o Put.        
        */

        // DELETE api/sorteios/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var sorteio = _sorteiosRepository.Buscar(id);

            if (sorteio == null)
                return NotFound();

            _sorteiosRepository.Remover(id);

            return NoContent();
        }
    }
}
