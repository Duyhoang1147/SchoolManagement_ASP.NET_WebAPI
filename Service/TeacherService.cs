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
    public class TeacherService
    {
        private readonly AppDbContext _context;

        public TeacherService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeacherGetDto>> GetAllTeachersAsync()
        {
            return await _context.Teachers
                    .Select(s => new TeacherGetDto
                    {
                        Id = s.Id,
                        Fullname = s.Fullname,
                        Email = s.Email,
                        Subject = s.Subject
                    })
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<TeacherGetDto?> GetTeacherByIdAsync(Guid id)
        {
            return await _context.Teachers
                    .Where(s => s.Id == id)
                    .Select(s => new TeacherGetDto
                    {
                        Id = s.Id,
                        Fullname = s.Fullname,
                        Email = s.Email,
                        Subject = s.Subject
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
        }

        public async Task<TeacherGetDto> CreateTeacherAsync(TeacherGetDto teacher)
        {
            var newTeacher = new Teacher
            {
                Id = Guid.NewGuid(),
                Fullname = teacher.Fullname,
                Email = teacher.Email,
                Subject = teacher.Subject
            };

            _context.Teachers.Add(newTeacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<TeacherGetDto?> UpdateTeacherAsync(Guid id, TeacherGetDto teacher)
        {
            var techer = new Teacher {
                Id = id,
                Fullname = teacher.Fullname,
                Email = teacher.Email,
                Subject = teacher.Subject
            };

            _context.Teachers.Update(techer);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<bool> DeleteTeacherAsync(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return false;
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}