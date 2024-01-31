namespace VirtualTeacher.Models
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(IEnumerable<T> items, int totalPages, int pageNumber)
        {
            AddRange(items);
            this.TotalPages = totalPages;
            this.PageNumber = pageNumber;
        }

        public int TotalPages { get; }
        public int PageNumber { get; }

        public bool HasPreviousPage
        {
            get
            {
                return PageNumber > 1;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return PageNumber < TotalPages;
            }
        }
    }
}
