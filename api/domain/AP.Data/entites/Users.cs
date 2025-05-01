using AP.Generic.Abstraction;

namespace AP.Data.entites;

public class Users : EasyBaseEntity<int>
{
    public string UserName { get; set; }
    public string Password { get; set; } 
    public int AP_UserRoleId { get; set; }
    public virtual UserRole AP_UserRole { get; set; }
}
public class UserRole : EasyBaseEntity<int>
{ 
    public bool IsAdmin { get; set; }
    public bool CanWrite { get; set; }
    public bool CanUpdate { get; set; }
    public bool CanDelete { get; set; }
}
