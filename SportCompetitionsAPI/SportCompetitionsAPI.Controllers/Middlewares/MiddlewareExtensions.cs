using Microsoft.AspNetCore.Diagnostics;

namespace SportCompetitionsAPI.Controllers.Middlewares
{
    /// <summary>
    /// Кастомные Middleware
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Глобальная обработка исключений
        /// </summary>
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }

        /// <summary>
        /// Создать задержку
        /// </summary>
        /// <param name="app"></param>
        public static void UseTimeDelayMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<TimeoutMiddleware>();
        }
    }
}
