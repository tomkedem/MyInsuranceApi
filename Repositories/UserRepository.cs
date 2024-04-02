using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    private readonly ILogger<UserRepository> _logger;

    public UserRepository(AppDbContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<(IEnumerable<UserDto>, int)> GetAllUsers(QueryObject queryObject)
    {
        var query = _context.Users.AsQueryable();

        var page = await query
            .Select(u => new UserDto
            {
                ID = u.ID,
                Name = u.Name,
                Email = u.Email
            })
            .OrderBy(u => queryObject.OrderBy + (queryObject.IsAscending ? " ASC" : " DESC"))
            .Skip((queryObject.PageNumber - 1) * queryObject.PageSize)
            .Take(queryObject.PageSize)
            .ToListAsync();

        var total = query.Count();

        return (page, total);
    }
    

    public async Task<User?> GetUserById(int id)
    {
        try
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.ID == id);
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, $"Failed to get user with id {id}");
            return null;
        }
    }
    public async Task CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        var updateUser = _context.Users.FirstOrDefault(u => u.ID == user.ID);
        if (updateUser != null)
        {
            updateUser.Name = user.Name;
            updateUser.Email = user.Email;
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    
}