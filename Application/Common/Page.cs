namespace Application.Common
{
    public class Page<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalItemCount { get; set; }
    }
}
