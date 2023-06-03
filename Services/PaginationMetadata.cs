namespace PracticeWebAPI.Services
{
    public class PaginationMetadata
    {
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public PaginationMetadata(int totalRecords, int currentPage, int pageSize)
        {
            TotalRecords = totalRecords;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        }
    }
}
