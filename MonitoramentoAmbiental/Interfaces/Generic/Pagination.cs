namespace MonitoramentoAmbiental.Models;

public class MetaData
{
    public int TotalItems { get; set; }
    public int ItemCount { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public MetaData Meta { get; set; } = new();
}
