using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.ViewModels
{
    public class ServiceModel : BaseModel
    {
        public string ServiceId { get; set; }
        public string ServiceNm { get; set; }
        public string DescriptionService { get; set; }
        public string FormLink { get; set; }
        public string SheetLink { get; set; }
        public int ProcessMaxDay { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentNm { get; set; }
    }
}
