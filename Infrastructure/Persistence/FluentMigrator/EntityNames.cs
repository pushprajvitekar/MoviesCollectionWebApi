namespace DatabaseMigrations
{
    internal static class EntityNames
    {
        internal static class TableName
        {
            internal const string Movie = "Movie";
            internal const string User = "AspNetUsers";
            internal const string UserRole = "AspNetRoles";
            internal const string UserMovieReview = "UserMovieReview";
            internal const string MovieGenre = "MovieGenre";
            internal const string UserMovie = "UserMovie";
        }
        internal static class ColumnName {
            internal const string Id = "Id";
            internal const string Name = "Name";
            internal const string Description = "Description";
            internal const string LoginName = "LoginName";
            internal const string HashedPassword = "HashedPassword";
            internal const string Review = "Review";
            internal const string Rating = "Rating";

        }
        internal static string foreignkeyname(string tablename, string idname)
        {
            return $"{tablename}{idname}";
        }

        internal static string junctiontablename(string table1, string table2)
        {
            return $"{table1}{table2}";
        }
    }
}
