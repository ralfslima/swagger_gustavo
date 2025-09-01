using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrçamentoObra.Data;
using OrçamentoObra.Dto.Categoria;
using OrçamentoObra.Dto.Orcamento;
using OrçamentoObra.Models;
using OrçamentoObra.Services.Interface;

namespace OrçamentoObra.Services
{
    public class OrcamentoService : IOrcamentoInterface
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public OrcamentoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseModel<OrcamentoModel>> BuscarOrcamentoPorId(int id)
        {
            ResponseModel<OrcamentoModel> response = new();

            try
            {
                var orcamento = await _context.Orcamentos.FindAsync(id);
                if (orcamento == null)
                {
                    response.Status = false;
                    response.Mensagem = "Orçamento não localizado";
                    return response;
                }           

                response.Dados = orcamento;
                response.Mensagem = "Orçamento localizado";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public bool OrcamentoExiste(OrcamentoCreateDTO orcamentoCreateDTO)
        {
            return _context.Categorias.Any(item => item.Nome == orcamentoCreateDTO.Nome);
        }

        public async Task<ResponseModel<OrcamentoModel>> CriarOrcamento(OrcamentoCreateDTO orcamentoCreateDTO)
        {
            ResponseModel<OrcamentoModel> response = new();

            try
            {
                if (OrcamentoExiste(orcamentoCreateDTO)){
                    response.Status = false;
                    response.Mensagem = "Orçamento já criado";
                    return response;
                }
                OrcamentoModel orcamento = _mapper.Map<OrcamentoModel>(orcamentoCreateDTO);

                _context.Orcamentos.Add(orcamento);
                await _context.SaveChangesAsync();

                response.Dados = orcamento;
                response.Mensagem = "Orçamento criado com sucesso";
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<OrcamentoModel>> EditarOrcamento(OrcamentoUpdateDTO orcamentoUpdateDTO)
        {
            ResponseModel<OrcamentoModel> response = new();
            try
            {
                var orcamentoBanco = _context.Orcamentos.Find(orcamentoUpdateDTO.Id);

                if (orcamentoBanco == null)
                {
                    response.Status = false;
                    response.Mensagem = "Orçamento não localizado";
                    return response;
                }

                orcamentoBanco.Nome = orcamentoUpdateDTO.Nome;

                _context.Orcamentos.Update(orcamentoBanco);
                await _context.SaveChangesAsync();

                response.Dados = orcamentoBanco;
                response.Mensagem = "Orçamento alterado com sucesso";
                return response;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }
        
                               
        public async Task<ResponseModel<OrcamentoModel>> ExcluirOrcamento(int id)
        {
            ResponseModel<OrcamentoModel> response = new();

            try
            {
                var orcamento = _context.Orcamentos.Find(id);

                if(orcamento == null)
                {
                    response.Status = false;
                    response.Mensagem = "Orçamento não localizado";
                    return response;
                }

                _context.Orcamentos.Remove(orcamento);
                await _context.SaveChangesAsync();

                response.Dados = orcamento;
                response.Mensagem = "Orçamento removido com sucesso";
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<List<OrcamentoModel>>> ListarOrcamentoPorCliente(int clienteId)
        {
            ResponseModel<List<OrcamentoModel>> response = new();

            try
            {
                var orcamentos = await _context.Orcamentos
                    .Where(c => c.ClienteId == clienteId)
                    .ToListAsync();

                if (orcamentos == null || orcamentos.Count == 0)
                {
                    response.Dados = new List<OrcamentoModel>();                 
                    response.Mensagem = "Nenhum orçamento encontrado para este cliente";
                    return response;
                }
                response.Dados = orcamentos;
                response.Mensagem = "Orçamentos listados com sucesso";
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<List<OrcamentoModel>>> ListarOrcamentos()
        {
            ResponseModel<List<OrcamentoModel>> response = new();

            try
            {
                var orcamentos = await _context.Orcamentos.ToListAsync();

                response.Dados = orcamentos;
                response.Mensagem = "Orçamentos listados com sucesso";
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }

        }
    }
}
