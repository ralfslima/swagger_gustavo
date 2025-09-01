using System.ComponentModel.DataAnnotations;

namespace OrçamentoObra.Dto.Item
{
    public class ItemCreateDTO
    {
        [Required(ErrorMessage = "Informe a descrição")]
        public string Descricao { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero.")]
        public decimal Quantidade { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor unitário deve ser maior que zero.")]
        public decimal ValorUnitario { get; set; }

        public decimal Total => Quantidade * ValorUnitario;
    }
}
