namespace SportCompetitionsAPI.Domain.Entities
{
    /// <summary>
    /// Человек
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Уникальный Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Фамилия имя и отчество
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; } = "";

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Соревнования
        /// </summary>
        public List<Competition> Competitions = new List<Competition>();
    }
}
