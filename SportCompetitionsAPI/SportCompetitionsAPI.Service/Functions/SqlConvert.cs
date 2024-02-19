namespace SportCompetitionsAPI.Service.Functions
{
    /// <summary>
    /// Преобразователи для SQL (02.19.2024 12.44.22)
    /// </summary>
    public static class SqlConvert
    {
        /// <summary>
        /// Преобразовать дату в строку
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns>Дата в виде строки</returns>
        public static string Date(DateTime date) 
            => $"{date.Month}.{date.Day}.{date.Year} {date.Hour}:{date.Minute}:{date.Second}";
    }
}
