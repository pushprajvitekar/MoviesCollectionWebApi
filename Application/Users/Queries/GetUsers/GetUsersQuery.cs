using Application.Common;
using Application.Users.Dtos;
using MediatR;

namespace Application.Users.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
    {
        public GetUsersQuery(SortingPaging? sortingPaging = null)
        {
            SortingPaging = sortingPaging;
        }

        public SortingPaging? SortingPaging { get; }
    }
}
