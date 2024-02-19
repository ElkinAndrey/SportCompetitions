namespace SportCompetitionsAPI.Controllers.Dto.Sport
{
    /// <summary>
    /// Данные для изменения вида спорта
    /// </summary>
    /// <param name="Name">Назване</param>
    /// <param name="Description">Описание</param>
    public sealed record class UpdateSportDto(
        string Name = "",
        string Description = "");
}
