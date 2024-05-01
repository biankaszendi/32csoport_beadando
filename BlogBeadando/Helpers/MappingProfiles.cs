using AutoMapper;
using BlogBeadando.Models;
namespace BlogBeadando.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterInputModel, User>().ReverseMap();
        }
    }
}
