using System.Data.SqlClient;
using System.Data;

namespace SportCompetitionsAPI.Service
{
    /// <summary>
    /// Класс для выполениня запростов к базе данных
    /// </summary>
    public class SqlQueries
    {
        /// <summary>
        /// Объект для доступа к базе данных
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// Конструктор
        /// </summary>
        public SqlQueries()
        {
            string connectionString =
                "Server = (localdb)\\mssqllocaldb; Database = SportCompetitions; Trusted_Connection = True";
            this.connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Метод для получения таблицы из базы данных
        /// </summary>
        /// <param name="queryString">Строка запроса</param>
        /// <returns>Таблица с данными</returns>
        public async Task<DataTable> QuerySelectAsync(string queryString)
        {
            var req = await Task.Run(() =>
            {
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            });
            return req;
        }

        /// <summary>
        /// Метод для изменения базы данных
        /// </summary>
        /// <param name="queryString">Строка запроса</param>
        /// <returns>Количество выполненных запросов</returns>
        public async Task<int> QueryChangesAsync(string queryString)
        {
            int numberCompletedRequests = 0;
            await connection.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(queryString, connection))
            {
                numberCompletedRequests = await cmd.ExecuteNonQueryAsync();
            }
            await connection.CloseAsync();
            return numberCompletedRequests;
        }
    }
}
