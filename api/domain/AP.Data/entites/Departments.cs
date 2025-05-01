using AP.Generic.Abstraction;

namespace AP.Data.entites;

public class Departments : EasyBaseEntity<int>
{
    public string Description { get; set; }
    public virtual ICollection<Workers> Workers { get; set; } = new HashSet<Workers>();
}
