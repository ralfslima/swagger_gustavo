using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrçamentoObra.Dto.Orcamento;
using OrçamentoObra.Services.Interface;

namespace OrçamentoObra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrcamentoController : ControllerBase
    {
        private readonly IOrcamentoInterface _orcamentoInterface;

        public OrcamentoController(IOrcamentoInterface orcamentoInterface)
        {
            _orcamentoInterface = orcamentoInterface;
        }

        [HttpPost]
        public async Task<IActionResult> CriarOrcamento([FromBody]OrcamentoCreateDTO orcamentoCreateDTO)
        {
            var orcamento = await _orcamentoInterface.CriarOrcamento(orcamentoCreateDTO);
            return Ok(orcamento);
        }

        [HttpGet]
        public async Task<IActionResult> ListarOrcamento()
        {
            var orcamentos = await _orcamentoInterface.ListarOrcamentos();
            return Ok(orcamentos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarOrcamento([FromBody] OrcamentoUpdateDTO orcamentoUpdateDTO)
        {
            var orcamento = await _orcamentoInterface.EditarOrcamento(orcamentoUpdateDTO);
            return Ok(orcamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverOrcamento(int id)
        {
            var orcamento = await _orcamentoInterface.ExcluirOrcamento(id);
            return Ok(orcamento);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarOrcamentoPorId(int id)
        {
            var orcamento = await _orcamentoInterface.BuscarOrcamentoPorId(id);
            return Ok(orcamento);
        }

        [HttpGet("Cliente")]
        public async Task<IActionResult> BuscarOrcamentoPorCliente(int clienteId)
        {
            var orcamento = await _orcamentoInterface.ListarOrcamentoPorCliente(clienteId);
            return Ok(orcamento);
        }
    }
}
