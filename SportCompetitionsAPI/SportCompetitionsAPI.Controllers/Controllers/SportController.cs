using Microsoft.AspNetCore.Mvc;
using SportCompetitionsAPI.Controllers.Dto.Sport;
using SportCompetitionsAPI.Service.Abstractions;

namespace SportCompetitionsAPI.Controllers.Controllers
{
    /// <summary>
    /// Контроллер для работы с видами спорта
    /// </summary>
    [Route("api/sports")]
    [ApiController]
    public class SportController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с видами спорта
        /// </summary>
        private ISportService sportService;

        /// <summary>
        /// Контроллер для работы с видами спорта
        /// </summary>
        /// <param name="sportService">Сервис для работы с видами спорта</param>
        public SportController(ISportService sportService)
        {
            this.sportService = sportService;
        }

        /// <summary>
        /// Создать спорт
        /// </summary>
        /// <param name="request">Данные для создания</param>
        [HttpPost("")]
        public async Task<IActionResult> Create(CreateSportDto request)
        {
            await sportService.Create(request.Name, request.Description);
            return Ok();
        }

        /// <summary>
        /// Получить список видов спорта
        /// </summary>
        [HttpGet("")]
        public async Task<IActionResult> Read()
        {
            var sports = await sportService.Read();
            var response = sports.Select(sport => new
            {
                Id = sport.Id,
                Name = sport.Name,
                Description = sport.Description,
            });
            return Ok(response);
        }

        /// <summary>
        /// Изменить спорт
        /// </summary>
        /// <param name="id">Id спорта</param>
        /// <param name="request">Данные для изменения</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateSportDto request)
        {
            await sportService.Update(id, request.Name, request.Description);
            return Ok();
        }

        /// <summary>
        /// Удалить спорта
        /// </summary>
        /// <param name="id">Id спорта</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await sportService.Delete(id);
            return Ok();
        }
    }
}
