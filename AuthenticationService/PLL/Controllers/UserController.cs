using AuthenticationService.BLL.Exceptions;
using AuthenticationService.BLL.Models;
using AuthenticationService.BLL.ViewModels;
using AuthenticationService.DAL.Repository;
using AuthenticationService.PLL.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationService.PLL.Controllers
{
    [ExceptionHandler]
    [ApiController]
    [Route("[controller]")] // Путь
    public class UserController : ControllerBase
    {
        private ILogger _logger;
        private IMapper _mapper;
        private IUserRepository _userRepository;

        public UserController(ILogger logger, IMapper mapper, IUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;

            logger.WriteEvent("Сообщение о событии в программе");
            logger.WriteError("Сообщение об ошибки в программе");

        }

        [HttpGet]
        public User GetUser()
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Иван",
                LastName = "Иванов",
                Email = "ivan@yandex.ru",
                Password = "11111122222qq",
                Login = "ivanov"
            };
        }

        [Authorize(Roles = "Администратор")] // Указание на необходимость авторизации в аккаунт с ролью "Администратор"
        [HttpGet]
        [Route("ViewModel")]
        public UserViewModel GetUserViewModel()
        {
            User user = GetUser();

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<UserViewModel> Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Запрос не корректен");

            User user = _userRepository.GetByLogin(login);

            if (user is null)
                throw new AuthenticationException("Пользователь на найден");
            else if (user.Password != password)
                throw new AuthenticationException("Введенный пароль не корректен");

            // Claim - это обьект с информацией о пользователе
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "AppCookie", // Название Claima
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);


            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
