using System;


namespace ssrcore.ViewModels
{
    public class ServiceRequestModel : BaseModel
    {
        public string TicketId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string StudentPhoto { get; set; }
        public string ServiceId { get; set; }
        public string ServiceNm { get; set; }
        public string JsonInformation { get; set; }
        public int? StaffId { get; set; }
        public string StaffUsername { get; set; }
        public string StaffNm { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentNm { get; set; }
        public string Content { get; set; }
        public DateTime DueDateTime { get; set; }
        public string Status { get; set; }
        public string implementer { get; set; }
    }
}
