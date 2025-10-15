using System;
using System.Collections.Generic;

namespace SchoolApi.Models;

public partial class InfoGroup
{
    public string CodeGroup { get; set; } = null!;

    public string Label { get; set; } = null!;

    public virtual ICollection<Student> StudentDnis { get; set; } = new List<Student>();

    public virtual ICollection<GroupStudent> GroupStudents { get; set; } = new List<GroupStudent>();

}
