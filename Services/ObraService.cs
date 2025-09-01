using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrçamentoObra.Data;
using OrçamentoObra.Dto.Item;
using OrçamentoObra.Dto.Obra;
using OrçamentoObra.Models;
using OrçamentoObra.Services.Interface;

namespace OrçamentoObra.Services
{
    public class ObraService : IObraInterface
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ObraService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseModel<ObraModel>> BuscarObraPorId(int id)
        {
            ResponseModel<ObraModel> response = new();

            try
            {
                var obra = await _context.Obras.FindAsync(id);

                if(obra == null)
                {
                    response.Status = false;
                    response.Mensagem = "Obra não localizada";
                    return response;
                }
                response.Dados = obra;
                response.Mensagem = "Obra localizada";
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<ObraModel>> CriarObra(ObraCreateDTO obraCreateDTO)
        {
            ResponseModel<ObraModel> response = new();

            try
            {
                if (ObraExiste(obraCreateDTO))
                {
                    response.Status = false;
                    response.Mensagem = "Obra já cadastrada";
                }

                ObraModel obra = _mapper.Map<ObraModel>(obraCreateDTO);

                _context.Add(obra);
                await _context.SaveChangesAsync();

                response.Dados = obra;
                response.Mensagem = "Obra cadastrada com sucesso";
                return response;

                

            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        private bool ObraExiste(ObraCreateDTO obraCreateDTO)
        {
            return _context.Obras.Any(item => item.NomeObra == obraCreateDTO.NomeObra);
        }

        public async Task<ResponseModel<ObraModel>> EditarObra(ObraUpdateDTO obraUpdateDTO)
        {
            ResponseModel<ObraModel> response = new();

            try
            {
                var obraBanco = _context.Obras.Find(obraUpdateDTO.Id);

                if (obraBanco == null)
                {
                    response.Status = false;
                    response.Mensagem = "Obra não localizada";
                    return response;
                }

                obraBanco.NomeObra = obraUpdateDTO.NomeObra;
                obraBanco.Descricao = obraUpdateDTO.Descricao;
                obraBanco.Endereco = obraUpdateDTO.Endereco;
                obraBanco.DataFim = obraUpdateDTO.DataFim;
                obraBanco.Status = obraUpdateDTO.Status;

                _context.Update(obraBanco);
                await _context.SaveChangesAsync();

                response.Dados = obraBanco;
                response.Mensagem = "Obra editada com sucesso";
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<ObraModel>> ExcluirObra(int id)
        {
            ResponseModel<ObraModel> response = new();

            try
            {
                var obra = await _context.Obras.FindAsync(id);

                if(obra == null)
                {
                    response.Status = false;
                    response.Mensagem = "Obra não localizada";
                    return response;
                }
                _context.Remove(obra);
                await _context.SaveChangesAsync();

                response.Dados = obra;
                response.Mensagem = "Obra removida com sucesso";
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<List<ObraModel>>> ListarObras()
        {
            ResponseModel<List<ObraModel>> response = new();

            try
            {
                var obras = await _context.Obras.ToListAsync();

                response.Dados = obras;
                response.Mensagem = "Obras listadas com sucesso";
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
