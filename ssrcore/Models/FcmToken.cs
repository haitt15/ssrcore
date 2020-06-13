using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ssrcore.Models
{
    public partial class FcmToken
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        [Column("FcmToken")]
        public string FcmToken1 { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.FcmToken))]
        public virtual Users User { get; set; }
    }
}
