using FluentValidation;
using Newtonsoft.Json;
using System.Net;

namespace Applicants.APIs.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.Clear();
        context.Response.ContentType = "application/json";

        if (exception is ValidationException validationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            // Collect validation errors
            var validationErrors = validationException.Errors.Select(x => x.ErrorMessage).ToList();
            var failureMessages = string.Join(", ", validationErrors);

            var errorResult = BaseResponse.CreateProblemDetail(failureMessages);
            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorResult));
        }

        // Default error response
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var globalError = BaseResponse.CreateProblemDetail("An error occurred while processing the request.");
        return context.Response.WriteAsync(JsonConvert.SerializeObject(globalError));
    }
}

