using OrçamentoObra.Dto.Item;
using OrçamentoObra.Models;

namespace OrçamentoObra.Services.Interface
{
    public interface IItemInterface
    {
        Task<ResponseModel<ItemModel>> CriarItem(ItemCreateDTO itemCreateDto);
        Task<ResponseModel<ItemModel>> EditarItem(ItemUpdateDTO itemUpdateDto);
        Task<ResponseModel<ItemModel>> ExcluirItem(int id);
        Task<ResponseModel<List<ItemModel>>> ListarItems();
        Task<ResponseModel<ItemModel>> BuscarItemPorId(int id);
        Task<ResponseModel<List<ItemModel>>> ListarItemPorCategoria(int categoriaId);
    }
}
