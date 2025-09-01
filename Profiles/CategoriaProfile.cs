using AutoMapper;
using OrçamentoObra.Dto.Categoria;
using OrçamentoObra.Models;


namespace OrçamentoObra.Profiles
{
    public class CategoriaProfile : Profile
    {
         public CategoriaProfile()
        {
            CreateMap<CategoriaCreateDTO, CategoriaModel>();
        }
    }
}
