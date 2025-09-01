using OrçamentoObra.Enum;
using System.ComponentModel.DataAnnotations;

namespace OrçamentoObra.Dto.Obra
{
    public class ObraCreateDTO
    {
        [Required(ErrorMessage = "Digite o nome da obra")]
        public string NomeObra { get; set; }

        [Required(ErrorMessage = "Digite a descrição da obra")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Digite o endereço da empresa")]
        public string Endereco { get; set; }
        public DateTime DataInicio { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; }

        [Required(ErrorMessage = "Digite o status da obra (Planejada, EmAndamento, Concluida ou Cancelada")]
        public StatusObra Status { get; set; }
    }
}

