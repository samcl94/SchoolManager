using SchoolApi.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolApi.GenerateFakeData
{
    public static class Seeder
    {
        public static async Task SeedStudentsAsync(SchoolContext context)
        {
            await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE student RESTART IDENTITY CASCADE");
            await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE group_student RESTART IDENTITY CASCADE");
            await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE info_group RESTART IDENTITY CASCADE");

           var students = SeedData.GenerateFakeStudents(20000);
           var groups = SeedData.GenerateFakeGroups(30);
           var groupStudents = SeedData.GenerateFakeGroupsStudents(students, groups);

            await context.Students.AddRangeAsync(students);
            await context.InfoGroups.AddRangeAsync(groups);
            await context.GroupStudents.AddRangeAsync(groupStudents);
            await context.SaveChangesAsync();
        }
    }
}
