namespace Application.Common
{
    public record SortingPaging(string? SortBy = null, bool SortAsc = false, int PageNumber = 1, int PageSize = 10);
}
