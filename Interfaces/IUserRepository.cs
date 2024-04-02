public interface IUserRepository
{
    Task<(IEnumerable<UserDto>, int)> GetAllUsers(QueryObject queryObject);
    Task<User> GetUserById(int id);
    Task CreateUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(int id);
}