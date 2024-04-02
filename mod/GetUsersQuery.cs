using MediatR;
public record GetUsersQuery (
    string? SortColumn,
    string? SortOrder ,
    int PageNumber ,
    int PageSize ) : IRequest<List<UserDto>>;

    
