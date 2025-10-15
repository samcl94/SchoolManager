using SchoolApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApi.Models
{
    [Table("group_student", Schema = "public")]
    public class GroupStudent
    {
        [Key, Column("code_group", Order = 0)]
        public string CodeGroup { get; set; } = null!;

        [Key, Column("student_dni", Order = 1)]
        public string StudentDni { get; set; } = null!;

        // 🔹 Propiedades de navegación (opcional)
        [ForeignKey("CodeGroup")]
        public virtual InfoGroup? InfoGroup { get; set; }

        [ForeignKey("StudentDni")]
        public virtual Student? Student { get; set; }
    }
}
