using System.Text.Json;

namespace Order.Management.System.API.Middlewares
    {
    public class GlobalErrorHandlingMiddleware
        {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
            {
            _next = next;
            }

        public async Task InvokeAsync(HttpContext context)
            {
            try
                {
                await _next(context);
                }
            catch ( Exception ex )
                {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new { error = ex.Message });
                await context.Response.WriteAsync(result);
                }
            }
        }
    }
