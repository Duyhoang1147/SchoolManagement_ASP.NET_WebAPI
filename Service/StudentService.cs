using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagerment_WebAPI.Data;
using SchoolManagerment_WebAPI.Model;
using SchoolManagerment_WebAPI.Model.Dto;

namespace SchoolManagerment_WebAPI.Service
{
    public class StudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentDto>> GetAllStudentsAsync()
        {
            return await _context.Students.Include(s => s.Student_Classroom)
                        .Select(s => new StudentDto{
                            Id = s.Id,
                            Fullname = s.Fullname,
                            Email = s.Email,
                            SchoolYear = s.SchoolYear,
                            ClassroomName = s.Student_Classroom != null ? s.Student_Classroom.Name : null
                        })
                        .AsNoTracking()
                        .ToListAsync();
        }

        public async Task<StudentDto?> GetStudentByIdAsync(Guid id)
        {
            return await _context.Students
                    .Include(s => s.Student_Classroom)
                    .Where(s => s.Id == id)
                    .Select(s => new StudentDto {
                        Id = s.Id,
                        Fullname = s.Fullname,
                        Email = s.Email,
                        SchoolYear = s.SchoolYear,
                        ClassroomName = s.Student_Classroom != null ? s.Student_Classroom.Name : null
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
        }

        public async Task<StudentCUDDot> CreateStudentAsync (StudentCUDDot studentDto)
        {
            var student = new Student
            {
                Fullname = studentDto.Fullname,
                Email = studentDto.Email,
                SchoolYear = studentDto.SchoolYear,
                Student_ClassroomId = studentDto.ClassroomName
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return studentDto;
        }

        public async Task<Student?> UpdateStudentAsync (Guid id, StudentCUDDot studentDto)
        {
                var student = new Student
                {
                    Id = id,
                    Fullname = studentDto.Fullname,
                    Email = studentDto.Email,
                    SchoolYear = studentDto.SchoolYear,
                    Student_ClassroomId = studentDto.ClassroomName
                };

                _context.Students.Update(student);
                await _context.SaveChangesAsync();
                return student;
        }


        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return false;
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}