namespace OrçamentoObra.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<OrcamentoModel> Orcamentos { get; set; } = new List<OrcamentoModel>(); // Cliente pode ter varios orçamentos (1:N)
    }
}
