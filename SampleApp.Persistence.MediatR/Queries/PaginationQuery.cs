namespace SampleApp.Persistence.MediatR.Queries
{
    public class PaginationQuery
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}