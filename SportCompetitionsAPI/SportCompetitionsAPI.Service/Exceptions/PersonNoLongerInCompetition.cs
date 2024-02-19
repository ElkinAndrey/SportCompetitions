namespace SportCompetitionsAPI.Service.Exceptions
{
    /// <summary>
    /// Ошибка. Человек уже не на соревнованиях
    /// </summary>
    public sealed class PersonNoLongerInCompetition : Exception
    {
        /// <summary>
        /// Ошибка. Человек уже не на соревнованиях
        /// </summary>
        public PersonNoLongerInCompetition() :
            base($"Человек уже не на соревнованиях")
        {

        }
    }
}
