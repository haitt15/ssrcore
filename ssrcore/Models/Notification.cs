using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ssrcore.Models
{
    public partial class Notification
    {
        [Key]
        public int Id { get; set; }
        [StringLength(120)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Content { get; set; }
        [Column("isRead")]
        public bool? IsRead { get; set; }
        public bool DelFlg { get; set; }
        [Required]
        [StringLength(50)]
        public string InsBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime InsDatetime { get; set; }
        [Required]
        [StringLength(50)]
        public string UpdBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdDatetime { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.Notification))]
        public virtual Users User { get; set; }
    }
}
