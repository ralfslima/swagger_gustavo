using OrçamentoObra.Dto.Orcamento;
using OrçamentoObra.Models;

namespace OrçamentoObra.Services.Interface
{
    public interface IOrcamentoInterface
    {
        Task<ResponseModel<OrcamentoModel>> CriarOrcamento(OrcamentoCreateDTO orcamentoCreateDTO);
        Task<ResponseModel<OrcamentoModel>> EditarOrcamento(OrcamentoUpdateDTO orcamentoUpdateDTO);
        Task<ResponseModel<OrcamentoModel>> ExcluirOrcamento(int id);
        Task<ResponseModel<List<OrcamentoModel>>> ListarOrcamentos();
        Task<ResponseModel<OrcamentoModel>> BuscarOrcamentoPorId(int id);
        Task<ResponseModel<List<OrcamentoModel>>> ListarOrcamentoPorCliente(int clienteId);
    }
}
