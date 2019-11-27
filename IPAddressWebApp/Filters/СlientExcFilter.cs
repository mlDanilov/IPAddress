using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace IPAddressWebApp.Filters
{
   
    public abstract class ClientExcFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionMessage = context.Exception.Message;
            context.Result = new ContentResult
            {
                Content = getMessage(exceptionMessage)
            };
            context.HttpContext.Response.StatusCode = 400;
            context.ExceptionHandled = false;
        }
        protected abstract string getMessage(string exceptionMessage_);
    }
    /// <summary>
    /// Фильтр срабатывает, если возникает исключение
    /// при добавлении новой подсети
    /// </summary>
    public class AddClientExcFilterAttribute : ClientExcFilterAttribute
    {
        protected override string getMessage(string exceptionMessage_)
        {
            return $"При добавлении нового клиента возникло исключение: \n {exceptionMessage_}";
        }
    }
    /// <summary>
    /// Фильтр срабатывает, если возникает исключение
    /// при редактировании подсети
    /// </summary>
    public class EditClientExcFilterAttribute : ClientExcFilterAttribute
    {
        protected override string getMessage(string exceptionMessage_)
        {
            return $"При редактировании клиента возникло исключение: \n {exceptionMessage_}";
        }
    }

    /// <summary>
    /// Фильтр срабатывает, если возникает исключение
    /// при удалении подсети
    /// </summary>
    public class DeleteClientExcFilterAttribute : ClientExcFilterAttribute
    {
        protected override string getMessage(string exceptionMessage_)
        {
            return $"При удалении подсети возникло исключение: \n {exceptionMessage_}";
        }

    }
}