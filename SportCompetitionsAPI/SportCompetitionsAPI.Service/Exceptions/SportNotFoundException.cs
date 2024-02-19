namespace SportCompetitionsAPI.Service.Exceptions
{
    /// <summary>
    /// Ошибка. Вид спорта не найден
    /// </summary>
    public sealed class SportNotFoundException : Exception
    {
        /// <summary>
        /// Ошибка. Вид спорта не найден
        /// </summary>
        public SportNotFoundException() :
            base($"Вид спорта не найден")
        {

        }

        /// <summary>
        /// Ошибка. Вид спорта не найден
        /// </summary>
        /// <param name="id">Id вида спорта</param>
        public SportNotFoundException(Guid id) :
            base($"Вид спорта с id \"{id}\" не найден")
        {

        }
    }
}
