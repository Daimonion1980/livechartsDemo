namespace WpfApp5
{
    public record RowSelectedEvent(IReadOnlyCollection<int> Data, string Name, int Id, bool IsSelected);
}
