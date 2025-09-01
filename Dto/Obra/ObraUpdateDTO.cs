using OrçamentoObra.Enum;
using System.ComponentModel.DataAnnotations;

namespace OrçamentoObra.Dto.Obra
{
    public class ObraUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string NomeObra { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public string Endereco { get; set; }
        public DateTime? DataFim { get; set; }

        [Required]
        public StatusObra Status { get; set; }
    }
}
