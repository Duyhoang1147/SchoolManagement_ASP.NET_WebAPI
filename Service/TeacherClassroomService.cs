using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManagerment_WebAPI.Data;
using SchoolManagerment_WebAPI.Model;
using SchoolManagerment_WebAPI.Model.Dto;

namespace SchoolManagerment_WebAPI.Service
{
    public class TeacherClassroomService
    {
        private readonly AppDbContext _context;

        public TeacherClassroomService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeacherClassroomDto>> GetAllTeacherClassroomsAsync()
        {
            return await _context.TeacherClassrooms
                    .Include(i => i.Classroom )
                    .Include(i => i.Teacher)
                    .Select(tc => new TeacherClassroomDto
                    {
                        Teacher = tc.Teacher != null ? tc.Teacher.Fullname : null,
                        Subject = tc.Teacher != null ? tc.Teacher.Subject : null,
                        Classroom = tc.Classroom != null ? tc.Classroom.Name : null
                    })
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<TeacherClassroomListClassroomDto> GetTeacherClassroomByTeacherIdAsync(Guid id)
        {
            var classrooms = await _context.TeacherClassrooms
                .Where(tc => tc.TeacherId == id)
                .Select(tc => new ClassroomGetDto
                {
                    Id = tc.ClassroomId,
                    Name = tc.Classroom != null ? tc.Classroom.Name : null
                })
                .ToListAsync();
            return new TeacherClassroomListClassroomDto { ClassroomId = classrooms };
        }

        public async Task<ClassroomListDto> GetTeacherClassroomByClassroomIdAsync(Guid id)
        {
            var ListTeacher = await _context.TeacherClassrooms
                .Include(i => i.Teacher)
                .Where(tc => tc.ClassroomId == id)
                .Select(tc => new TeacherGetDto
                {
                    Id = tc.TeacherId,
                    Fullname = tc.Teacher != null ? tc.Teacher.Fullname : null,
                    Email = tc.Teacher != null ? tc.Teacher.Email : null,
                    Subject = tc.Teacher != null ? tc.Teacher.Subject : null
                })
                .ToListAsync();
            return new ClassroomListDto { TeacherId = ListTeacher };
        }

        public async Task<TeacherClassroomCreateDto> CreateTeacherClassroomAsync(TeacherClassroomCreateDto teacherClassroom)
        {
            var newTeacherClassroom = new TeacherClassroom
            {
                TeacherId = teacherClassroom.TeacherId,
                ClassroomId = teacherClassroom.ClassroomId
            };

            _context.TeacherClassrooms.Add(newTeacherClassroom);
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