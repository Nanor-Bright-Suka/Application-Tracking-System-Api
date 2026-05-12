


namespace ATS.Dtos.Jobs;
public class PagedResultDto<T>
{
    public List<T> Items { get; set; } = [];

    public int Page { get; set; }
    public int PageSize { get; set; }

    public int TotalCount { get; set; }
}