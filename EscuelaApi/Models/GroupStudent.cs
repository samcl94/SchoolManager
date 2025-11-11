using System;
using System.Collections.Generic;

namespace SchoolApi.Models;

public partial class GroupStudent
{
    public string CodeGroup { get; set; } = null!;

    public string StudentDni { get; set; } = null!;

    public virtual InfoGroup CodeGroupNavigation { get; set; } = null!;

    public virtual Student StudentDniNavigation { get; set; } = null!;
}
