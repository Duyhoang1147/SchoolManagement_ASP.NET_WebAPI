using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SchoolManagerment_WebAPI.Model.Dto
{
    public class TeacherClassroomDto
    {
        public string? Subject { get; set; }
        public string? Teacher { get; set; }
        public string? Classroom { get; set; }
    }
}