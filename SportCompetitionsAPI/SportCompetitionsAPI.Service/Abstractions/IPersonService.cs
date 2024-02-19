using SportCompetitionsAPI.Domain.Entities;

namespace SportCompetitionsAPI.Service.Abstractions
{
    /// <summary>
    /// Сервис для работы с людьми
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Создать человека
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="email">Электронная почта</param>
        /// <param name="dateOfBirth">Дата рождения</param>
        Task Create(string name, string email, DateTime dateOfBirth);

        /// <summary>
        /// Получить список людей
        /// </summary>
        Task<IEnumerable<Person>> Read(
            Guid? competitionId = null,
            bool isParticipatingInCompetition = true,
            bool isNotParticipatingInCompetition = false);

        /// <summary>
        /// Получить человека по id
        /// </summary>
        /// <param name="id">Id человека</param>
        Task<Person> ReadById(Guid id);

        /// <summary>
        /// Изменить человека
        /// </summary>
        /// <param name="id">Id человека</param>
        /// <param name="name">Имя</param>
        /// <param name="email">Электронная почта</param>
        /// <param name="dateOfBirth">Дата рождения</param>
        Task Update(Guid id, string name, string email, DateTime dateOfBirth);

        /// <summary>
        /// Удалить человека
        /// </summary>
        /// <param name="id">Id человека</param>
        Task Delete(Guid id);
    }
}
