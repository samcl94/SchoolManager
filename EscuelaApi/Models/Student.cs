using System;
using System.Collections.Generic;

namespace SchoolApi.Models;

public partial class Student
{
    public string Dni { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string ParentEmergencyPhone1 { get; set; } = null!;

    public string? ParentEmergencyPhone2 { get; set; }

    public virtual ICollection<InfoGroup> CodeGroups { get; set; } = new List<InfoGroup>();

    public virtual ICollection<GroupStudent> GroupStudents { get; set; } = new List<GroupStudent>();

}
