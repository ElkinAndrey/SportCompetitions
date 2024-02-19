using SportCompetitionsAPI.Domain.Entities;

namespace SportCompetitionsAPI.Service.Abstractions
{
    /// <summary>
    /// Сервис для работы с видами спорта
    /// </summary>
    public interface ISportService
    {
        /// <summary>
        /// Создать вид спорта
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="description">Описание</param>
        Task Create(string name, string description);

        /// <summary>
        /// Изменить вид спорта
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">Название</param>
        /// <param name="description">Описание</param>
        Task Update(Guid id, string name, string description);

        /// <summary>
        /// Удалить вид спорта
        /// </summary>
        /// <param name="id">Id</param>
        Task Delete(Guid id);

        /// <summary>
        /// Получить список видов спорта
        /// </summary>
        /// <returns>Список видов спорта</returns>
        Task<IEnumerable<Sport>> Read();
    }
}
