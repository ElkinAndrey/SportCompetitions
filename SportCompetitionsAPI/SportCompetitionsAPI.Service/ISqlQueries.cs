using System.Data.SqlClient;
using System.Data;

namespace SportCompetitionsAPI.Service
{
    /// <summary>
    /// Выполенине запростов к базе данных
    /// </summary>
    public interface ISqlQueries
    {
        /// <summary>
        /// Метод для получения таблицы из базы данных
        /// </summary>
        /// <param name="queryString">Строка запроса</param>
        /// <returns>Таблица с данными</returns>
        Task<DataTable> QuerySelectAsync(string queryString);

        /// <summary>
        /// Метод для изменения базы данных
        /// </summary>
        /// <param name="queryString">Строка запроса</param>
        /// <returns>Количество выполненных запросов</returns>
        Task<int> QueryChangesAsync(string queryString);
    }
}
