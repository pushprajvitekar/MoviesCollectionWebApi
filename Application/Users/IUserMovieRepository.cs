using Application.Common;
using Domain.Users;
using Domain.Users.Queries;

namespace Application.Users
{
    public interface IUserMovieRepository
    {
        UserMovie Add(UserMovie userMovie);
        UserMovie Delete(UserMovie userMovie);
        UserMovie? GetById(string userId, int id);
        IList<UserMovie> GetAll(int userId, UserMovieFilter? filter = null, SortingPaging? sortingPaging = null);
        UserMovie? GetById(int id);
    }
}
