using System.Data.SqlClient;
using System.Data;

namespace SportCompetitionsAPI.Service.ADO
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
        /// <param name="sqlParametes">Параметры в запросе</param>
        /// <returns>Таблица с данными</returns>
        Task<DataTable> QuerySelectAsync(
            string queryString,
            IEnumerable<SqlValues> sqlParametes = null!);

        /// <summary>
        /// Метод для изменения базы данных
        /// </summary>
        /// <param name="queryString">Строка запроса</param>
        /// <param name="sqlParametes">Параметры в запросе</param>
        /// <returns>Количество выполненных запросов</returns>
        Task<int> QueryChangesAsync(
            string queryString,
            IEnumerable<SqlValues> sqlParametes = null!);
    }
}
