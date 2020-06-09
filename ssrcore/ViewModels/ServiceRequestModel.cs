using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.ViewModels
{
    public class ServiceRequestModel : BaseModel
    {
        public string TicketId { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public string ServiceId { get; set; }
        public string ServiceNm { get; set; }
        public int? StaffId { get; set; }
        public string Staff { get; set; }
        public string Content { get; set; }
        public DateTime DueDateTime { get; set; }
        public string Status { get; set; }
    }
}
