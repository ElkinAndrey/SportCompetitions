using SportCompetitionsAPI.Domain.Entities;

namespace SportCompetitionsAPI.Service.Abstractions
{
    /// <summary>
    /// Сервис для работы с соревнованиями
    /// </summary>
    public interface ICompetitionService
    {
        /// <summary>
        /// Создать соревнование
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="date">Дата проведения</param>
        /// <param name="sportId">Id вида спорта</param>
        Task Create(string name, DateTime date, Guid sportId);

        /// <summary>
        /// Получить список соревнований
        /// </summary>
        /// <param name="personId">Id человека</param>
        Task<IEnumerable<Competition>> Read(Guid? personId = null);

        /// <summary>
        /// Получить соревнование по id
        /// </summary>
        /// <param name="id">Id соревнования</param>
        Task<Competition> ReadById(Guid id);

        /// <summary>
        /// Изменить соревнование
        /// </summary>
        /// <param name="id">Id соревнования</param>
        /// <param name="name">Название</param>
        /// <param name="date">Дата проведения</param>
        /// <param name="sportId">Id вида спорта</param>
        Task Update(Guid id, string name, DateTime date, Guid sportId);

        /// <summary>
        /// Удалить соревнование
        /// </summary>
        /// <param name="id">Id соревнования</param>
        Task Delete(Guid id);

        /// <summary>
        /// Добавить человека на соревнования
        /// </summary>
        /// <param name="competitionId">Id соревнования</param>
        /// <param name="personId">Id Человека</param>
        /// <param name="isInclude">Включать ли (true - включить, false - исключить)</param>
        Task IncludePersonInCompetitions(Guid competitionId, Guid personId, bool isInclude);
    }
}
