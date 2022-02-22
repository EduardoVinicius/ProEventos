using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Domain;

namespace ProEventos.Application.Helpers
{
    public class ProEventosProfile : Profile
    {
        public ProEventosProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Batch, BatchDto>().ReverseMap();
            CreateMap<SocialNetwork, SocialNetworkDto>().ReverseMap();
            CreateMap<Speaker, SpeakerDto>().ReverseMap();
        }
    }
}