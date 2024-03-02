using SportCompetitionsAPI.Domain.Entities;
using SportCompetitionsAPI.Service.Abstractions;
using SportCompetitionsAPI.Service.ADO;
using SportCompetitionsAPI.Service.Exceptions;
using System.Data;

namespace SportCompetitionsAPI.Service.Services
{
    /// <summary>
    /// Сервис для работы с людьми. Использует ADO.NET
    /// </summary>
    public class ADOPersonService : IPersonService
    {
        /// <summary>
        /// Запросы к базе данных
        /// </summary>
        private ISqlQueries sqlQueries;

        /// <summary>
        /// Сервис для работы с людьми. Использует ADO.NET
        /// </summary>
        /// <param name="sqlQueries">Запросы к базе данных</param>
        public ADOPersonService(ISqlQueries sqlQueries)
        {
            this.sqlQueries = sqlQueries;
        }

        public async Task Create(string name, string email, DateTime dateOfBirth)
        {
            string query = @$"
                INSERT INTO [Person] ([Name], [Email], [DateOfBirth]) 
                VALUES (@name, @email, @dateOfBirth)
            ";
            var parameters = new List<SqlValues>()
            {
                new SqlValues { Name = "@name", Value = name },
                new SqlValues { Name = "@email", Value = email },
                new SqlValues { Name = "@dateOfBirth", Value = dateOfBirth },
            };
            await sqlQueries.QueryChangesAsync(query, parameters);
        }

        public async Task Delete(Guid id)
        {
            string query = @$"
                DELETE FROM [Person] 
                WHERE [Id] = @id     
            ";
            var parameters = new List<SqlValues>()
            {
                new SqlValues{ Name = "@id", Value = id },
            };
            int count = await sqlQueries.QueryChangesAsync(query, parameters);
            if (count == 0) throw new PersonNotFoundException();
        }

        public async Task<IEnumerable<Person>> Read(
            Guid? competitionId = null,
            bool isParticipatingInCompetition = true,
            bool isNotParticipatingInCompetition = false)
        {

            string query;
            var parameters = new List<SqlValues>()
            {
                new SqlValues { Name = "@competitionId", Value = competitionId! },
            };
            if (competitionId is null || (isParticipatingInCompetition && isNotParticipatingInCompetition))
            {
                parameters = null!;
                query = @$"
                    SELECT 
                        [Id],
                        [Name],
                        [Email],
                        [DateOfBirth]
                    FROM [Person]
                ";
            }
            else if (isParticipatingInCompetition)
                query = @$"
                    SELECT 
                        [Person].[Id] AS [Id],
                        [Person].[Name] AS [Name],
                        [Person].[Email] AS [Email],
                        [Person].[DateOfBirth] AS [DateOfBirth]
                    FROM [PersonCompetition]
                    LEFT JOIN [Person] ON 
	                    [PersonCompetition].[PersonId]=[Person].[Id]
                    WHERE [PersonCompetition].[CompetitionId] = @competitionId
                ";
            else if (isNotParticipatingInCompetition)
                query = @$"
                    SELECT 
                        [Person].[Id] AS [Id],
                        [Person].[Name] AS [Name],
                        [Person].[Email] AS [Email],
                        [Person].[DateOfBirth] AS [DateOfBirth]
                    FROM [Person]
                    EXCEPT
                    SELECT 
                        [Person].[Id] AS [Id],
                        [Person].[Name] AS [Name],
                        [Person].[Email] AS [Email],
                        [Person].[DateOfBirth] AS [DateOfBirth]
                    FROM [PersonCompetition]
                    LEFT JOIN [Person] ON 
	                    [PersonCompetition].[PersonId]=[Person].[Id]
                    WHERE [PersonCompetition].[CompetitionId] = @competitionId
                ";
            else
                return new List<Person>();
            DataTable dataTable = await sqlQueries.QuerySelectAsync(query, parameters);

            var persons = new List<Person>();
            foreach (DataRow row in dataTable.Rows)
            {
                var person = GetPersonByRow(row);
                persons.Add(person);
            }
            return persons;
        }

        public async Task<Person> ReadById(Guid id)
        {
            string query = @$"
                SELECT 
                    [Id],
                    [Name],
                    [Email],
                    [DateOfBirth]
                FROM [Person]
                WHERE [Id] = @id
            ";
            var parameters = new List<SqlValues>()
            {
                new SqlValues { Name = "@id", Value = id! },
            };
            DataTable dataTable = await sqlQueries.QuerySelectAsync(query, parameters);
            if (dataTable.Rows.Count == 0) throw new PersonNotFoundException();
            var person = GetPersonByRow(dataTable.Rows[0]);
            return person;
        }

        public async Task Update(Guid id, string name, string email, DateTime dateOfBirth)
        {
            string query = @$"
                UPDATE [Person] 
                SET [Name]=@name, [Email]=@email, [DateOfBirth]=@dateOfBirth
                WHERE [Id] =@id
            ";
            var parameters = new List<SqlValues>()
            {
                new SqlValues { Name = "@id", Value = id },
                new SqlValues { Name = "@name", Value = name },
                new SqlValues { Name = "@email", Value = email },
                new SqlValues { Name = "@dateOfBirth", Value = dateOfBirth },
            };
            int count = await sqlQueries.QueryChangesAsync(query, parameters);
            if (count == 0) throw new PersonNotFoundException();
        }

        /// <summary>
        /// Получить человека по ряду
        /// </summary>
        /// <param name="row">Ряд</param>
        /// <returns>Человек</returns>
        private Person GetPersonByRow(DataRow row) => new Person
        {
            Id = row.Field<Guid>("Id"),
            Name = row.Field<string>("Name") ?? "",
            Email = row.Field<string>("Email") ?? "",
            DateOfBirth = row.Field<DateTime>("DateOfBirth"),
        };
    }
}
