namespace SportCompetitionsAPI.Controllers.Dto.Sport
{
    /// <summary>
    /// Данные для создания вида спорта
    /// </summary>
    /// <param name="name">Назване</param>
    /// <param name="description">Описание</param>
    public sealed record class CreateSportDto(
        string name = "",
        string description = "");
}
