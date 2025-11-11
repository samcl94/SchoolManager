using EscuelaApi.Models;
using SchoolApi.Models;
using System;
using System.Collections.Generic;

namespace SchoolApi.Models;

public partial class Subject
{
    public string CodeSubject { get; set; } = null!;

    public string Label { get; set; } = null!;

    public int Ordre { get; set; }

    public virtual ICollection<SubjectStudent> SubjectStudents { get; set; } = new List<SubjectStudent>();

    public virtual ICollection<GroupSubject> GroupSubjects { get; set; } = new List<GroupSubject>();

}
