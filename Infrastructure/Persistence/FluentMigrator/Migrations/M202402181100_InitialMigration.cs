using FluentMigrator;

namespace DatabaseMigrations.Migrations
{
    [Migration(202402181100)]
    public class M202402181100_InitialMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
           
            Create.Table(EntityNames.TableName.MovieGenre)
                .WithColumn(EntityNames.ColumnName.Id).AsInt32().PrimaryKey()
                .WithColumn(EntityNames.ColumnName.Name).AsString(50).NotNullable()
            ;

            //Create.Table(EntityNames.TableName.UserRole)
            //    .WithColumn(EntityNames.ColumnName.Id).AsInt32().PrimaryKey()
            //    .WithColumn(EntityNames.ColumnName.Name).AsString(50).NotNullable()
            //;

            var genreFk= EntityNames.foreignkeyname(EntityNames.TableName.MovieGenre,EntityNames.ColumnName.Id);

            Create.Table(EntityNames.TableName.Movie)
                .WithColumn(EntityNames.ColumnName.Id).AsInt32().PrimaryKey()
                .WithColumn(EntityNames.ColumnName.Name).AsString(400).NotNullable()
                .WithColumn(EntityNames.ColumnName.Description).AsString(2000).Nullable()
                .WithColumn(genreFk).AsInt32().NotNullable()
            ;
            Create.ForeignKey()
                .FromTable(EntityNames.TableName.Movie)
                .ForeignColumn(genreFk)
                .ToTable(EntityNames.TableName.MovieGenre)
                .PrimaryColumn(EntityNames.ColumnName.Id);


            var movieFk = EntityNames.foreignkeyname(EntityNames.TableName.Movie, EntityNames.ColumnName.Id);
            var userFk = EntityNames.foreignkeyname(EntityNames.TableName.User, EntityNames.ColumnName.Id);

            Create.Table(EntityNames.TableName.UserMovie)
               .WithColumn(EntityNames.ColumnName.Id).AsInt32().PrimaryKey()
               .WithColumn(movieFk).AsInt32().NotNullable()
               .WithColumn(userFk).AsString().NotNullable()
           ;
            Create.ForeignKey()
                .FromTable(EntityNames.TableName.UserMovie)
                .ForeignColumn(movieFk)
                .ToTable(EntityNames.TableName.Movie)
                .PrimaryColumn(EntityNames.ColumnName.Id);

            Create.ForeignKey()
              .FromTable(EntityNames.TableName.UserMovie)
              .ForeignColumn(userFk)
              .ToTable(EntityNames.TableName.User)
              .PrimaryColumn(EntityNames.ColumnName.Id);

            var userMovieFk = EntityNames.foreignkeyname(EntityNames.TableName.UserMovie, EntityNames.ColumnName.Id);
            Create.Table(EntityNames.TableName.UserMovieReview)
               .WithColumn(EntityNames.ColumnName.Id).AsInt32().PrimaryKey()
               .WithColumn(EntityNames.ColumnName.Review).AsString(2000).NotNullable()
               .WithColumn(EntityNames.ColumnName.Rating).AsFloat().NotNullable()
               .WithColumn(userMovieFk).AsInt32().NotNullable()
           ;


            Create.ForeignKey()
                .FromTable(EntityNames.TableName.UserMovieReview)
                .ForeignColumn(userMovieFk)
                .ToTable(EntityNames.TableName.UserMovie)
                .PrimaryColumn(EntityNames.ColumnName.Id);

            SeedLookupData();
        }

        private void SeedLookupData()
        {
           
            Insert.IntoTable(EntityNames.TableName.MovieGenre)
                .Row(new { Id=1, Name="Action"})
                .Row(new { Id = 2, Name = "Romance" })
                .Row(new { Id = 3, Name = "War" })
                .Row(new { Id = 4, Name = "Drama" })
                .Row(new { Id = 5, Name = "Thriller" })
                .Row(new { Id = 6, Name = "Comedy" })
                ;

            //Insert.IntoTable(EntityNames.TableName.UserRole)
            //  .Row(new { Id = 1, Name = "Admin" })
            //  .Row(new { Id = 2, Name = "Manager" })
            //  .Row(new { Id = 3, Name = "User" })
            //  ;
        }
    }
}
