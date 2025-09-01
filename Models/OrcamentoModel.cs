namespace OrçamentoObra.Models
{
    public class OrcamentoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int ClienteId { get; set; } //FK
        public ClienteModel Cliente { get; set; } //Cada Orçamento pertence a um Cliente
        public ICollection<CategoriaModel> Categorias { get; set; } = new List<CategoriaModel>();
    }
}
