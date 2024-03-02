using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace SportCompetitionsAPI.Service.ADO
{
    /// <summary>
    /// Выполенине запростов к базе данных
    /// </summary>
    public class SqlQueries : ISqlQueries
    {
        /// <summary>
        /// Объект для доступа к базе данных
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// Конструктор
        /// </summary>
        public SqlQueries(IConfiguration configuration)
        {
            string connectionString = configuration.GetSection("AppSettings:ConnectionString").Value!;
            connection = new SqlConnection(connectionString);
        }

        public async Task<DataTable> QuerySelectAsync(
            string queryString,
            IEnumerable<SqlValues> sqlParametes = null!)
        {
            await Console.Out.WriteLineAsync(queryString);
            var req = await Task.Run(() =>
            {
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                if (sqlParametes is not null)
                    foreach (SqlValues sqlParamete in sqlParametes)
                        adapter.SelectCommand.Parameters.AddWithValue(sqlParamete.Name, sqlParamete.Value);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            });
            return req;
        }

        public async Task<int> QueryChangesAsync(
            string queryString,
            IEnumerable<SqlValues> sqlParametes = null!)
        {
            await Console.Out.WriteLineAsync(queryString);
            int numberCompletedRequests = 0;
            await connection.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(queryString, connection))
            {
                if (sqlParametes is not null)
                    foreach (SqlValues sqlParamete in sqlParametes)
                        cmd.Parameters.AddWithValue(sqlParamete.Name, sqlParamete.Value);
                numberCompletedRequests = await cmd.ExecuteNonQueryAsync();
            }
            await connection.CloseAsync();
            return numberCompletedRequests;
        }
    }
}
