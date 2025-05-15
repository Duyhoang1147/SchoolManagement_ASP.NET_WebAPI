using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagerment_WebAPI.Model
{
    public class TeacherClassroom
    {
        public Guid TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public Guid ClassroomId { get; set; }
        public Classroom? Classroom { get; set; }
    }
}