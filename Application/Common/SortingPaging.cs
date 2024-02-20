using FluentValidation;

namespace Application.Common
{
    public record SortingPaging(string? SortBy = null, bool SortAsc = false, int PageNumber = 1, int PageSize = 10);


    public class SortingPagingValidator : AbstractValidator<SortingPaging>
    {
        private readonly string[] allowedSortOptions = { "name", "description", "id" };

        public SortingPagingValidator()
        {
            When(c => !string.IsNullOrEmpty(c.SortBy), () =>
            RuleFor(x => x.SortBy)
                .Must(BeAValidSortOption)
                .WithMessage($"Sort option must be one of: {string.Join(", ", allowedSortOptions)}"));
        }

        private bool BeAValidSortOption(string? sortOption)
        {
            return allowedSortOptions.Contains(sortOption?.ToLower());
        }
    }

}
