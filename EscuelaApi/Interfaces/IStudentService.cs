using SchoolApi.Models;

namespace SchoolApi.Interfaces
{
    public interface IStudentService
    {
        Task<Student?> GetStudentByDniAsync(string dni);
        Task<IEnumerable<Student>> GetStudentsAsync(string? dni, int? birthYear, int page, int pageSize);
        Task<IEnumerable<Student>> CreateStudentsAsync(IEnumerable<Student> students);
        Task<bool> DeleteStudentAsync(string dni);
    }
}
