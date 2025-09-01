using OrçamentoObra.Dto.Cliente;
using OrçamentoObra.Models;

namespace OrçamentoObra.Services.Interface
{
    public interface IClienteInterface
    {
        Task<ResponseModel<ClienteModel>> CriarCliente(ClienteCreateDTO clienteCreateDTO);
        Task<ResponseModel<ClienteModel>> EditarCliente(ClienteUpdateDTO clienteUpdateDTO);
        Task<ResponseModel<ClienteModel>> DeletarCliente(int id);
        Task<ResponseModel<List<ClienteModel>>> ListarClientes();
        Task<ResponseModel<ClienteModel>> BuscarClienteId(int id);
    }
}
