using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagerment_WebAPI.Model.Dto
{
    public class TeacherClassroomCreateDto
    {
        
        public Guid TeacherId { get; set; }
        public Guid ClassroomId { get; set; }
    }
}