namespace SportCompetitionsAPI.Controllers.Dto.Sport
{
    /// <summary>
    /// Данные для изменения вида спорта
    /// </summary>
    /// <param name="name">Назване</param>
    /// <param name="description">Описание</param>
    public sealed record class UpdateSportDto(
        string name = "",
        string description = "");
}
