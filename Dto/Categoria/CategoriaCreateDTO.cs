using System.ComponentModel.DataAnnotations;

namespace OrçamentoObra.Dto.Categoria
{
    public class CategoriaCreateDTO
    {
        [Required(ErrorMessage = "Informe o nome da categoria")]
        public string Nome { get; set; }
    }
}
