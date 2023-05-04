using System.Net;
using System.Text.Json;
using Domain.Exceptions;

namespace Web.Middleware;

public class MiddlewareExceptionHandler
{
    private readonly IWebHostEnvironment _env;
    private readonly ILoggerFactory _logger;
    private readonly RequestDelegate _next;

    public MiddlewareExceptionHandler(IWebHostEnvironment env, ILoggerFactory logger, RequestDelegate next)
    {
        _env = env;
        _logger = logger;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); //request send if get error call catch
        }
        catch (Exception exception)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var result = ServerHandleError(context, exception, options);
            result = HandleResult(context, exception, result, options);
            await context.Response.WriteAsync(result);
        }
    }

    private static string ServerHandleError(HttpContext context, Exception exception, JsonSerializerOptions options)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonSerializer.Serialize(new ApiToReturn(500, exception.Message), options);
        return result;
    }

    private static string HandleResult(HttpContext context, Exception exception, string result, JsonSerializerOptions options)
    {
        switch (exception)
        {
            case NotFoundException notFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new ApiToReturn(404, notFoundException.Message,notFoundException.Messages,exception.Message),options);
                break;
            case BadRequestEntityException badRequestException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new ApiToReturn(400, badRequestException.Message, badRequestException.Messages, exception.Message),options);
                break;
            case ValidtionEntityException validtionEntityException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new ApiToReturn(400, validtionEntityException.Message, validtionEntityException.Messages, exception.Message),options);
                break;
        }

        return result;
    }
}