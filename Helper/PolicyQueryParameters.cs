public class PolicyQueryParameters
{
    public string? SortColumn { get; set; }
    public SortOrder SortOrder { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? StartDate { get; set; }
    public int? UserID { get; set; }
}