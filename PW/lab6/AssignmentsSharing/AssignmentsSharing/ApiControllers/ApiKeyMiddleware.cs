using Microsoft.AspNetCore.Mvc;

namespace AssignmentsSharing.ApiControllers
{
    public class ApiKeyMiddleware
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                IConfiguration configuration =
                context.RequestServices.GetRequiredService<IConfiguration>();
                if (context.Request.Headers.TryGetValue("ApiKey", out var apiKey)
                && apiKey == configuration["ApiKey"])
                    await next.Invoke();
                else
                {
                    context.Response.StatusCode = 401;
                    context.Response.WriteAsync("Unauthorized").Wait();
                }
            });
        }
    }
}
