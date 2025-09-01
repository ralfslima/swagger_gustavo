using System.ComponentModel.DataAnnotations;

namespace OrçamentoObra.Dto.Empresa
{
    public class EmpresaCreateDto
    {
        [Required(ErrorMessage = "Digite a razão social da empresa")]
        public string RazaoSocial { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "CNPJ deve ter 14 caracteres.")]
        public string Cnpj { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Digite o telefone da empresa")]
        public string Telefone { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o nome do responsável da empresa")]
        public string Responsavel { get; set; }
    }
}
