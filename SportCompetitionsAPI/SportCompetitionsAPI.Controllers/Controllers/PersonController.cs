using Microsoft.AspNetCore.Mvc;
using SportCompetitionsAPI.Controllers.Dto.Person;
using SportCompetitionsAPI.Domain.Entities;

namespace SportCompetitionsAPI.Controllers.Controllers
{
    /// <summary>
    /// Контроллер для работы с людьми
    /// </summary>
    [Route("api/persons")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        /// <summary>
        /// Создать человека
        /// </summary>
        /// <param name="request">Данные для создания</param>
        [HttpPost("")]
        public async Task<IActionResult> Create(CreatePersonDto request)
        {
            return Ok();
        }

        /// <summary>
        /// Получить список людей
        /// </summary>
        [HttpGet("")]
        public async Task<IActionResult> Read()
        {
            var response = new List<Person>()
            {
                new Person()
                {
                    Id = Guid.NewGuid(),
                    Name = "Дмитрий Дмитриевич Дмитриев",
                    Email = "dima@dima.dima",
                    DateOfBirth = new DateTime(2002, 5, 8, 0, 0, 0),
                },
                new Person()
                {
                    Id = Guid.NewGuid(),
                    Name = "Иван Иванович Иванов",
                    Email = "ivan@ivan.ivan",
                    DateOfBirth = new DateTime(2003, 6, 1, 0, 0, 0),
                },
                new Person()
                {
                    Id = Guid.NewGuid(),
                    Name = "Андрей Андреевич Андреев",
                    Email = "andrey@andrey.andrey",
                    DateOfBirth = new DateTime(1999, 10, 24, 0, 0, 0),
                },
            };

            return Ok(response);
        }

        /// <summary>
        /// Получить человека по id
        /// </summary>
        /// <param name="id">Id человека</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> ReadById(Guid id)
        {
            var response = new Person()
            {
                Id = Guid.NewGuid(),
                Name = "Дмитрий Дмитриевич Дмитриев",
                Email = "dima@dima.dima",
                DateOfBirth = new DateTime(2002, 5, 8, 0, 0, 0),
            };

            return Ok(response);
        }

        /// <summary>
        /// Получить соревнования по id человека
        /// </summary>
        /// <param name="id">Id человека</param>
        [HttpGet("{id}/competition")]
        public async Task<IActionResult> ReadCompetitionById(Guid id)
        {
            var response = new List<Competition>()
            {
                new Competition()
                {
                    Id = Guid.NewGuid(),
                    Name = "Открытый турнир по Брейкингу 1х1 среди новичков и продолжающих",
                    Date = new DateTime(2024, 2, 16, 20, 30, 0),
                    Sport = new Sport() { Id = Guid.NewGuid(), Name = "Волейбол", Description = "" },
                },
                new Competition()
                {
                    Id = Guid.NewGuid(),
                    Name = "Матч по гандболу \"Пермские медведи\" - \"Зенит\"",
                    Date = new DateTime(2024, 2, 18, 20, 0, 0),
                    Sport = new Sport() { Id = Guid.NewGuid(), Name = "Футбол", Description = "" },
                },
                new Competition()
                {
                    Id = Guid.NewGuid(),
                    Name = "Спортивные мероприятия на катке у \"Театра-Театра\"",
                    Date = new DateTime(2024, 2, 20, 16, 0, 0),
                    Sport = new Sport() { Id = Guid.NewGuid(), Name = "Баскетбол", Description = "" },
                },
            };

            return Ok(response);
        }

        /// <summary>
        /// Изменить человека
        /// </summary>
        /// <param name="id">Id человека</param>
        /// <param name="request">Данные для изменения</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePersonDto request)
        {
            return Ok();
        }

        /// <summary>
        /// Удалить человека
        /// </summary>
        /// <param name="id">Id человека</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok();
        }
    }
}
