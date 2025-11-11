namespace SchoolApi.GenerateFakeData
{
    using Bogus;
    using EscuelaApi.Models;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            var faker = new Faker<InfoGroup>("es")
                .RuleFor(s => s.CodeGroup, f => $"G{f.IndexFaker + 1:D2}")
                .RuleFor(s => s.Ordre, f => f.IndexFaker + 1)
                .RuleFor(s => s.Label, f => $"GROUP {f.IndexFaker + 1:D2}");

            return faker.Generate(numberGroups);
        }

        public static List<GroupStudent> GenerateFakeGroupsStudents(List<Student> students, List<InfoGroup> groups)
        {
            var faker = new Faker("es");
            var groupStudents = new List<GroupStudent>();

            //Restriction: Each student can be only in one group
            var availableStudents = new List<Student>(students);

            foreach (var g in groups)
            {
                var memberPerGroup = faker.Random.Int(3, 30);
                var pickStudents = faker.PickRandom(availableStudents, Math.Min(memberPerGroup, availableStudents.Count)).ToList();

                foreach (var s in pickStudents)
                {
                    groupStudents.Add(new GroupStudent { CodeGroup = g.CodeGroup, StudentDni = s.Dni});

                }

                availableStudents.RemoveAll(s => pickStudents.Contains(s));
            }

            return groupStudents;

        }

        public static List<Subject> GenerateFakeSubjects(int numberSubjects)
        {

            var faker = new Faker<Subject>("es")
                .RuleFor(s => s.CodeSubject, f => $"S{f.IndexFaker + 1:D2}")
                .RuleFor(s => s.Ordre, f => f.IndexFaker + 1)
                .RuleFor(s => s.Label, f => $"Subject {f.IndexFaker + 1:D2}");

            return faker.Generate(numberSubjects);

        }

        public static List<SubjectStudent> GenerateFakeSubjectStudents(List<GroupStudent> groupStudents, List<GroupSubject> groupSubjects)
        {
            var faker = new Faker("es");
            var subjectStudents = new List<SubjectStudent>();

            //Take each student and his group
            foreach (var gs in groupStudents)
            {
                var subjectsAvailables = groupSubjects.FindAll(s => s.CodeGroup == gs.CodeGroup);
                var subjects = faker.PickRandom(subjectsAvailables, faker.Random.Int(3, subjectsAvailables.Count)).ToList();

                foreach (var s in subjects)
                {
                    subjectStudents.Add(new SubjectStudent { CodeSubject = s.CodeSubject, StudentDni = gs.StudentDni});
                }
            }
            return subjectStudents;
        }

        public static List<GroupSubject> GenerateFakeGroupsSubject(List<InfoGroup> groups, List<Subject> subjects)
        {
            var faker = new Faker("es");
            var groupsSubject = new List<GroupSubject>();

            foreach (var g in groups)
            { 
                var subjectsPerGroups =faker.Random.Int(3, 13);
                var subjectsInGroup = faker.PickRandom(subjects, subjectsPerGroups).ToList();

                foreach (var s in subjectsInGroup) 
                { 
                    groupsSubject.Add(new GroupSubject {CodeGroup = g.CodeGroup, CodeSubject = s.CodeSubject});
                }
            }

            return groupsSubject;
        }
    }

}
