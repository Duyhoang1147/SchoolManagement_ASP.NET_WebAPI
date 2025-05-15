using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagerment_WebAPI.Model.Dto
{
    public class ClassroomGetDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}