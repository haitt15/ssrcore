using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.ViewModels
{
    public class SearchServicModel : ResourceParameters
    {
        public string ServiceId { get; set; }
        public string ServiceNm { get; set; }
        public string DepartmentId { get; set; }
    }
}
