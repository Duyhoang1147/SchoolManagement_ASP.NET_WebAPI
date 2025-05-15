using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagerment_WebAPI.Model.Dto
{
    public class TeacherClassroomListClassroomDto
    {
        public List<ClassroomGetDto> ClassroomId { get; set; } = new List<ClassroomGetDto>();
    }
}