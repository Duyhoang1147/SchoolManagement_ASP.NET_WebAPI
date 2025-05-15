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
    public class ClassroomService
    {
        private readonly AppDbContext _context;
        public ClassroomService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClassroomGetDto>> GetAllClassroomsAsync()
        {
            return await _context.Classrooms.
                    Select(c => new ClassroomGetDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<ClassroomGetDto?> GetClassroomByIdAsync(Guid id)
        {
            return await _context.Classrooms
                    .Where(c => c.Id == id)
                    .Select(c => new ClassroomGetDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
        }

        public async Task<ClassroomGetDto> CreateClassroomAsync([FromBody] ClassroomGetDto classroom)
        {
            var newClassroom = new Classroom
            {
                Name = classroom.Name
            };
            _context.Classrooms.Add(newClassroom);
            await _context.SaveChangesAsync();
            return classroom;
        }

        public async Task<ClassroomGetDto?> UpdateClassroomAsync(Guid id, ClassroomGetDto classroom)
        {
            try {
                var newClassroom = new Classroom
                {
                    Id = id,
                    Name = classroom.Name
                };
                
                _context.Classrooms.Update(newClassroom);
                await _context.SaveChangesAsync();
                return classroom;
            } catch (DbUpdateConcurrencyException) {
                return null;
            }
        }

        public async Task<bool> DeleteClassroomAsync(Guid id)
        {
            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return false;
            }

            _context.Classrooms.Remove(classroom);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}