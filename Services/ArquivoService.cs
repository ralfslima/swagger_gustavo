using OrçamentoObra.Models;
using OrçamentoObra.Services.Interface;

namespace OrçamentoObra.Services
{
    public class ArquivoService : IArquivoInterface
    {
        private readonly IWebHostEnvironment _env;

        public ArquivoService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<ResponseModel<string>> SalvarArquivo(IFormFile arquivo)
        {
            ResponseModel<string> response = new();

            try
            {
                if (arquivo == null || arquivo.Length == 0)
                {
                    response.Status = false;
                    response.Mensagem = "Arquivo inválido.";
                    return response;
                }

                var pastaDestino = Path.Combine(_env.ContentRootPath, "Arquivos");
                if (!Directory.Exists(pastaDestino))
                    Directory.CreateDirectory(pastaDestino);

                var nomeArquivo = $"{Guid.NewGuid()}_{arquivo.FileName}";
                var caminhoArquivo = Path.Combine(pastaDestino, nomeArquivo);

                using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }

                response.Dados = nomeArquivo;
                response.Mensagem = "Arquivo salvo com sucesso.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<List<string>>> SalvarArquivos(List<IFormFile> arquivos)
        {
            ResponseModel<List<string>> response = new() { Dados = new List<string>() };

            try
            {
                foreach (var arquivo in arquivos)
                {
                    var resultado = await SalvarArquivo(arquivo);
                    if (!resultado.Status)
                    {
                        response.Status = false;
                        response.Mensagem = resultado.Mensagem;
                        return response;
                    }

                    response.Dados.Add(resultado.Dados);
                }

                response.Mensagem = "Todos os arquivos foram salvos com sucesso.";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }
    }
}

