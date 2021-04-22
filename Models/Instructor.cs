using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synapse.Models
{
    public class Instructor
    {
        public int InstructorID { get; set; }
        public String userID { get; set; }
        public String firstName { get; set; }
        public String middleName { get; set; }
        public String lastName { get; set; }
        public DateTime startDate { get; set; }

        public virtual ICollection<Class_Event> Class_Events { get; set; }
        //public virtual ICollection<Skill> Skills { get; set; }
        //public virtual ICollection<Availability> Class_Events { get; set; }
        //public virtual ICollection<Class_Event> Class_Events { get; set; }
        //public virtual ICollection<Class_Event> Class_Events { get; set; }
    }
}
