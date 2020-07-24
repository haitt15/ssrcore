using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ssrcore.Models
{
    public partial class Service
    {
        public Service()
        {
            ServiceRequest = new HashSet<ServiceRequest>();
        }

        [Key]
        [StringLength(10)]
        public string ServiceId { get; set; }
        [Required]
        [StringLength(50)]
        public string ServiceNm { get; set; }
        [StringLength(500)]
        public string DescriptionService { get; set; }
        public string FormLink { get; set; }
        public string SheetLink { get; set; }
        public int ProcessMaxDay { get; set; }
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
        [InverseProperty("Service")]
        public virtual Department Department { get; set; }
        [InverseProperty("Service")]
        public virtual ICollection<ServiceRequest> ServiceRequest { get; set; }
    }
}
