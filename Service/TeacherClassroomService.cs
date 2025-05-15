using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManagerment_WebAPI.Data;
using SchoolManagerment_WebAPI.Model;

namespace SchoolManagerment_WebAPI.Service
{
    public class TeacherClassroomService
    {
        private readonly AppDbContext _context;

        public TeacherClassroomService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeacherClassroom>> GetAllTeacherClassroomsAsync()
        {
            return await _context.TeacherClassrooms.OrderBy(tc => tc.TeacherId).ToListAsync();
        }

        public async Task<List<TeacherClassroom>> GetTeacherClassroomByTeacherIdAsync(Guid id)
        {
            return await _context.TeacherClassrooms.Where(tc => tc.TeacherId == id).ToListAsync();
        }

        public async Task<List<TeacherClassroom>> GetTeacherClassroomByClassroomIdAsync(Guid id)
        {
            return await _context.TeacherClassrooms.Where(tc => tc.ClassroomId == id).ToListAsync();
        }

        public async Task<TeacherClassroom> CreateTeacherClassroomAsync(TeacherClassroom teacherClassroom)
        {
            _context.TeacherClassrooms.Add(teacherClassroom);
            await _context.SaveChangesAsync();
            return teacherClassroom;
        }
        public async Task<bool> DeleteTeacherClassroomAsync(Guid TeacherId, Guid ClassroomId)
        {
            var teacherClassroom = await _context.TeacherClassrooms
                .FirstOrDefaultAsync(tc => tc.TeacherId == TeacherId && tc.ClassroomId == ClassroomId);
            if (teacherClassroom == null)
            {
                return false;
            }

            _context.TeacherClassrooms.Remove(teacherClassroom);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTeacherClassroomAsync(Guid TeacherId, Guid ClassroomId, Guid newClassroomId)
        {
            var existingTeacherClassroom = await _context.TeacherClassrooms
                .FirstOrDefaultAsync(tc => tc.TeacherId == TeacherId && tc.ClassroomId == ClassroomId);
            if (existingTeacherClassroom == null)
            {
                return false;
            }

            existingTeacherClassroom.ClassroomId = newClassroomId;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}