namespace OrçamentoObra.Models
{
    public class CategoriaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int OrcamentoId { get; set; } //FK
        public OrcamentoModel Orcamento { get; set; } // N:1
        public ICollection<ItemModel> Itens { get; set; } = new List<ItemModel>(); //1:N
    }
}
