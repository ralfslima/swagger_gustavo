using System.ComponentModel.DataAnnotations;

namespace OrçamentoObra.Dto.Categoria
{
    public class CategoriaUpdateDto
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}
