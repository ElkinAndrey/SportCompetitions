using SportCompetitionsAPI.Domain.Entities;
using SportCompetitionsAPI.Service.Abstractions;
using SportCompetitionsAPI.Service.Exceptions;
using System.Data;

namespace SportCompetitionsAPI.Service.Services
{
    /// <summary>
    /// Сервис для работы с видами спорта. Использует ADO.NET
    /// </summary>
    public class ADOSportService : ISportService
    {
        /// <summary>
        /// Запросы к базе данных
        /// </summary>
        private ISqlQueries sqlQueries;

        /// <summary>
        /// Сервис для работы с видами спорта. Использует ADO.NET
        /// </summary>
        /// <param name="sqlQueries">Запросы к базе данных</param>
        public ADOSportService(ISqlQueries sqlQueries) 
        { 
            this.sqlQueries = sqlQueries;
        }

        public async Task Create(string name, string description)
        {
            string query = @$"
                INSERT INTO [Sport] ([Name], [Description]) 
                VALUES (N'{name}', N'{description}')
            ";
            await sqlQueries.QueryChangesAsync(query);
        }

        public async Task Delete(Guid id)
        {
            string query = @$"
                DELETE FROM [Sport] 
                WHERE [Id] = '{id}'     
            ";
            int count = await sqlQueries.QueryChangesAsync(query);
            if (count == 0) throw new SportNotFoundException();
        }

        public async Task<IEnumerable<Sport>> Read()
        {
            var sports = new List<Sport>();
            string query = @$"
                SELECT 
                    [Id],
                    [Name],
                    [Description]
                FROM [Sport]
            ";
            DataTable dataTable = await sqlQueries.QuerySelectAsync(query);
            foreach (DataRow row in dataTable.Rows)
            {
                var sport = GetSportByRow(row);
                sports.Add(sport);
            }
            return sports;
        }

        public async Task Update(Guid id, string name, string description)
        {
            string query = @$"
                UPDATE [Sport] 
                SET [Name]=N'{name}', [Description]=N'{description}'
                WHERE [Id] = '{id}'
            ";
            int count = await sqlQueries.QueryChangesAsync(query);
            if (count == 0) throw new SportNotFoundException();
        }

        /// <summary>
        /// Получить вид спорта по ряду
        /// </summary>
        /// <param name="row">Ряд</param>
        /// <returns>Вид спорта</returns>
        private Sport GetSportByRow(DataRow row) => new Sport
        {
            Id = row.Field<Guid>("Id"),
            Name = row.Field<string>("Name") ?? "",
            Description = row.Field<string>("Description") ?? "",
        };
    }
}
