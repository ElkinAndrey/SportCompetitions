namespace SportCompetitionsAPI.Controllers.Dto.Competition
{
    /// <summary>
    /// Данные для добавления человека на соревнования
    /// </summary>
    /// <param name="PersonId">Id человека</param>
    public sealed record class AddPersonToCompetitionDto(
        Guid PersonId);
}
