using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Routing;

namespace Synapse.Models
{
    public class Class_Event
    {
        public int ID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int NumStudents { get; set; }
        public int NumInstructors { get; set; }
        public String Organization { get; set; }

        public ClassStatus Status { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }
        //Equipment list
        //Location ID
        //Organization..?
    }

    public enum ClassStatus
    {
        Submitted,
        Approved,
        Needs_Action,
        Cancelled
    }
    //public int InstructorID { get; set; }
    //Equipment list
    //Location ID
    //Organization..?
}

