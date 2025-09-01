namespace OrçamentoObra.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Total { get; set; }
        public int CategoriaId { get; set; } //FK
        public CategoriaModel Categoria { get; set; } //(N:1)
    }
}
