using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrçamentoObra.Dto.Obra;
using OrçamentoObra.Services.Interface;

namespace OrçamentoObra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObraController : ControllerBase
    {
        private readonly IObraInterface _obrainterface;
        public ObraController(IObraInterface obraInterface)
        {
            _obrainterface = obraInterface;
        }

        [HttpPost]
        public async Task<IActionResult> CriarObra([FromBody]ObraCreateDTO obraCreateDTO)
        {
            var obra = await _obrainterface.CriarObra(obraCreateDTO);
            return Ok(obra);
        }

        [HttpGet]
        public async Task<IActionResult> ListarObras()
        {
            var obras = await _obrainterface.ListarObras();
            return Ok(obras);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarObra([FromBody] ObraUpdateDTO obraUpdateDTO)
        {
            var obra = await _obrainterface.EditarObra(obraUpdateDTO);
            return Ok(obra);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverObra(int id)
        {
            var obra = await _obrainterface.ExcluirObra(id);
            return Ok(obra);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarObraPorId(int id)
        {
            var obra = await _obrainterface.BuscarObraPorId(id);
            return Ok(obra);
        }
    }
}
