using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.ViewModels
{
    public class SearchDepartmentModel : ResourceParameters
    {
        public string DepartmentId { get; set; }
        public string DepartmentNm { get; set; }
    }
}
