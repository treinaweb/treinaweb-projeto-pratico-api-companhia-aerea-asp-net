using System.Net;
using System.Text.Json;
using FluentValidation;

namespace CiaAerea.Middlewares;

public class ValidationExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(ValidationException e)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.Conflict;
            var result = JsonSerializer.Serialize(new { erros = e.Errors.Select(erro => erro.ErrorMessage)});
            await response.WriteAsync(result);
        }
    }
}