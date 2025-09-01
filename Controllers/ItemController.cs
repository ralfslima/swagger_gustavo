using Microsoft.AspNetCore.Mvc;  // Adicionando o namespace correto para MVC

namespace OrçamentoObra.Controllers
{
    [ApiController] // Especifica que a classe é um controlador de API
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        // Método que lista todos os itens
        [HttpGet("")]
        public IActionResult ListarItems()
        {
            // Seu código para listar todos os itens
            return Ok();
        }

        // Método que lista itens por categoria
        [HttpGet("categoria/{categoriaId}")]
        public IActionResult ListarItemPorCategoria(int categoriaId)
        {
            // Seu código para listar itens por categoria
            return Ok();
        }
    }
}
