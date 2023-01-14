using AuthenticationService.BLL.Models;
using AuthenticationService.BLL.ViewModels;
using AutoMapper;

namespace AuthenticationService.PLL.MappingProfiles
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
