using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ssrcore.Models
{
    public partial class Department
    {
        public Department()
        {
            Service = new HashSet<Service>();
            Staff = new HashSet<Staff>();
        }

        [Key]
        [StringLength(10)]
        public string DepartmentId { get; set; }
        [Required]
        [StringLength(50)]
        public string DepartmentNm { get; set; }
        [StringLength(11)]
        public string Hotline { get; set; }
        public int? ManagerId { get; set; }
        [Required]
        [StringLength(6)]
        public string RoomNum { get; set; }
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

        [ForeignKey(nameof(ManagerId))]
        [InverseProperty("Department")]
        public virtual Staff Manager { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Service> Service { get; set; }
        [InverseProperty("DepartmentNavigation")]
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
