using System;
using System.Net.Mail;

namespace AuthenticationService.DAL.Models.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public bool FromRussia { get; set; }

        public UserViewModel(User user)
        {
            Id = user.Id;
            FullName = GetFullName(user.FirstName, user.LastName);
            FromRussia = GetFromRussiaValue(user.Email);
        }

        /// <summary>
        /// Метод для получения полного имени (имя + фамилия)
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <returns></returns>
        public string GetFullName(string firstName, string lastName)
        {
            return string.Concat(firstName, " ", lastName);
        }

        /// <summary>
        /// Метод для проверки на проживания в России (спомощью почты)
        /// </summary>
        /// <param name="email">Почта</param>
        /// <returns></returns>
        public bool GetFromRussiaValue(string email)
        {
            MailAddress mailAddress = new MailAddress(email);

            if (mailAddress.Host.Contains(".ru"))
                return true;
            else
                return false;
        }

    }
}
