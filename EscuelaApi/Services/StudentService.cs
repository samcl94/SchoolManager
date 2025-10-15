using SchoolApi.Interfaces;
using SchoolApi.Models;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Utils;
using System.ComponentModel;
using System.Diagnostics;

namespace SchoolApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly SchoolContext _context;

        public StudentService(SchoolContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetStudentByDniAsync(string dni)
        {
            if (!ValidationHelper.IsValidDniNie(dni))
                throw new ArgumentException("Invalid DNI/NIE format");

            return await _context.Students
                .SingleOrDefaultAsync(s => s.Dni.ToUpper() == dni.ToUpper());
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync(string? dni, int? birthYear, int page, int pageSize)
        {
            var query = _context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(dni))
            {
                if (!ValidationHelper.IsValidDniNie(dni))
                    throw new ArgumentException("Invalid DNI/NIE format");

                query = query.Where(s => s.Dni.ToUpper() == dni.ToUpper());
            }

            if (birthYear.HasValue)
                query = query.Where(s => s.BirthDate.Year == birthYear.Value);

            return await query
                .OrderBy(s => s.Dni)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<IEnumerable<Student>> CreateStudentsAsync(IEnumerable<Student> students)
        {
            if (students == null || !students.Any())
                throw new ArgumentNullException(nameof(students));

            foreach (var s in students)
            {
                if (!ValidationHelper.IsValidDniNie(s.Dni))
                    throw new ArgumentException($"Invalid DNI/NIE format for student {s.FirstName} {s.LastName}, no data will be added");

                if (string.IsNullOrWhiteSpace(s.FirstName) ||
                    string.IsNullOrWhiteSpace(s.LastName) ||
                    s.BirthDate == default || //birthday is mandatory in Models, so asp send the default date if value is null in JSON body
                    string.IsNullOrWhiteSpace(s.ParentEmergencyPhone1))
                    throw new ArgumentException($"Missing required fields for student {s.Dni}, no data will be added");
            }


            await _context.Students.AddRangeAsync(students);
            await _context.SaveChangesAsync();

            return students;

        }
        public async Task<bool> DeleteStudentAsync(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                throw new ArgumentException("DNI cannot be null or empty");
            }

            if (!ValidationHelper.IsValidDniNie(dni))
            {
                throw new ArgumentException($"Invalid DNI/NIE format: {dni}");
            }

            var student = await _context.Students.SingleOrDefaultAsync(s => s.Dni.ToUpper() == dni.ToUpper());

            if (student == null) return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return true;
        }

    }

}
