using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ssrcore.Models
{
    public partial class Comment
    {
        [Key]
        public int Id { get; set; }
        [StringLength(11)]
        public string TicketId { get; set; }
        [StringLength(500)]
        public string Content { get; set; }
        public int UserId { get; set; }
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

        [ForeignKey(nameof(TicketId))]
        [InverseProperty(nameof(ServiceRequest.Comment))]
        public virtual ServiceRequest Ticket { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.Comment))]
        public virtual Users User { get; set; }
    }
}
