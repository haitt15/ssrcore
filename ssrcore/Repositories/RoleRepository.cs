using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<Role> CreateRole(RoleModel model)
        {
            var role = new Role
            {
                RoleId = model.RoleId,
                RoleNm = model.RoleNm,
                DelFlg = false,
                InsBy = "Admin",
                InsDatetime = DateTime.Now,
                UpdBy = "Admin",
                UpdDatetime = DateTime.Now
            };
            await _context.Role.AddAsync(role);
            return role;
        }

        public string FindRole(Users user)
        {
            string role = _context.Users.Where(u => u.Username == user.Username)
                                     .Select(s => s.Role.RoleNm).SingleOrDefault();
            return role;
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
