namespace SportCompetitionsAPI.Controllers.Dto.Competition
{
    /// <summary>
    /// Данные для создания соревнования
    /// </summary>
    /// <param name="Date">Дата проведения</param>
    /// <param name="SportId">Id вида спорта</param>
    /// <param name="Name">Название</param>
    public sealed record class CreateCompetitionDto(
        DateTime Date,
        Guid SportId,
        string Name = "");
}
