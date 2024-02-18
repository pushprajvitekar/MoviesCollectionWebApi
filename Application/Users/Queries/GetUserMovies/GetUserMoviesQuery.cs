﻿using Application.Movies.Dtos;
using Domain.Common;
using Domain.Movies.Queries;
using Domain.Users.Queries;
using MediatR;

namespace Application.Users.Queries.GetUserMovies
{
    public class GetUserMoviesQuery : IRequest<IEnumerable<MovieDto>>
    {
        public GetUserMoviesQuery(UserMovieFilter? filter, SortingPaging? sortingPaging)
        {
            Filter = filter;
            SortingPaging = sortingPaging;
        }

        public UserMovieFilter? Filter { get; }
        public SortingPaging? SortingPaging { get; }
    }
}
