using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagerment_WebAPI.Model
{
    public class StudentDto
    {
    public Guid Id { get; set; }
    public string? Fullname { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public int SchoolYear { get; set; }
    public string? ClassroomName { get; set; }
    }
}