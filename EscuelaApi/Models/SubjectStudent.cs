using System;
using System.Collections.Generic;

namespace SchoolApi.Models;

public partial class SubjectStudent
{
    public string StudentDni { get; set; } = null!;

    public string CodeSubject { get; set; } = null!;

    public virtual Subject CodeSubjectNavigation { get; set; } = null!;

    public virtual Student StudentDniNavigation { get; set; } = null!;
}
