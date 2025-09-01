using OrçamentoObra.Enum;
using System.ComponentModel.DataAnnotations;

namespace OrçamentoObra.Models
{
    public class ObraModel
    {
        [Key]
        public int ObraId { get; set; }
        public string NomeObra { get; set; }
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public StatusObra Status { get; set; } //Enum
        public int EmpresaId { get; set; } //FK
        public EmpresaModel Empresa { get; set; } //Cada empresa pertence a uma obra
    }
}
