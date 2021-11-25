using Mde.WishList.Api.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mde.WishList.Api.WebApi.Middlewares
{
    /// <summary>
    /// Middleware to present exceptions as structured HTTP responses
    /// </summary>
    internal sealed class ExceptionPresenter : IMiddleware
    {
        private readonly ILogger<ExceptionPresenter> _logger;
        public ExceptionPresenter(ILogger<ExceptionPresenter> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var response = GetResponse(exception);
            httpContext.Response.StatusCode = response.StatusCode;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }


        private static ErrorResponse GetResponse(Exception exception) =>
            exception switch
            {
                NotFoundException notFoundException => new ErrorResponse { 
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "Not Found",
                    Message = notFoundException.Message
                },
                ValidationException validationException => new ValidationErrorResponse
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Title = "Validation Failed",
                    Message = validationException.Message,
                    ValidationErrors = new ReadOnlyDictionary<string, string[]>(validationException.Errors)
                },
                ForbiddenAccessException forbiddenException => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Title = "Forbidden",
                    Message = forbiddenException.Message
                },
                UnauthorizedAccessException notunauthorizedException => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Message = notunauthorizedException.Message
                },
                ApplicationException applicationException => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Application Error",
                    Message = applicationException.Message
                },
                _ => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Server Error",
                    Message = exception.Message
                }
            };
    }

    public class ErrorResponse
    {
        public string Title { get; set; }
        public string Message { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
    }

    public class ValidationErrorResponse : ErrorResponse
    {
        public IReadOnlyDictionary<string, string[]> ValidationErrors { get; set; }
    }
}
