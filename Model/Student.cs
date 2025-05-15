using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagerment_WebAPI.Model
{
    public class Student : User
    {
        [Required]
        public int SchoolYear { get; set; }
        
        public Guid? Student_ClassroomId {get; set;}
        public Classroom? Student_Classroom { get; set; }
    }
}