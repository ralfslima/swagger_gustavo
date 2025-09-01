using System.ComponentModel.DataAnnotations;

namespace OrçamentoObra.Dto.Cliente
{
    public class ClienteUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}
