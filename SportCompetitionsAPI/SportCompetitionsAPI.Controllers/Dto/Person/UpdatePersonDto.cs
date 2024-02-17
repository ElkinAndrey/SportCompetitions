namespace SportCompetitionsAPI.Controllers.Dto.Person
{
    /// <summary>
    /// Данные для изменения человека
    /// </summary>
    /// <param name="DateOfBirth">Дата рождения</param>
    /// <param name="Name">Имя</param>
    /// <param name="Email">Электронная почта</param>
    public sealed record class UpdatePersonDto(
        DateTime DateOfBirth,
        string Name = "",
        string Email = "");
}
