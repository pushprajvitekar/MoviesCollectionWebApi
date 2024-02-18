using Application.Movies.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movies.Commands.CreateMovie
{
    public class CreateMovieRequest : IRequest<MovieDto>
    {
        public CreateMovieRequest(CreateMovieDto createMovieDto)
        {
            CreateMovieDto = createMovieDto;
        }

        public CreateMovieDto CreateMovieDto { get; }
    }
}
