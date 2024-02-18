namespace Application.Common
{
    public record SortingPaging(string? SortBy, bool SortAsc, int PageNumber, int PageSize);
}
