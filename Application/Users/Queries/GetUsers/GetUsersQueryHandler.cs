using Application.Users.Dtos;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public GetUsersQueryHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {

            var users = await userManager.GetUsersInRoleAsync(UserRoleEnum.User.ToString());
            if (request.SortingPaging != null)
            {
                var pageNum = request.SortingPaging?.PageNumber ?? 1;
                var pageSize = request.SortingPaging?.PageSize ?? 10;
                var skip = request.SortingPaging?.PageNumber <= 1 ? 0 : pageNum * pageSize;
                var pagedUsers = users.Skip(skip)
                            .Take(pageSize)
                            .Select(a => new UserDto { UserName = a.UserName ?? "Unknown" });
                return pagedUsers;
            }
            var dtos = users
                       .Select(a => new UserDto { UserName = a.UserName ?? "Unknown" });
            return dtos;
        }
    }
}
