using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaHomomorphicApis.DAL;
using PruebaTecnicaHomomorphicApis.modelos;
using PruebaTecnicaHomomorphicApis.Services;

namespace PruebaTecnicaHomomorphicApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesServices clientesRepository;
        public ClientesController(ClientesServices clientesRepository)
        {
            this.clientesRepository = clientesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()
        {
            var clientes = clientesRepository.GetClientes();
            return clientes;
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<Clientes>> GetCliente(String id)
        {
            var cliente = clientesRepository.buscar(id);

            if (cliente != null)
            {
                return cliente;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(Clientes clientes)
        {
            if (clientesRepository.guardar(clientes))
            {
                return Ok(clientes);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClientes(String id)
        {
            if (clientesRepository.eliminar(id))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
