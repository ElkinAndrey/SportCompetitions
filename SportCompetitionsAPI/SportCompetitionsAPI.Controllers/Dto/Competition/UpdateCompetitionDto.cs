namespace SportCompetitionsAPI.Controllers.Dto.Competition
{
    /// <summary>
    /// Данные для изменения соревнования
    /// </summary>
    /// <param name="Date">Дата проведения</param>
    /// <param name="SportId">Id вида спорта</param>
    /// <param name="Name">Название</param>
    public sealed record class UpdateCompetitionDto(
        DateTime Date,
        Guid SportId,
        string Name = "");
}
