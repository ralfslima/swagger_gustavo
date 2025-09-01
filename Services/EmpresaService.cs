using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrçamentoObra.Data;
using OrçamentoObra.Dto.Cliente;
using OrçamentoObra.Dto.Empresa;
using OrçamentoObra.Models;
using OrçamentoObra.Services.Interface;

namespace OrçamentoObra.Services
{
    public class EmpresaService : IEmpresaInterface
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public EmpresaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseModel<EmpresaModel>> BuscarEmpresaPorId(int id)
        {
            ResponseModel<EmpresaModel> response = new();

            try
            {
                var empresa = await _context.Empresas.FindAsync(id);

                if (empresa == null)
                {
                    response.Status = false;
                    response.Mensagem = "Empresa não localizada";
                    return response;
                   
                }
                response.Dados = empresa;
                response.Mensagem = "Empresa localizada com sucesso";
                return response;

            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;

            }
        }

        public async Task<ResponseModel<EmpresaModel>> CriarEmpresa(EmpresaCreateDto empresaCreateDto)
        {
            ResponseModel<EmpresaModel> response = new();

            try
            {
                if (EmpresaExiste(empresaCreateDto))
                {
                    response.Status = false;
                    response.Mensagem = "Empresa já registrada";
                    return response;
                }
                EmpresaModel empresa = _mapper.Map<EmpresaModel>(empresaCreateDto);

                _context.Add(empresa);
                await _context.SaveChangesAsync();

                response.Dados = empresa;
                response.Mensagem = "Empresa cadastrada com sucesso";
                return response;

            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public bool EmpresaExiste(EmpresaCreateDto empresaCreateDto)
        {
            return _context.Empresas.Any(item => item.Cnpj == empresaCreateDto.Cnpj);
        }

        public async Task<ResponseModel<EmpresaModel>> EditarEmpresa(EmpresaUpdateDTO empresaUpdateDto)
        {
            ResponseModel<EmpresaModel> response = new();

            try
            {
                var empresaBanco = _context.Empresas.Find(empresaUpdateDto.Id);

                if(empresaBanco == null)
                {
                    response.Status = false;
                    response.Mensagem = "Empresa não localizada";
                    return response;
                }
                empresaBanco.RazaoSocial = empresaUpdateDto.RazaoSocial;
                empresaBanco.Cnpj = empresaUpdateDto.Cnpj;
                empresaBanco.Endereco = empresaUpdateDto.Endereco;
                empresaBanco.Telefone = empresaUpdateDto.Telefone;
                empresaBanco.Email = empresaUpdateDto.Email;
                empresaBanco.Responsavel = empresaUpdateDto.Responsavel;

                _context.Update(empresaBanco);
                await _context.SaveChangesAsync();

                response.Dados = empresaBanco;
                response.Mensagem = "Dados alterados com sucesso";
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<EmpresaModel>> ExcluirEmpresa(int id)
        {
            ResponseModel<EmpresaModel> response = new();

            try
            {
                var empresa = await _context.Empresas.FindAsync(id);

                if(empresa == null)
                {
                    response.Status = false;
                    response.Mensagem = "Empresa não localizada";
                    return response;
                }
                response.Dados = empresa;
                response.Mensagem = "Empresa removida com sucesso";

                _context.Remove(empresa);
                await _context.SaveChangesAsync();
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<List<EmpresaModel>>> ListarEmpresas()
        {
            ResponseModel<List<EmpresaModel>> response = new();

            try
            {
                var empresa = await _context.Empresas.ToListAsync();

                response.Dados = empresa;
                response.Mensagem = "Empresas listadas com sucesso";
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
