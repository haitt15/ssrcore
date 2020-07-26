using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ssrcore.Models
{
    public partial class ServiceRequest
    {
        public ServiceRequest()
        {
            Comment = new HashSet<Comment>();
        }

        [Key]
        [StringLength(11)]
        public string TicketId { get; set; }
        public int UserId { get; set; }
        [Required]
        [StringLength(10)]
        public string ServiceId { get; set; }
        public int? StaffId { get; set; }
        [StringLength(500)]
        public string Content { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DueDateTime { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
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
        public string JsonInformation { get; set; }

        [ForeignKey(nameof(ServiceId))]
        [InverseProperty("ServiceRequest")]
        public virtual Service Service { get; set; }
        [ForeignKey(nameof(StaffId))]
        [InverseProperty("ServiceRequest")]
        public virtual Staff Staff { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.ServiceRequest))]
        public virtual Users User { get; set; }
        [InverseProperty("Ticket")]
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
