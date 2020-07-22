

namespace ssrcore.ViewModels
{
    public class UserModel : BaseModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public string UserNo { get; set; }
        public string FullName { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string RoleId { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentNm { get; set; }
        public string Implementer { get; set; }
    }
}
