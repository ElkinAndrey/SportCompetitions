using Microsoft.AspNetCore.Mvc;
using SportCompetitionsAPI.Controllers.Dto.Competition;
using SportCompetitionsAPI.Domain.Entities;

namespace SportCompetitionsAPI.Controllers.Controllers
{
    /// <summary>
    /// Контроллер для работы с соревнованиями
    /// </summary>
    [Route("api/competitions")]
    [ApiController]
    public class CompetitionController : ControllerBase
    {
        /// <summary>
        /// Создать соревнование
        /// </summary>
        /// <param name="request">Данные для создания</param>
        [HttpPost("")]
        public async Task<IActionResult> Create(CreateCompetitionDto request)
        {
            return Ok();
        }

        /// <summary>
        /// Получить список соревнований
        /// </summary>
        [HttpGet("")]
        public async Task<IActionResult> Read()
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
        /// Получить соревнование по id
        /// </summary>
        /// <param name="id">Id соревнования</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> ReadById(Guid id)
        {
            var response = new Competition()
            {
                Id = Guid.NewGuid(),
                Name = "Открытый турнир по Брейкингу 1х1 среди новичков и продолжающих",
                Date = new DateTime(2024, 2, 16, 20, 30, 0),
                Sport = new Sport() { Id = Guid.NewGuid(), Name = "Волейбол", Description = "" },
            };

            return Ok(response);
    }

        /// <summary>
        /// Получить людей по id соревнования
        /// </summary>
        /// <param name="id">Id соревнования</param>
        [HttpGet("{id}/persons")]
        public async Task<IActionResult> ReadPersonsById(Guid id)
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
        /// Изменить соревнование
        /// </summary>
        /// <param name="id">Id соревнования</param>
        /// <param name="request">Данные для изменения</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCompetitionDto request)
        {
            return Ok();
        }

        /// <summary>
        /// Удалить соревнование
        /// </summary>
        /// <param name="id">Id соревнования</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok();
        }

        /// <summary>
        /// Добавить человека на соревнования
        /// </summary>
        /// <param name="id">Id соревнования</param>
        [HttpPut("{id}/person/add")]
        public async Task<IActionResult> AddPerson(Guid id, AddPersonToCompetitionDto request)
        {
            return Ok();
        }

        /// <summary>
        /// Удалить человека с соревнований
        /// </summary>
        /// <param name="id">Id соревнования</param>
        [HttpPut("{id}/person/delete")]
        public async Task<IActionResult> DeletePerson(Guid id, DeletePersonFromCompetitionDto request)
        {
            return Ok();
        }
    }
}
