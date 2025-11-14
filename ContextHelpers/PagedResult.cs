namespace ContextHelpers;

public class PagedResult<T> where T : new()
{
    public int Count { get; set; }

    public List<T> Items { get; set; }
}
