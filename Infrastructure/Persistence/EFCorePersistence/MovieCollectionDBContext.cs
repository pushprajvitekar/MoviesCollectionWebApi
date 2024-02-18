using Domain.Movies;
using Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EFCorePersistence
{
    public class MovieCollectionDBContext : IdentityUserContext<ApplicationUser>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }
        public DbSet<UserMovieReview> UserMovieReviews { get; set; }
        public MovieCollectionDBContext(DbContextOptions<MovieCollectionDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(p => p.Movies)
                ;

            modelBuilder.Entity<UserMovie>()
                .HasOne(p => p.Movie)
                .WithMany(p => p.Users)
                //.HasForeignKey(p => p.Movie.Id)
                ;

            modelBuilder.Entity<UserMovie>()
               .HasOne(p => p.UserMovieReview)
               //.WithOne(p => p.UserMovie)
               ;

            modelBuilder.Entity<Movie>()
               .HasOne(p => p.Genre)
               ;

            modelBuilder.Entity<UserMovieReview>()
              .HasOne(p => p.UserMovie)
              .WithOne(p => p.UserMovieReview)
              .HasForeignKey<UserMovie>(c => c.Id);
            ;

            base.OnModelCreating(modelBuilder);
        }


    }
}
