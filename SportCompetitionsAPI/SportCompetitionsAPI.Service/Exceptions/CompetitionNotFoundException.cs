namespace SportCompetitionsAPI.Service.Exceptions
{
    /// <summary>
    /// Ошибка. Соревнование не найдено
    /// </summary>
    public sealed class CompetitionNotFoundException : Exception
    {
        /// <summary>
        /// Ошибка. Соревнование не найдено
        /// </summary>
        public CompetitionNotFoundException() :
            base($"Соревнование не найдено")
        {

        }

        /// <summary>
        /// Ошибка. Соревнование не найдено
        /// </summary>
        /// <param name="id">Id соревнования</param>
        public CompetitionNotFoundException(Guid id) :
            base($"Соревнование с id \"{id}\" не найдено")
        {

        }
    }
}
