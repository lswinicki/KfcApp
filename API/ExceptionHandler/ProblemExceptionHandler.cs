using Domain.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.ExceptionHandler;

[Serializable]
public class ProblemExceptionHandler : IExceptionHandler
{
    private IProblemDetailsService _problemDetailsService;

    public ProblemExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        switch (exception)
        {
            case ProblemValidationException validationException:
            {
                if (exception is not ProblemValidationException)
                {
                    return true;
                }
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = validationException.Error,
                    Detail = validationException.Message.ToString(),
                    Type = "ValidationProblem"
                };

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
                {
                    HttpContext = httpContext,
                    ProblemDetails = problemDetails
                });
            }
            case ProblemException problemException:
            {
                if (exception is not ProblemException)
                {
                    return true;
                }
                
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = problemException.Error,
                    Detail = problemException.Message,
                    Type = "Bad request"
                };

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
                {
                    HttpContext = httpContext,
                    ProblemDetails = problemDetails
                });
            }
        }

        return false;
    }
}