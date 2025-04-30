using AP.Generic.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Data.entites
{
    public class Departments : EasyBaseEntity<int>
    {
        public string Description { get; set; }
        public virtual ICollection<Workers> Workers { get; set; } = new HashSet<Workers>();
    }
}
