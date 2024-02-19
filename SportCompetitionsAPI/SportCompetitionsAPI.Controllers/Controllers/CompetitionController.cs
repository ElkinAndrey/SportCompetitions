using Microsoft.AspNetCore.Mvc;
using SportCompetitionsAPI.Controllers.Dto.Competition;
using SportCompetitionsAPI.Domain.Entities;
using SportCompetitionsAPI.Service.Abstractions;

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
        /// Сервис для работы с видами спорта
        /// </summary>
        private IPersonService personService;

        /// <summary>
        /// Сервис для работы с соревнованиями
        /// </summary>
        private ICompetitionService competitionService;

        /// <summary>
        /// Контроллер для работы с соревнованиями
        /// </summary>
        /// <param name="personService">Сервис для работы с видами спорта</param>
        /// <param name="competitionService">Сервис для работы с соревнованиями</param>
        public CompetitionController(IPersonService personService, ICompetitionService competitionService)
        {
            this.personService = personService;
            this.competitionService = competitionService;
        }

        /// <summary>
        /// Создать соревнование
        /// </summary>
        /// <param name="request">Данные для создания</param>
        [HttpPost("")]
        public async Task<IActionResult> Create(CreateCompetitionDto request)
        {
            await competitionService.Create(request.Name, request.Date, request.SportId);
            return Ok();
        }

        /// <summary>
        /// Получить список соревнований
        /// </summary>
        [HttpGet("")]
        public async Task<IActionResult> Read()
        {
            var competitions = await competitionService.Read();
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
        /// Получить соревнование по id
        /// </summary>
        /// <param name="id">Id соревнования</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> ReadById(Guid id)
        {
            var competition = await competitionService.ReadById(id);
            var response = new
            {
                Id = competition.Id,
                Name = competition.Name,
                Date = competition.Date,
                Sport = new
                {
                    Id = competition.Sport.Id,
                    Name = competition.Sport.Name,
                }
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
            var persons = await personService.Read(id);
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
        /// Получить людей по id соревнования
        /// </summary>
        /// <param name="id">Id соревнования</param>
        [HttpGet("{id}/persons/not")]
        public async Task<IActionResult> ReadNotParticipatingPersonsById(Guid id)
        {
            var persons = await personService.Read(id, false, true);
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
        /// Изменить соревнование
        /// </summary>
        /// <param name="id">Id соревнования</param>
        /// <param name="request">Данные для изменения</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCompetitionDto request)
        {
            await competitionService.Update(id, request.Name, request.Date, request.SportId);
            return Ok();
        }

        /// <summary>
        /// Удалить соревнование
        /// </summary>
        /// <param name="id">Id соревнования</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await competitionService.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Добавить человека на соревнования
        /// </summary>
        /// <param name="id">Id соревнования</param>
        [HttpPut("{id}/person/add")]
        public async Task<IActionResult> AddPerson(Guid id, AddPersonToCompetitionDto request)
        {

            await competitionService.IncludePersonInCompetitions(id, request.PersonId, true);
            return Ok();
        }

        /// <summary>
        /// Удалить человека с соревнований
        /// </summary>
        /// <param name="id">Id соревнования</param>
        [HttpPut("{id}/person/delete")]
        public async Task<IActionResult> DeletePerson(Guid id, DeletePersonFromCompetitionDto request)
        {
            await competitionService.IncludePersonInCompetitions(id, request.PersonId, false);
            return Ok();
        }
    }
}
