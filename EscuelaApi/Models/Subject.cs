using SchoolApi.Models;
using System;
using System.Collections.Generic;

namespace SchoolApi.Models;

public partial class Subject
{
    public string CodeSubject { get; set; } = null!;

    public string Label { get; set; } = null!;

    public virtual ICollection<SubjectStudent> SubjectStudents { get; set; } = new List<SubjectStudent>();
}
