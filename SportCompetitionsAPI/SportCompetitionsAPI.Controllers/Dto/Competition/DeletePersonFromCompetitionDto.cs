namespace SportCompetitionsAPI.Controllers.Dto.Competition
{
    /// <summary>
    /// Данные для удаления человека с соревнований
    /// </summary>
    /// <param name="PersonId">Id человека</param>
    public sealed record class DeletePersonFromCompetitionDto(
        Guid PersonId);
}
