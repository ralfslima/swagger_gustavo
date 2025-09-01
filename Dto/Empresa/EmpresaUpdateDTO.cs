using System.ComponentModel.DataAnnotations;

namespace OrçamentoObra.Dto.Empresa
{
    public class EmpresaUpdateDTO
    {
        public int Id { get; set; }

        [Required]
        public string RazaoSocial { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14)]
        public string Cnpj { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Endereco { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string Responsavel { get; set; }
    }
}
