using SportCompetitionsAPI.Domain.Entities;
using SportCompetitionsAPI.Service.Abstractions;
using SportCompetitionsAPI.Service.Exceptions;
using SportCompetitionsAPI.Service.Functions;
using System.Data;
using System.Data.SqlClient;

namespace SportCompetitionsAPI.Service.Services
{
    /// <summary>
    /// Сервис для работы с соревнованиями. Использует ADO.NET
    /// </summary>
    public class ADOCompetitionService : ICompetitionService
    {
        /// <summary>
        /// Запросы к базе данных
        /// </summary>
        private SqlQueries sqlQueries;

        /// <summary>
        /// Сервис для работы с соревнованиями. Использует ADO.NET
        /// </summary>
        /// <param name="sqlQueries">Запросы к базе данных</param>
        public ADOCompetitionService(SqlQueries sqlQueries)
        {
            this.sqlQueries = sqlQueries;
        }

        public async Task Create(string name, DateTime date, Guid sportId)
        {
            string query = @$"
                INSERT INTO [Competition] ([Name], [Date], [SportId])
                VALUES (N'{name}', '{SqlConvert.Date(date)}', '{sportId}')
            ";
            try
            {
                await sqlQueries.QueryChangesAsync(query);
            }
            catch (SqlException)
            {
                throw new SportNotFoundException();
            }
        }

        public async Task Delete(Guid id)
        {
            string query = @$"
                DELETE FROM [Competition] 
                WHERE [Id] = '{id}'
            ";
            int count = await sqlQueries.QueryChangesAsync(query);
            if (count == 0) throw new CompetitionNotFoundException();
        }

        public async Task IncludePersonInCompetitions(Guid competitionId, Guid personId, bool isInclude)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Competition>> Read(Guid? personId = null)
        {
            var persons = new List<Competition>();
            string query = @$"
                SELECT 
                    [Competition].[Id],
                    [Competition].[Name],
                    [Competition].[Date],
                    [Sport].[Id] AS [SportId],
		            [Sport].[Name] AS [SportName]
                FROM [Competition]
	            LEFT JOIN [Sport] ON 
		            [Sport].[Id]=[Competition].[SportId]
            ";
            DataTable dataTable = await sqlQueries.QuerySelectAsync(query);
            foreach (DataRow row in dataTable.Rows)
            {
                var person = GetCompetitionByRow(row);
                persons.Add(person);
            }
            return persons;
        }

        public async Task<Competition> ReadById(Guid id)
        {
            string query = @$"
                SELECT 
                    [Competition].[Id],
                    [Competition].[Name],
                    [Competition].[Date],
                    [Sport].[Id] AS [SportId],
		            [Sport].[Name] AS [SportName]
                FROM [Competition]
	            LEFT JOIN [Sport] ON 
		            [Sport].[Id]=[Competition].[SportId]
                WHERE [Competition].[Id] = '{id}'
            ";
            DataTable dataTable = await sqlQueries.QuerySelectAsync(query);
            if (dataTable.Rows.Count == 0) throw new CompetitionNotFoundException();
            var competition = GetCompetitionByRow(dataTable.Rows[0]);
            return competition;
        }

        public async Task Update(Guid id, string name, DateTime date, Guid sportId)
        {
            string query = @$"
                UPDATE [Competition] 
                SET [Name]=N'{name}', [Date]='{SqlConvert.Date(date)}', [SportId]='{sportId}'
                WHERE [Id] = '{id}'
            ";
            try
            {
                int count = await sqlQueries.QueryChangesAsync(query);
                if (count == 0) throw new CompetitionNotFoundException();
            }
            catch (SqlException)
            {
                throw new SportNotFoundException();
            }
        }

        /// <summary>
        /// Получить человека по ряду
        /// </summary>
        /// <param name="row">Ряд</param>
        /// <returns>Человек</returns>
        private Competition GetCompetitionByRow(DataRow row) => new Competition
        {
            Id = row.Field<Guid>("Id"),
            Name = row.Field<string>("Name") ?? "",
            Date = row.Field<DateTime>("Date"),
            Sport = new Sport
            {
                Id = row.Field<Guid>("SportId"),
                Name = row.Field<string>("SportName") ?? "", 
            }
        };
    }
}
