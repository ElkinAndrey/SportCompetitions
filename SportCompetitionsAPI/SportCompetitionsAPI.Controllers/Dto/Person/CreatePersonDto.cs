namespace SportCompetitionsAPI.Controllers.Dto.Person
{
    /// <summary>
    /// Данные для создания человека
    /// </summary>
    /// <param name="DateOfBirth">Дата рождения</param>
    /// <param name="Name">Имя</param>
    /// <param name="Email">Электронная почта</param>
    public sealed record class CreatePersonDto(
        DateTime DateOfBirth,
        string Name = "",
        string Email = "");
}
