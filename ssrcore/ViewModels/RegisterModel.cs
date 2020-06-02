using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.ViewModels
{
    public class RegisterModel : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserNo { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string Role { get; set; }

    }
}
