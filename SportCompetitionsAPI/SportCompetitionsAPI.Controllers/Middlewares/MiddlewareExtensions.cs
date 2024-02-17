using Microsoft.AspNetCore.Diagnostics;

namespace SportCompetitionsAPI.Controllers.Middlewares
{
    /// <summary>
    /// Кастомные Middleware
    /// </summary>
    public static class MiddlewareExtensions
    {
        public static void UseTimeDelayMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<TimeoutMiddleware>();
        }
    }
}
