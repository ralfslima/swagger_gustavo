using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrçamentoObra.Dto.Categoria;
using OrçamentoObra.Services.Interface;

namespace OrçamentoObra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaInterface _categoria;
        public CategoriaController(ICategoriaInterface categoria)
        {
            _categoria = categoria;
        }

        [HttpPost]
        public async Task<IActionResult> CriarCategoria([FromBody] CategoriaCreateDTO categoriaCreateDTO)
        {
            var categoria = await _categoria.CriarCategoria(categoriaCreateDTO);
            return Ok(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> ListarCategorias()
        {
            var categorias = await _categoria.ListarTodasCategorias();
            return Ok(categorias);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCategoria([FromBody]CategoriaUpdateDto categoriaUpdateDto)
        {
            var categoria = await _categoria.EditarCategoria(categoriaUpdateDto);
            return Ok(categoria);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverCategoria(int id)
        {
            var categoria = await _categoria.DeletarCategoria(id);
            return Ok(categoria);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarCategoriaPorId(int id)
        {
            var categoria = await _categoria.BuscarCategoriaId(id);
            return Ok(categoria);
        }

        [HttpGet("Orcamento")]
        public async Task<IActionResult> ListarCategoriaPorOrcamento(int orcamentoId)
        {
            var categoria = await _categoria.ListarCategoriasPorOrcamento(orcamentoId);
            return Ok(categoria);
        }
        
    }
}
