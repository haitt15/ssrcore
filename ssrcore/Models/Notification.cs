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
        public int FromUser { get; set; }
        public int ToUser { get; set; }
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

        [ForeignKey(nameof(FromUser))]
        [InverseProperty(nameof(Users.NotificationFromUserNavigation))]
        public virtual Users FromUserNavigation { get; set; }
        [ForeignKey(nameof(ToUser))]
        [InverseProperty(nameof(Users.NotificationToUserNavigation))]
        public virtual Users ToUserNavigation { get; set; }
    }
}
