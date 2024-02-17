namespace SportCompetitionsAPI.Domain.Entities
{
    /// <summary>
    /// Вид спорта
    /// </summary>
    public class Sport
    {
        /// <summary>
        /// Уникальный Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = "";
    }
}
