using EscuelaApi.Models;
using System;
using System.Collections.Generic;

namespace SchoolApi.Models;

public partial class InfoGroup
{
    public string CodeGroup { get; set; } = null!;

    public string Label { get; set; } = null!;

    public int Ordre { get; set; }

    public virtual ICollection<GroupStudent> GroupStudents { get; set; } = new List<GroupStudent>();

    public virtual ICollection<GroupSubject> GroupSubjects { get; set; } = new List<GroupSubject>();

}
