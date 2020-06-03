using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ssrcore.Models
{
    public partial class Staff
    {
        public Staff()
        {
            RequestHistoryFromStaffNavigation = new HashSet<RequestHistory>();
            RequestHistoryToStaffNavigation = new HashSet<RequestHistory>();
            ServiceRequest = new HashSet<ServiceRequest>();
        }

        [Key]
        public int StaffId { get; set; }
        [Required]
        [StringLength(10)]
        public string DepartmentId { get; set; }
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

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("Staff")]
        public virtual Department DepartmentNavigation { get; set; }
        [ForeignKey(nameof(StaffId))]
        [InverseProperty(nameof(Users.Staff))]
        public virtual Users StaffNavigation { get; set; }
        [InverseProperty("Manager")]
        public virtual Department Department { get; set; }
        [InverseProperty(nameof(RequestHistory.FromStaffNavigation))]
        public virtual ICollection<RequestHistory> RequestHistoryFromStaffNavigation { get; set; }
        [InverseProperty(nameof(RequestHistory.ToStaffNavigation))]
        public virtual ICollection<RequestHistory> RequestHistoryToStaffNavigation { get; set; }
        [InverseProperty("Staff")]
        public virtual ICollection<ServiceRequest> ServiceRequest { get; set; }
    }
}
