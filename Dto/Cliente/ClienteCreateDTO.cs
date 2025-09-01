using System.ComponentModel.DataAnnotations;

namespace OrçamentoObra.Dto.Cliente
{
    public class ClienteCreateDTO
    {
        [Required(ErrorMessage = "Informe o nome do cliente")]
        public string Nome { get; set; }
    }
}
