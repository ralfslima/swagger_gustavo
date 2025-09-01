using OrçamentoObra.Dto.Categoria;
using OrçamentoObra.Models;

namespace OrçamentoObra.Services.Interface
{
    public interface ICategoriaInterface
    {
        Task<ResponseModel<CategoriaModel>> CriarCategoria(CategoriaCreateDTO categoriaCreateDTO);
        Task<ResponseModel<CategoriaModel>> EditarCategoria(CategoriaUpdateDto categoriaUpdateDto);
        Task<ResponseModel<CategoriaModel>> DeletarCategoria(int id);
        Task<ResponseModel<List<CategoriaModel>>> ListarTodasCategorias();
        Task<ResponseModel<CategoriaModel>> BuscarCategoriaId(int id);
        Task<ResponseModel<List<CategoriaModel>>> ListarCategoriasPorOrcamento(int orcamentoId);
    }
}
