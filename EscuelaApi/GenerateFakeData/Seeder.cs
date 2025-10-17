using SchoolApi.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolApi.GenerateFakeData
{
    public static class Seeder
    {
        public static async Task SeedStudentsAsync(SchoolContext context)
        {
            await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE student RESTART IDENTITY CASCADE");
            await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE info_group RESTART IDENTITY CASCADE");
            await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE subject RESTART IDENTITY CASCADE");

            var students = SeedData.GenerateFakeStudents(20000);
            var groups = SeedData.GenerateFakeGroups(30);
            var subjects = SeedData.GenerateFakeSubjects(30);
            var groupStudents = SeedData.GenerateFakeGroupsStudents(students, groups);
            var subjectStudents = SeedData.GenerateFakeSubjectStudents(students, subjects);

            await context.Students.AddRangeAsync(students);
            await context.InfoGroups.AddRangeAsync(groups);
            await context.Subjects.AddRangeAsync(subjects);
            await context.GroupStudents.AddRangeAsync(groupStudents);
            await context.SubjectStudents.AddRangeAsync(subjectStudents);
            await context.SaveChangesAsync();
        }
    }
}
