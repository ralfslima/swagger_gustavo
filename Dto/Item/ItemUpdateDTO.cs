using System.ComponentModel.DataAnnotations;

namespace OrçamentoObra.Dto.Item
{
    public class ItemUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Quantidade { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal ValorUnitario { get; set; }

        public decimal Total => Quantidade * ValorUnitario;
    }
}
