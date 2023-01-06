using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenticationService.BLL.Exceptions
{
    public class ExceptionHandler : ActionFilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// Метод для фильтрации исключений
        /// </summary>
        public void OnException(ExceptionContext context)
        {
            string message = "Произошла непредвиденная ошибка. Администраций сайта уже бежит на помощь!";

            if (context.Exception is CustomException)
            {
                message = context.Exception.Message;
            }

            context.Result = new BadRequestObjectResult(message);
        }
    }
}
