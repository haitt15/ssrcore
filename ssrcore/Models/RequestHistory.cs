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
        public string ContentHistory { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdDatetime { get; set; }
    }
}
