using Microsoft.EntityFrameworkCore;
using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task Create(Role role)
        {
            await _context.Role.AddAsync(role);
        }

        public Role GetRole(Users user)
        {
            //string role = await _context.Users.Where(u => u.Username == user.Username)
            //                         .Select(s => s.Role.RoleNm).SingleOrDefaultAsync();
            //return role;
            var role =  _context.Role.Where(u => u.RoleId == user.RoleId).SingleOrDefault();
            return role;
        }

    }
}
