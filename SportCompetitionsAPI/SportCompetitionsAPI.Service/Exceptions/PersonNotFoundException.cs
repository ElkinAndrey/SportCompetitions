namespace SportCompetitionsAPI.Service.Exceptions
{
    /// <summary>
    /// Ошибка. Человек не найден
    /// </summary>
    public sealed class PersonNotFoundException : Exception
    {
        /// <summary>
        /// Ошибка. Вид спорта не найден
        /// </summary>
        public PersonNotFoundException() :
            base($"Человек не найден")
        {

        }

        /// <summary>
        /// Ошибка. Вид спорта не найден
        /// </summary>
        /// <param name="id">Id человека</param>
        public PersonNotFoundException(Guid id) :
            base($"Человек с id \"{id}\" не найден")
        {

        }
    }
}
