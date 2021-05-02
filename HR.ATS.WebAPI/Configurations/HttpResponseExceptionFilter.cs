using System.Net;
using HR.ATS.CrossCutting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HR.ATS.WebAPI.Configurations
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ValidationException exception)
            {
                var errorMessage = new TotvsErrorMessage(
                    exception.GetType().Name,
                    exception.Message,
                    exception.Message
                );
                context.Result = new ObjectResult(errorMessage)
                {
                    StatusCode = (int?) HttpStatusCode.BadRequest
                };
                context.ExceptionHandled = true;
            }
        }

        public int Order => int.MaxValue - 10;
    }
}