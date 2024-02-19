using Domain.Movies;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace EFCorePersistence
{
    public partial class MovieCollectionDBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }
        public MovieCollectionDBContext(DbContextOptions<MovieCollectionDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(p => p.Movies)
                ;
            modelBuilder.Entity<MovieGenre>().ToTable("MovieGenre").HasKey(p => p.Id);
            modelBuilder.Entity<UserMovie>()
                .ToTable("UserMovie")
                .HasKey(p => p.Id)
                ;
            modelBuilder.Entity<UserMovie>()
            .HasOne(p => p.Movie)
                .WithMany(p => p.Users)
                .HasForeignKey(p => p.MovieId);

            modelBuilder.Entity<UserMovie>()
            .HasOne(p => p.User)
                .WithMany(p => p.Movies)
                .HasForeignKey(f => f.AspNetUsersId).HasConstraintName("FK_UserMovie_AspNetUsersId_AspNetUsers_Id");

            modelBuilder.Entity<UserMovie>()
               .HasOne(p => p.UserMovieReview)
               .WithOne(p => p.UserMovie)
               .HasForeignKey<UserMovieReview>(c => c.UserMovieId)
               ;

            modelBuilder.Entity<Movie>().ToTable("Movie").HasKey(p => p.Id);
            modelBuilder.Entity<Movie>().HasOne(p => p.MovieGenre);


            modelBuilder.Entity<UserMovieReview>().ToTable("UserMovieReview").HasKey(p => p.Id);
            //modelBuilder.Entity<UserMovieReview>()
            //  .HasOne(p => p.UserMovie)
            //  .WithOne(p => p.UserMovieReview)
            //  .HasForeignKey<UserMovie>(c => c.Id);


            base.OnModelCreating(modelBuilder);

        }


    }
}
