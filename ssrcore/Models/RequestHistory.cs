using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ssrcore.Models
{
    public partial class RequestHistory
    {
        [Key]
        public int Id { get; set; }
        [StringLength(11)]
        public string TicketId { get; set; }
        public int? CommentId { get; set; }
        [Required]
        [StringLength(50)]
        public string FromStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string ToStatus { get; set; }
        public int FromStaff { get; set; }
        public int ToStaff { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdDatetime { get; set; }

        [ForeignKey(nameof(CommentId))]
        [InverseProperty("RequestHistory")]
        public virtual Comment Comment { get; set; }
        [ForeignKey(nameof(FromStaff))]
        [InverseProperty(nameof(Staff.RequestHistoryFromStaffNavigation))]
        public virtual Staff FromStaffNavigation { get; set; }
        [ForeignKey(nameof(TicketId))]
        [InverseProperty(nameof(ServiceRequest.RequestHistory))]
        public virtual ServiceRequest Ticket { get; set; }
        [ForeignKey(nameof(ToStaff))]
        [InverseProperty(nameof(Staff.RequestHistoryToStaffNavigation))]
        public virtual Staff ToStaffNavigation { get; set; }
    }
}
