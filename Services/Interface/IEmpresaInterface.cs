using OrçamentoObra.Dto.Empresa;
using OrçamentoObra.Models;

namespace OrçamentoObra.Services.Interface
{
    public interface IEmpresaInterface
    {
        Task<ResponseModel<EmpresaModel>> CriarEmpresa(EmpresaCreateDto empresaCreateDto);
        Task<ResponseModel<EmpresaModel>> EditarEmpresa(EmpresaUpdateDTO empresaUpdateDto);
        Task<ResponseModel<EmpresaModel>> ExcluirEmpresa(int id);
        Task<ResponseModel<List<EmpresaModel>>> ListarEmpresas();
        Task<ResponseModel<EmpresaModel>> BuscarEmpresaPorId(int id);
    }
}
