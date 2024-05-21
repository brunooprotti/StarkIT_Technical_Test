using AutoMapper;
using StarkIT.Application.Features.Name.Commands.CreateNewName;
using StarkIT.Domain.Models;

namespace StarkIT.Application.Mapper
{
    public class MappersProfile : Profile
    {
        public MappersProfile() 
        {
            CreateMap<CreateNewNameCommand,Names>().ReverseMap();
        }
    }
}
