using AP.Data.entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Data.dtos
{
   public sealed class WorkerDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int DepartmentId { get; set; } 
        public int Age { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Salary { get; set; }
        public DateTime StartDate { get; set; }
    }
}
