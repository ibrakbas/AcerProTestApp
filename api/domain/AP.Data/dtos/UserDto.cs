namespace AP.Data.dtos;

public sealed class LoginDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
public sealed class UserDto
{
    public string UserName { get; set; }
    public string Password { get; set; } 
    public UserRoleDto UserRole { get; set; }
}
public sealed class UserRoleDto
{
    public int AP_UserId { get; set; }
    public bool IsAdmin { get; set; }
    public bool CanWrite { get; set; }
    public bool CanDelete { get; set; }
}
