using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.ViewModels
{
    public class RequestHistoryModel
    {
        public int Id { get; set; }
        public string TicketId { get; set; }
        public int? CommentId { get; set; }
        public string FromStatus { get; set; }
        public string ToStatus { get; set; }
        public int FromStaff { get; set; }
        public int ToStaff { get; set; }
        public DateTime UpdDatetime { get; set; }

    }
}
