using Microsoft.Extensions.Configuration.UserSecrets;

public class QueryObject
{
    public string? OrderBy  { get; set; } = null;
    public bool IsAscending { get; set; } = true;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 7;
    public DateTime? StartDate { get; set; } = null;
    public int? UserID { get; set; }
}
