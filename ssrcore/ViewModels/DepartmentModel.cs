using ssrcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.ViewModels
{
    public class DepartmentModel : BaseModel
    {
        public string DepartmentId { get; set; }
        public string DepartmentNm { get; set; }
        public string Hotline { get; set; }
        public int? ManagerId { get; set; }
        public string Manager { get; set; }
        public string RoomNum { get; set; }
    }
}
