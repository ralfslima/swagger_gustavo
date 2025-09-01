using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrçamentoObra.Dto.Empresa;
using OrçamentoObra.Models;
using OrçamentoObra.Services.Interface;

namespace OrçamentoObra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaInterface _empresaInterface;
        public EmpresaController(IEmpresaInterface empresaInterface)
        {
            _empresaInterface = empresaInterface;
        }

        [HttpPost]
        public async Task <IActionResult> RegistrarEmpresa([FromBody] EmpresaCreateDto empresaCreateDto)
        {
            var empresa = await _empresaInterface.CriarEmpresa(empresaCreateDto);
            return Ok(empresa);
        }

        [HttpGet]
        public async Task <IActionResult> ListarEmpresas()
        {
            var empresas = await _empresaInterface.ListarEmpresas();
            return Ok(empresas);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarEmpresa([FromBody] EmpresaUpdateDTO empresaUpdateDTO)
        {
            var empresa = await _empresaInterface.EditarEmpresa(empresaUpdateDTO);
            return Ok(empresa);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverEmpresa(int id)
        {
            var empresa = await _empresaInterface.ExcluirEmpresa(id);
            return Ok(empresa);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarEmpresaId(int id)
        {
            var empresa = await _empresaInterface.BuscarEmpresaPorId(id);
            return Ok(empresa);
        }
            

    }
}
