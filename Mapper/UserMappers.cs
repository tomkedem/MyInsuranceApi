public static class UserMappers
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            ID = user.ID,
            Name = user.Name,
            Email = user.Email
        };
    }

    public static User ToUserModel(this UserDto userDto)
    {
        return new User
        {
            ID = userDto.ID,
            Name = userDto.Name,
            Email = userDto.Email
        };
    }
}