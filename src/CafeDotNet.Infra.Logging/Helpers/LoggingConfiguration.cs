using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace CafeDotNet.Infra.Logging.Helpers;

public static class LoggingConfiguration
{
    public static void AddCustomSerilog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, services, configuration) =>
        {
            configuration
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    "logs/app-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1)
                );
        });
    }

    public static void UseCustomSerilog(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.IncludeQueryInRequestPath = true;
            options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
            options.GetLevel = (httpContext, elapsed, ex) =>
            {
                if (ex != null) return LogEventLevel.Error; 
                if (httpContext.Response.StatusCode >= 400) return LogEventLevel.Warning;
                return LogEventLevel.Verbose; 
            };
        });
    }
}
