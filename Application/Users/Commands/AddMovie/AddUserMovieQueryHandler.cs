﻿using Application.Movies;
using Application.Users.Dtos;
using Domain.Exceptions;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands.AddMovie
{
    public class AddUserMovieQueryHandler : IRequestHandler<AddUserMovieQuery, UserMovieDto>
    {
        private readonly IUserMovieRepository userMovieRepository;
        private readonly IMovieRepository movieRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public AddUserMovieQueryHandler(IUserMovieRepository userMovieRepository, IMovieRepository movieRepository, UserManager<ApplicationUser> userManager)
        {
            this.userMovieRepository = userMovieRepository;
            this.movieRepository = movieRepository;
            this.userManager = userManager;
        }
        public async Task<UserMovieDto> Handle(AddUserMovieQuery request, CancellationToken cancellationToken)
        {

            var movieId = request.AddMovieDto.MovieId;
            var userId = request.UserId;
            var user = userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user?.UserName == null) throw new DomainException("User not found", null, DomainErrorCode.NotFound);
            var exists = await userMovieRepository.GetByUserName(user.UserName, movieId);
            if (exists != null) throw new DomainException("Movie already added", null, DomainErrorCode.Exists);
            var movie = await movieRepository.GetById(movieId);
            if (movie == null) throw new DomainException("Movie not found", null, DomainErrorCode.NotFound);
            var usermovie = new UserMovie();
            usermovie.AssociateUserMovie(user, movie);
            var res = await userMovieRepository.Add(usermovie);
            return new UserMovieDto(usermovie.Id, usermovie.Movie.Id, usermovie.Movie.Name, usermovie.Movie.Description, usermovie.Movie.MovieGenre.Name);
        }
    }
}
