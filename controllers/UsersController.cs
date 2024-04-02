using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepo;

    public UsersController(IUserRepository userRepository)
    {
        _userRepo = userRepository;
    }

    // Implement GET, POST, PUT, DELETE methods for Users using dependency injected _userRepo

    [HttpGet]
    public async Task<IActionResult> GetUsers(string sortColumn, SortOrder sortOrder, int pageNumber, int pageSize)
    {
        QueryObject queryObject = new QueryObject
        {
            OrderBy = sortColumn,
            IsAscending = sortOrder == SortOrder.Ascending,
            PageNumber = pageNumber,                  
            PageSize = pageSize
        };  
        var (users, totalCount) = await _userRepo.GetAllUsers(queryObject);       
        
        var response = new
        {
            Users = users,
            TotalCount = totalCount
        };

        return Ok(response);
    }
    

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _userRepo.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user.Result.ToUserDto());
    }

    [HttpPost]
    public IActionResult CreateUser(User user)
    {
        _userRepo.CreateUser(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.ID }, user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, User user)
    {
        var existingUser = _userRepo.GetUserById(id);
        if (existingUser == null)
        {
            return NotFound();
        }
        user.ID = id;
        _userRepo.UpdateUser(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var existingUser = _userRepo.GetUserById(id);
        if (existingUser == null)
        {
            return NotFound();
        }
        _userRepo.DeleteUser(id);
        return NoContent();
    }
}