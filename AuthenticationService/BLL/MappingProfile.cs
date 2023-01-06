using AuthenticationService.DAL.Models;
using AuthenticationService.DAL.Models.ViewModels;
using AutoMapper;

namespace AuthenticationService.BLL
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Автомапер 
        /// </summary>
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>()
                .ConstructUsing(v => new UserViewModel(v));
        }
    }
}
