using OrçamentoObra.Models;

namespace OrçamentoObra.Services.Interface
{
    public interface IArquivoInterface
    {
        Task<ResponseModel<string>> SalvarArquivo(IFormFile arquivo);
        Task<ResponseModel<List<string>>> SalvarArquivos(List<IFormFile> arquivos);
    }
}
