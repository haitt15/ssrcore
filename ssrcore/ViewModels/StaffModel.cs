using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.ViewModels
{
    public class StaffModel : BaseModel
    {
        public int StaffId { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentNm { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
    }
}
