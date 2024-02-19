using SportCompetitionsAPI.Domain.Entities;
using SportCompetitionsAPI.Service.Abstractions;
using SportCompetitionsAPI.Service.Exceptions;
using SportCompetitionsAPI.Service.Functions;
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
                VALUES (N'{name}', N'{email}', '{SqlConvert.Date(dateOfBirth)}')
            ";
            await sqlQueries.QueryChangesAsync(query);
        }

        public async Task Delete(Guid id)
        {
            string query = @$"
                DELETE FROM [Person] 
                WHERE [Id] = '{id}'     
            ";
            int count = await sqlQueries.QueryChangesAsync(query);
            if (count == 0) throw new PersonNotFoundException();
        }

        public async Task<IEnumerable<Person>> Read(
            Guid? competitionId = null,
            bool isParticipatingInCompetition = true,
            bool isNotParticipatingInCompetition = false)
        {
            string query;
            if (competitionId is null || (isParticipatingInCompetition && isNotParticipatingInCompetition))
                query = @$"
                    SELECT 
                        [Id],
                        [Name],
                        [Email],
                        [DateOfBirth]
                    FROM [Person]
                ";
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
                    WHERE [PersonCompetition].[CompetitionId] = '{competitionId}'
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
                    WHERE [PersonCompetition].[CompetitionId] = '{competitionId}'
                ";
            else
                return new List<Person>();

            DataTable dataTable = await sqlQueries.QuerySelectAsync(query);
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
                WHERE [Id] = '{id}'
            ";
            DataTable dataTable = await sqlQueries.QuerySelectAsync(query);
            if (dataTable.Rows.Count == 0) throw new PersonNotFoundException();
            var person = GetPersonByRow(dataTable.Rows[0]);
            return person;
        }

        public async Task Update(Guid id, string name, string email, DateTime dateOfBirth)
        {
            string query = @$"
                UPDATE [Person] 
                SET [Name]=N'{name}', [Email]=N'{email}', [DateOfBirth]='{SqlConvert.Date(dateOfBirth)}'
                WHERE [Id] = '{id}'
            ";
            int count = await sqlQueries.QueryChangesAsync(query);
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
