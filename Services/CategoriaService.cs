using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrçamentoObra.Data;
using OrçamentoObra.Dto.Categoria;
using OrçamentoObra.Models;
using OrçamentoObra.Services.Interface;

namespace OrçamentoObra.Services
{
    public class CategoriaService : ICategoriaInterface
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CategoriaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseModel<CategoriaModel>> BuscarCategoriaId(int id)
        {
            ResponseModel<CategoriaModel> response = new();

            try
            {
                var categoria = await _context.Categorias.FindAsync(id);

                if (categoria == null)
                {
                    response.Status = false;
                    response.Mensagem = "Nehuma categoria localizada com esse Id";
                    return response;
                }

                response.Dados = categoria;
                response.Mensagem = "Categoria localizada com sucesso";
                return response;

            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public bool CategoriaExiste(CategoriaCreateDTO categoriaCreateDTO)
        {
            return _context.Categorias.Any(item => item.Nome  == categoriaCreateDTO.Nome);
        }

        public async Task<ResponseModel<CategoriaModel>> CriarCategoria(CategoriaCreateDTO categoriaCreateDTO)
        {
            ResponseModel<CategoriaModel> response = new();

            try
            {
                if (CategoriaExiste(categoriaCreateDTO))
                {
                    response.Status = false;
                    response.Mensagem = "Categoria já registrada";
                    return response;
                }

                CategoriaModel categoria = _mapper.Map<CategoriaModel>(categoriaCreateDTO);

                _context.Add(categoria);
                await _context.SaveChangesAsync();

                response.Dados = categoria;
                response.Mensagem = "Categoria criada com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<CategoriaModel>> DeletarCategoria(int id)
        {
            ResponseModel<CategoriaModel> response = new();

            try
            {
                var categoria = await _context.Categorias.FindAsync(id);

                if(categoria == null)
                {
                    response.Status = false;
                    response.Mensagem = "Categoria não localizada";
                    return response;
                }

                response.Dados = categoria;
                response.Mensagem = "Categoria removida com sucesso";

                _context.Categorias.Remove(categoria);
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

        public async Task<ResponseModel<CategoriaModel>> EditarCategoria(CategoriaUpdateDto categoriaUpdateDto)
        {
            ResponseModel<CategoriaModel> response = new();

            try
            {
                var CategoriaBanco = _context.Categorias.Find(categoriaUpdateDto.Id);

                if(CategoriaBanco == null)
                {
                    response.Status = false;
                    response.Mensagem = "Categoria não localizada";
                    return response;
                }

                CategoriaBanco.Nome = categoriaUpdateDto.Nome;

                _context.Categorias.Update(CategoriaBanco);
                await _context.SaveChangesAsync();

                response.Dados = CategoriaBanco;
                response.Mensagem = "Aluno cadastrado com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
            
        }

        public async Task<ResponseModel<List<CategoriaModel>>> ListarCategoriasPorOrcamento(int orcamentoId)
        {
            ResponseModel<List<CategoriaModel>> response = new();
            try
            {
                var categorias = await _context.Categorias
                    .Where(c => c.OrcamentoId == orcamentoId)
                    .ToListAsync();

                if (categorias == null || categorias.Count == 0)
                {
                    response.Dados = new List<CategoriaModel>();
                    response.Mensagem = "Nenhuma categoria encontrada para este orçamento";
                    return response;
                }

                response.Dados = categorias;
                response.Mensagem = "Categorias listadas com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;              
                return response;
            }
        }

        public async Task<ResponseModel<List<CategoriaModel>>> ListarTodasCategorias()
        {
            ResponseModel<List<CategoriaModel>> response = new();
            try
            {
                var categorias = await _context.Categorias.ToListAsync();

                response.Dados = categorias;
                response.Mensagem = "Categorias listadas com sucesso";
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
