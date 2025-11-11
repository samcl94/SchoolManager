using SchoolApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscuelaApi.Models
{
    [Table("group_subject", Schema = "public")]
    public class GroupSubject
    {
        [Key, Column("code_group", Order = 0)]
        public string CodeGroup { get; set; } = null!;

        [Key, Column("code_subject", Order = 1)]
        public string CodeSubject { get; set; } = null!;

        // 🔹 Relaciones
        [ForeignKey("CodeGroup")]
        public virtual InfoGroup? InfoGroup { get; set; }

        [ForeignKey("CodeSubject")]
        public virtual Subject? Subject { get; set; }
    }
}
