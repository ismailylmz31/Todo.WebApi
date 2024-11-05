using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Todo.Core.Entities;
using Todo.Core.Exceptions;

namespace Todo.WebApi.MiddleWares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var errors = new ReturnModel<List<string>>
            {
                Success = false,
                Status = httpContext.Response.StatusCode
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            if (exception is NotFoundException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                errors.Message = exception.Message;
                errors.Status = 404;
            }
            else if (exception is ValidationException validationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                errors.Data = validationException.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList();
                errors.Message = "Validation errors occurred";
                errors.Status = 400;
            }
            else if (exception is BusinessException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                errors.Message = exception.Message;
                errors.Status = 400;
            }
            else
            {
                // Beklenmeyen hata durumunda 500 yanıtı
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                errors.Message = "An unexpected error occurred.";
                errors.Status = 500;
            }

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errors, jsonOptions));
        }
    }
}
