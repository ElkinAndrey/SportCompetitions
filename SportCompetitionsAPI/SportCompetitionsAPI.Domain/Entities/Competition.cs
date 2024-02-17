namespace SportCompetitionsAPI.Domain.Entities
{
    /// <summary>
    /// Соревнование
    /// </summary>
    public class Competition
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
        /// Дата проведения
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Вид спорта
        /// </summary>
        public Sport Sport { get; set; } = null!;

        /// <summary>
        /// Участники соревнования
        /// </summary>
        public List<Person> Persons = new List<Person>();
    }
}
