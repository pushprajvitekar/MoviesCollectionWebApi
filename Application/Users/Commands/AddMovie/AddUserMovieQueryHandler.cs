using Application.Movies.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.AddMovie
{
    public class AddUserMovieQueryHandler : IRequestHandler<AddUserMovieQuery, MovieDto>
    {
        public Task<MovieDto> Handle(AddUserMovieQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
