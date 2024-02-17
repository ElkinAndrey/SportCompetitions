using Microsoft.AspNetCore.Mvc;
using SportCompetitionsAPI.Controllers.Dto.Sport;
using SportCompetitionsAPI.Domain.Entities;

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
        /// Создать спорт
        /// </summary>
        /// <param name="request">Данные для создания</param>
        [HttpPost("")]
        public async Task<IActionResult> Create(CreateSportDto request)
        {
            return Ok();
        }

        /// <summary>
        /// Получить список видов спорта
        /// </summary>
        [HttpGet("")]
        public async Task<IActionResult> Read()
        {
            var response = new List<Sport>()
            {
                new Sport() { Id = Guid.NewGuid(), Name = "Волейбол", Description = "" },
                new Sport() { Id = Guid.NewGuid(), Name = "Футбол", Description = "" },
                new Sport() { Id = Guid.NewGuid(), Name = "Баскетбол", Description = "" },
            };

            return Ok(response);
        }

        /// <summary>
        /// Получить вид спорта
        /// </summary>
        /// <param name="id">Id спорта</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> ReadById(Guid id)
        {
            var response = new Sport() { Id = Guid.NewGuid(), Name = "Волейбол", Description = "" };

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
            return Ok();
        }

        /// <summary>
        /// Удалить спорта
        /// </summary>
        /// <param name="id">Id спорта</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok();
        }
    }
}
