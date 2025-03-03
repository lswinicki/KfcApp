using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.ExceptionHandler;

[Serializable]
public class ProblemExceptionFilter : IAsyncExceptionFilter
{
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ProblemValidationException validationException:
            {
                if (context.Exception is not ProblemValidationException)
                {
                    return;
                }

                context.HttpContext.Response.StatusCode = 400;
                context.Result = new ObjectResult(new ProblemDetails
                {
                    Status = 400,
                    Title = validationException.Error,
                    Detail = validationException.Message.ToString(),
                    Type = "Bad request"
                });
            }
                break;
            case ProblemException problemException:
            {
                if (context.Exception is not ProblemException)
                {
                    return;
                }

                context.HttpContext.Response.StatusCode = 400;
                context.Result = new ObjectResult(new ProblemDetails
                {
                    Status = 400,
                    Title = problemException.Error,
                    Detail = problemException.Message,
                    Type = "Bad request"
                });
            }
                break;
        }
    }
}