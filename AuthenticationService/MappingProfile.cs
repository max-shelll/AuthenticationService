using AuthenticationService.Models;
using AuthenticationService.Models.ViewModel;
using AutoMapper;

namespace AuthenticationService
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
