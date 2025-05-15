using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagerment_WebAPI.Model
{
    public class Teacher : User
    {
        [Required]
        [MaxLength(70)]
        public string? Subject { get; set; }

        public ICollection<TeacherClassroom>? TeacherClassrooms { get; set; } = new List<TeacherClassroom>();
    }
}