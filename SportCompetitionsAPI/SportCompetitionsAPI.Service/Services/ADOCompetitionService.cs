using SportCompetitionsAPI.Domain.Entities;
using SportCompetitionsAPI.Service.Abstractions;
using SportCompetitionsAPI.Service.Exceptions;
using SportCompetitionsAPI.Service.Functions;
using System.Data;
using System.Data.SqlClient;
using SportCompetitionsAPI.Service.ADO;

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
        private ISqlQueries sqlQueries;

        /// <summary>
        /// Сервис для работы с соревнованиями. Использует ADO.NET
        /// </summary>
        /// <param name="sqlQueries">Запросы к базе данных</param>
        public ADOCompetitionService(ISqlQueries sqlQueries)
        {
            this.sqlQueries = sqlQueries;
        }

        public async Task Create(string name, DateTime date, Guid sportId)
        {
            string query = @$"
                INSERT INTO [Competition] ([Name], [Date], [SportId])
                VALUES (@name, @date, @sportId)
            ";
            var parameters = new List<SqlValues>()
            {
                new SqlValues { Name = "@name", Value = name },
                new SqlValues { Name = "@date", Value = date },
                new SqlValues { Name = "@sportId", Value = sportId },
            };
            try
            {
                await sqlQueries.QueryChangesAsync(query, parameters);
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
                WHERE [Id] = @id
            ";
            var parameters = new List<SqlValues>()
            {
                new SqlValues { Name = "@id", Value = id },
            };
            int count = await sqlQueries.QueryChangesAsync(query, parameters);
            if (count == 0) throw new CompetitionNotFoundException();
        }

        public async Task IncludePersonInCompetitions(Guid competitionId, Guid personId, bool isInclude)
        {
            string querySearchCompetition = @$"
                SELECT [Id]
                FROM [Competition]
                WHERE [Competition].[Id] = @competitionId
            ";
            var parametersSearchCompetition = new List<SqlValues>()
            {
                new SqlValues { Name = "@competitionId", Value = competitionId! },
            };
            var dataTableCompetition = await sqlQueries.QuerySelectAsync(querySearchCompetition, parametersSearchCompetition);
            if (dataTableCompetition.Rows.Count == 0) throw new CompetitionNotFoundException();
            string querySearchPerson = @$"
                SELECT [Id]
                FROM [Person]
                WHERE [Person].[Id] = @personId
            ";
            var parametersSearchPerson = new List<SqlValues>()
            {
                new SqlValues { Name = "@personId", Value = personId! },
            };
            var dataTablePerson = await sqlQueries.QuerySelectAsync(querySearchPerson, parametersSearchPerson);
            if (dataTablePerson.Rows.Count == 0)
                throw new PersonNotFoundException();

            if (isInclude)
            {
                string query = @$"
                    INSERT INTO [PersonCompetition] ([CompetitionId], [PersonId])
                    VALUES (@competitionId, @personId)
                ";
                var parameters = new List<SqlValues>()
                {
                    new SqlValues { Name = "@competitionId", Value = competitionId },
                    new SqlValues { Name = "@personId", Value = personId },
                };
                try
                {
                    await sqlQueries.QueryChangesAsync(query, parameters);
                }
                catch (SqlException)
                {
                    throw new PersonAlreadyInCompetition();
                }
            }
            else
            {
                string query = @$"
                    DELETE FROM [PersonCompetition] 
                    WHERE [CompetitionId]=@competitionId AND [PersonId]=@personId
                ";
                var parameters = new List<SqlValues>()
                {
                    new SqlValues { Name = "@competitionId", Value = competitionId },
                    new SqlValues { Name = "@personId", Value = personId },
                };
                try
                {
                    await sqlQueries.QueryChangesAsync(query, parameters);
                }
                catch (SqlException)
                {
                    throw new PersonNoLongerInCompetition();
                }
            }
        }

        public async Task<IEnumerable<Competition>> Read(Guid? personId = null)
        {
            var persons = new List<Competition>();
            List<SqlValues> parameters = null!;
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
            if (personId is not null)
            {
                query = @$"
                    SELECT 
                        [Competition].[Id],
                        [Competition].[Name],
                        [Competition].[Date],
                        [Sport].[Id] AS [SportId],
	                    [Sport].[Name] AS [SportName]
                    FROM [PersonCompetition]
                    LEFT JOIN [Competition] ON 
	                    [PersonCompetition].[CompetitionId]=[Competition].[Id]
                    LEFT JOIN [Sport] ON 
	                    [Sport].[Id]=[Competition].[SportId]
                    WHERE [PersonCompetition].[PersonId] = @personId
                ";
                parameters = new List<SqlValues>()
                {
                    new SqlValues { Name = "@personId", Value = personId! }
                };
            }
            DataTable dataTable = await sqlQueries.QuerySelectAsync(query, parameters);
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
                WHERE [Competition].[Id] = @id
            ";
            var parameters = new List<SqlValues>()
            {
                new SqlValues { Name = "@id", Value = id! }
            };
            DataTable dataTable = await sqlQueries.QuerySelectAsync(query, parameters);
            if (dataTable.Rows.Count == 0) throw new CompetitionNotFoundException();
            var competition = GetCompetitionByRow(dataTable.Rows[0]);
            return competition;
        }

        public async Task Update(Guid id, string name, DateTime date, Guid sportId)
        {
            string query = @$"
                UPDATE [Competition] 
                SET [Name]=@name, [Date]=@date, [SportId]=@sportId
                WHERE [Id]=@id
            ";
            var parameters = new List<SqlValues>()
            {
                new SqlValues { Name = "@id", Value = id },
                new SqlValues { Name = "@name", Value = name },
                new SqlValues { Name = "@date", Value = date },
                new SqlValues { Name = "@sportId", Value = sportId },
            };
            try
            {
                int count = await sqlQueries.QueryChangesAsync(query, parameters);
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
