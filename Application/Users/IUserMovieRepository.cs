using Application.Common;
using Domain.Users;
using Domain.Users.Queries;

namespace Application.Users
{
    public interface IUserMovieRepository
    {
        Task<UserMovie> Add(UserMovie userMovie);
        Task<bool> Delete(UserMovie userMovie);
        Task<UserMovie?> GetByUserName(string username, int movieId);
        Task<Page<UserMovie>> GetAll(string username, UserMovieFilter? filter = null, SortingPaging? sortingPaging = null);
    }
}
