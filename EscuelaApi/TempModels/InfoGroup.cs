using System;
using System.Collections.Generic;

namespace SchoolApi.TempModels;

public partial class InfoGroup
{
    public string CodeGroup { get; set; } = null!;

    public string Label { get; set; } = null!;

    public virtual ICollection<Student> StudentDnis { get; set; } = new List<Student>();
}
