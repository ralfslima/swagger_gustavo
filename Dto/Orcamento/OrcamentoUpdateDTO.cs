using System.ComponentModel.DataAnnotations;

namespace OrçamentoObra.Dto.Orcamento
{
    public class OrcamentoUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}
