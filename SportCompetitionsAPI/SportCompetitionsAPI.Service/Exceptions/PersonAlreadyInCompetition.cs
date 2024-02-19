namespace SportCompetitionsAPI.Service.Exceptions
{
    /// <summary>
    /// Ошибка. Человек уже на соревнованиях
    /// </summary>
    public sealed class PersonAlreadyInCompetition : Exception
    {
        /// <summary>
        /// Ошибка. Человек уже на соревнованиях
        /// </summary>
        public PersonAlreadyInCompetition() :
            base($"Человек уже на соревнованиях")
        {

        }
    }
}
