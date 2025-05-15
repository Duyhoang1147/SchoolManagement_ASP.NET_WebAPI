using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagerment_WebAPI.Model.Dto
{
    public class ClassroomListDto
    {
        public List<TeacherGetDto> TeacherId { get; set; } = new List<TeacherGetDto>();
    }
}