namespace SchoolApi.GenerateFakeData
{
    using Bogus;
    using Microsoft.EntityFrameworkCore.Query;
    using SchoolApi.Models;
    using System.Security.Policy;

    public static class SeedData
    {
        public static List<Student> GenerateFakeStudents(int count)
        {
            var faker = new Faker<Student>("es")
                .RuleFor(s => s.Dni, f => f.Random.Replace("########?"))
                .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                .RuleFor(s => s.LastName, f => f.Name.LastName())
                .RuleFor(s => s.BirthDate, f =>
                    DateOnly.FromDateTime(
                        f.Date.Between(new DateTime(1995, 1, 1), new DateTime(2015, 12, 31))
                    )
                )
                .RuleFor(s => s.ParentEmergencyPhone1, f => f.Phone.PhoneNumber("6########"))
                .RuleFor(s => s.ParentEmergencyPhone2, f => f.Phone.PhoneNumber("6########"));

            return faker.Generate(count);
        }

        public static List<InfoGroup> GenerateFakeGroups(int numberGroups)
        {
            int counter = 1;

            var faker = new Faker<InfoGroup>("es")
                .RuleFor(s => s.CodeGroup, f => $"G{counter++.ToString("D2")}")
                .RuleFor(s => s.Label, f => $"GROUP {counter.ToString("D2")}");

            return faker.Generate(numberGroups);
        }

        public static List<GroupStudent> GenerateFakeGroupsStudents(List<Student> students, List<InfoGroup> groups)
        {
            var faker = new Faker("es");
            var groupStudents = new List<GroupStudent>();

            foreach (var g in groups)
            {
                var memberPerGroup = faker.Random.Int(3, 30);
                var pickStudents = faker.PickRandom(students, memberPerGroup).ToList();

                foreach (var s in pickStudents)
                {
                    groupStudents.Add(new GroupStudent { CodeGroup = g.CodeGroup, StudentDni = s.Dni });

                }

            }

            return groupStudents;

        }


    }

}
