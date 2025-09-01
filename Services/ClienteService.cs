using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrçamentoObra.Data;
using OrçamentoObra.Dto.Categoria;
using OrçamentoObra.Dto.Cliente;
using OrçamentoObra.Models;
using OrçamentoObra.Services.Interface;

namespace OrçamentoObra.Services
{
    public class ClienteService : IClienteInterface
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ClienteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseModel<ClienteModel>> BuscarClienteId(int id)
        {
            ResponseModel<ClienteModel> response = new();

            try {
                var cliente = await _context.Clientes.FindAsync(id);

                if (cliente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Cliente não encontrado";
                    return response;
                }
                response.Dados = cliente;
                response.Mensagem = "Cliente localizado com sucesso";
                return response;

            }
            catch(Exception ex) 
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<ClienteModel>> CriarCliente(ClienteCreateDTO clienteCreateDTO)
        {
            ResponseModel<ClienteModel> response = new();

            try
            {
                if (CLienteExiste(clienteCreateDTO))
                {
                    response.Status = false;
                    response.Mensagem = "Cliente já registrado";
                    return response;
                }
                ClienteModel cliente = _mapper.Map<ClienteModel>(clienteCreateDTO);

                _context.Add(cliente);
                await _context.SaveChangesAsync();

                response.Dados = cliente;
                response.Mensagem = "Cliente cadastrado com sucesso";
                return response;

            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem = ex.Message;
                return response;
            }
        }
        public bool CLienteExiste(ClienteCreateDTO clienteCreateDTO)
        {
            return _context.Clientes.Any(item => item.Nome == clienteCreateDTO.Nome);
        }

        public async Task<ResponseModel<ClienteModel>> DeletarCliente(int id)
        {
            ResponseModel<ClienteModel> response = new();
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);

                if(cliente == null)
                {
                    response.Status = false;
                    response.Mensagem = "Cliente não localizado";
                    return response;
                }
                response.Dados = cliente;
                response.Mensagem = "Cliente removido com sucesso";

                _context.Remove(cliente);
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

        public async Task<ResponseModel<ClienteModel>> EditarCliente(ClienteUpdateDTO clienteUpdateDTO)
        {
            ResponseModel<ClienteModel> response = new();

            try
            {
                var clienteBanco = _context.Clientes.Find(clienteUpdateDTO.Id);

                if(clienteBanco == null)
                {
                    response.Status = false;
                    response.Mensagem = "Cliente não localizado";
                    return response;
                }
                clienteBanco.Nome = clienteUpdateDTO.Nome;

                _context.Update(clienteBanco);
                await _context.SaveChangesAsync();

                response.Dados = clienteBanco;
                response.Mensagem = "Cliente editado com sucesso";
                return response;
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Mensagem= ex.Message;
                return response;
            }
        }

        public async Task<ResponseModel<List<ClienteModel>>> ListarClientes()
        {
            ResponseModel<List<ClienteModel>> response = new();

            try
            {
                var clientes = await _context.Clientes.ToListAsync();

                response.Dados = clientes;
                response.Mensagem = "Clientes listados com sucesso";
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
