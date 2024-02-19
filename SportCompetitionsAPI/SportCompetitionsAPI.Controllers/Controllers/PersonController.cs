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
        /// Сервис для работы с соревнованиями
        /// </summary>
        private ICompetitionService competitionService;

        /// <summary>
        /// Контроллер для работы с видами спорта
        /// </summary>
        /// <param name="personService">Сервис для работы с видами спорта</param>
        /// <param name="competitionService">Сервис для работы с соревнованиями</param>
        public PersonController(IPersonService personService, ICompetitionService competitionService)
        {
            this.personService = personService;
            this.competitionService = competitionService;
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
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
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
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
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
            var competitions = await competitionService.Read(id);
            var response = competitions.Select(competition => new
            {
                Id = competition.Id,
                Name = competition.Name,
                Date = competition.Date,
                Sport = new
                {
                    Id = competition.Sport.Id,
                    Name = competition.Sport.Name,
                }
            });
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
