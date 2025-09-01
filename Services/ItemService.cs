using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrçamentoObra.Data;
using OrçamentoObra.Dto.Item;
using OrçamentoObra.Models;
using OrçamentoObra.Services.Interface;

namespace OrçamentoObra.Services
{
    public class ItemService : IItemInterface
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ItemService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseModel<ItemModel>> BuscarItemPorId(int id)
        {
            ResponseModel<ItemModel> resonse = new();

            try
            {
                var item = await _context.Items.FindAsync(id);

                if(item == null)
                {
                    resonse.Status = false;
                    resonse.Mensagem = "Item não encontrado";
                    return resonse;
                   
                }
                resonse.Dados = item;
                resonse.Mensagem = "Item localizado!";
                return resonse;

            }
            catch(Exception ex)
            {
                resonse.Status = false;
                resonse.Mensagem = ex.Message;
                return resonse;
            }
        }

        public async Task<ResponseModel<ItemModel>> CriarItem(ItemCreateDTO itemCreateDto)
        {
            ResponseModel<ItemModel> response = new();

            try
            {
                if (ItemExiste(itemCreateDto))
                {
                    response.Status = false;
                    response.Mensagem = "Item já registrado";
                    return response;
                }
                ItemModel item = _mapper.Map<ItemModel>(itemCreateDto);

                _context.Add(item);
                await _context.SaveChangesAsync();

                response.Dados = item;
                response.Mensagem = "Item cadastrado com sucesso";
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public bool ItemExiste(ItemCreateDTO itemCreateDTO)
        {
            return _context.Items.Any(item => item.Descricao == itemCreateDTO.Descricao);
        }

        public async Task<ResponseModel<ItemModel>> EditarItem(ItemUpdateDTO itemUpdateDto)
        {
            ResponseModel<ItemModel> response = new();

            try
            {
                var itemBanco = _context.Items.Find(itemUpdateDto.Id);

                if(itemBanco == null)
                {
                    response.Status = false;
                    response.Mensagem = "Item não encontrado";
                    return response;
                }
                itemBanco.Descricao = itemUpdateDto.Descricao;
                itemBanco.Quantidade = itemUpdateDto.Quantidade;
                itemBanco.ValorUnitario = itemUpdateDto.ValorUnitario;
                itemBanco.Total = itemUpdateDto.Total;

                _context.Update(itemBanco);
                await _context.SaveChangesAsync();

                response.Dados = itemBanco;
                response.Mensagem = "Item alterado com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }
        
        public async Task<ResponseModel<ItemModel>> ExcluirItem(int id)
        {
            ResponseModel<ItemModel> response = new();

            try
            {
                var item = await _context.Items.FindAsync(id);

                if(item == null)
                {
                    response.Status = false;
                    response.Mensagem = "Item não localizado";
                    return response;
                }

                _context.Remove(item);
                await _context.SaveChangesAsync();

                response.Dados = item;
                response.Mensagem = "Item removido com sucesso";
                return response;

            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<List<ItemModel>>> ListarItemPorCategoria(int categoriaId)
        {
            ResponseModel<List<ItemModel>> response = new();
            try
            {
                var items = await _context.Items
                    .Where(c => c.CategoriaId == categoriaId)
                    .ToListAsync();

                if (items == null || items.Count == 0)
                {
                    response.Dados = new List<ItemModel>();
                    response.Status = true;
                    response.Mensagem = "Nenhum item encontrado para esta categoria";
                    return response;
                }

                response.Dados = items;
                response.Status = true;
                response.Mensagem = "Items listadas com sucesso";
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
         }

        public async Task<ResponseModel<List<ItemModel>>> ListarItems()
        {
            ResponseModel<List<ItemModel>> response = new();

            try
            {
                var items = await _context.Items.ToListAsync();

                response.Dados = items;
                response.Mensagem = "Items listados com sucesso";
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response ;
            }
        }
    }
}
