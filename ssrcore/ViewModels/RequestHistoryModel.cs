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
        public string ContentHistory { get; set; }
        public DateTime UpdDatetime { get; set; }

    }
}
