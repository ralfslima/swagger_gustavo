using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrçamentoObra.Dto.Cliente;
using OrçamentoObra.Services.Interface;

namespace OrçamentoObra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteInterface _cliente;
        public ClienteController(IClienteInterface cliente)
        {
            _cliente = cliente;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarCliente([FromBody] ClienteCreateDTO clienteCreateDTO)
        {
            var cliente = await _cliente.CriarCliente(clienteCreateDTO);
            return Ok(cliente);
        }
        [HttpGet]
        public async Task<IActionResult> ListarClientes()
        {
            var clientes = await _cliente.ListarClientes();
            return Ok(clientes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCliente([FromBody] ClienteUpdateDTO clienteUpdateDTO)
        {
            var cliente = await _cliente.EditarCliente(clienteUpdateDTO);
            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverCliente(int id)
        {
            var cliente = await _cliente.DeletarCliente(id);
            return Ok(cliente);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarClientePorId(int id)
        {
            var cliente = await _cliente.BuscarClienteId(id);
            return Ok(cliente);
        }
    }
}
