namespace OrçamentoObra.Models
{
    public class EmpresaModel
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Responsavel { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public ICollection<ObraModel> Obras { get; set; } = new List<ObraModel>();
        //Uma empresa pode ter varias obras(1:N)
    }
}





