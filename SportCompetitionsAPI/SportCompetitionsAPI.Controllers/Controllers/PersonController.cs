using Microsoft.AspNetCore.Mvc;
using SportCompetitionsAPI.Controllers.Dto.Person;
using SportCompetitionsAPI.Domain.Entities;
using SportCompetitionsAPI.Service.Abstractions;

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
        /// Сервис для работы с видами спорта
        /// </summary>
        private IPersonService personService;

        /// <summary>
        /// Контроллер для работы с видами спорта
        /// </summary>
        /// <param name="personService">Сервис для работы с видами спорта</param>
        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }

        /// <summary>
        /// Создать человека
        /// </summary>
        /// <param name="request">Данные для создания</param>
        [HttpPost("")]
        public async Task<IActionResult> Create(CreatePersonDto request)
        {
            await personService.Create(request.Name, request.Email, request.DateOfBirth);
            return Ok();
        }

        /// <summary>
        /// Получить список людей
        /// </summary>
        [HttpGet("")]
        public async Task<IActionResult> Read()
        {
            var persons = await personService.Read();
            var response = persons.Select(person => new
            {
                person.Id,
                person.Name,
                person.Email,
                person.DateOfBirth,
            });
            return Ok(response);
        }

        /// <summary>
        /// Получить человека по id
        /// </summary>
        /// <param name="id">Id человека</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> ReadById(Guid id)
        {
            var person = await personService.ReadById(id);
            var response = new
            {
                person.Id,
                person.Name,
                person.Email,
                person.DateOfBirth,
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
            await personService.Update(id, request.Name, request.Email, request.DateOfBirth);
            return Ok();
        }

        /// <summary>
        /// Удалить человека
        /// </summary>
        /// <param name="id">Id человека</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await personService.Delete(id);
            return Ok();
        }
    }
}
