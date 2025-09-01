using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrçamentoObra.Services;
using OrçamentoObra.Services.Interface;

namespace OrçamentoObra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArquivoController : ControllerBase
    {
        private readonly IArquivoInterface _arquivoInterface;

        public ArquivoController(IArquivoInterface arquivoInterface)
        {
            _arquivoInterface = arquivoInterface;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile arquivo)
        {
            var resultado = await _arquivoInterface.SalvarArquivo(arquivo);
            return Ok(resultado);
        }

        [HttpPost("upload-multiple")]
        public async Task<IActionResult> UploadMultiple([FromForm] List<IFormFile> arquivos)
        {
            var resultado = await _arquivoInterface.SalvarArquivos(arquivos);
            return Ok(resultado);
        }
    }
}
