using SportCompetitionsAPI.Service.Functions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SportCompetitionsAPI.Test
{
    public class SqlConvertTests
    {
        /// <summary>
        /// Проверка получения строки из даты
        /// </summary>
        [Fact]
        public void TestDate()
        {

            // Подготовка
            var datetime = new DateTime(2024, 12, 29, 3, 56, 1);

            // Действие
            string datestring = SqlConvert.Date(datetime);

            // Утверждение
            Assert.True(datestring == $"12.29.2024 3:56:1");
        }
    }
}
