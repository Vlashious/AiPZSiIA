using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class Logger
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public Logger(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<Logger>();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        finally
        {
            _logger.LogInformation("Request {method} {url} => {statusCode}",
                context.Request?.Method, 
                context.Request?.Path.Value, 
                context.Response?.StatusCode);
        }
    }
}