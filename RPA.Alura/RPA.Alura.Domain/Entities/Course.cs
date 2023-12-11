using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA.Alura.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }        
        public string Instructor { get; set; }
        public int Workload { get; set; } 
        public string Description { get; set; }
    }
}
