using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagerment_WebAPI.Model
{
    public class Classroom
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<TeacherClassroom> TeacherClassrooms { get; set; } = new List<TeacherClassroom>();
    }
}