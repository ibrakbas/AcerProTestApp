using AP.Generic.Abstraction;

namespace AP.Data.entites;

public class Workers : EasyBaseEntity<int>
{

    public string Name { get; set; }
    public string Surname { get; set; }
    public int DepartmentId { get; set; }
    public virtual Departments Department { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; } 
    public string Salary { get; set; } 
    public DateTime StartDate { get; set; } 


}
