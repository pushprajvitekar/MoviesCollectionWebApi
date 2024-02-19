namespace Application.Users.Dtos
{
    public record UserMovieDto(int Id, int MovieId, string MovieName, string? Description, string Genre);
}
