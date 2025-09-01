using OrçamentoObra.Dto.Obra;
using OrçamentoObra.Models;

namespace OrçamentoObra.Services.Interface
{
    public interface IObraInterface
    {
        Task<ResponseModel<ObraModel>> CriarObra(ObraCreateDTO obraCreateDTO);
        Task<ResponseModel<ObraModel>> EditarObra(ObraUpdateDTO obraUpdateDTO);
        Task<ResponseModel<ObraModel>> ExcluirObra(int id);
        Task<ResponseModel<List<ObraModel>>> ListarObras();
        Task<ResponseModel<ObraModel>> BuscarObraPorId(int id);
    }
}
