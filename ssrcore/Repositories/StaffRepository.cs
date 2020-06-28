using Microsoft.EntityFrameworkCore;
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public class StaffRepository : BaseRepository, IStaffRepository
    {
        public StaffRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task Create(Staff staff)
        {
            staff.DelFlg = false;
            staff.InsBy = Constants.Admin.ADMIN;
            staff.InsDatetime = DateTime.Now;
            staff.UpdBy = Constants.Admin.ADMIN;
            staff.UpdDatetime = DateTime.Now;
            await _context.Staff.AddAsync(staff);
        }

        public void Delete(Staff staff)
        {
            staff.DelFlg = true;
        }

        public async Task<PagedList<UserModel>> Get(SearchStaffModel model)
        {
            var query = _context.Staff.Where(d => (d.DelFlg == false)
                        && (model.DepartmentId == null || d.DepartmentNavigation.DepartmentId == model.DepartmentId)
                        && (model.DepartmentNm == null || d.DepartmentNavigation.DepartmentNm.Contains(model.DepartmentNm))
                        && (model.Username == null || d.StaffNavigation.Username.Contains(model.Username))
                        && (model.FullName == null || d.StaffNavigation.Username.Contains(model.FullName)))
                    .Select(s => new UserModel
                    {
                        Id = s.StaffId,
                        DepartmentId = s.DepartmentId,
                        DepartmentNm = s.DepartmentNavigation.DepartmentNm,
                        Username = s.StaffNavigation.Username,
                        FullName = s.StaffNavigation.FullName,
                        DelFlg = s.DelFlg,
                        InsBy = s.InsBy,
                        InsDatetime = s.InsDatetime,
                        UpdBy = s.UpdBy,
                        UpdDatetime = s.UpdDatetime
                    });


            var totalCount = await query.CountAsync();
            List<UserModel> result = null;

            if (model.SortBy == Constants.SortBy.SORT_NAME_ASC)
            {
                query = query.OrderBy(t => t.DepartmentNm);
            }
            else if (model.SortBy == Constants.SortBy.SORT_NAME_DES)
            {
                query = query.OrderByDescending(t => t.DepartmentNm);
            }

            result = await query.Skip(model.Size * (model.Page - 1))
                                .Take(model.Size)
                                .ToListAsync();

            return PagedList<UserModel>.ToPagedList(result, totalCount, model.Page, model.Size);
        }

        public async Task<Staff> GetByIdToEntity(int staffId)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            return staff;
        }

        public async Task<UserModel> GetByIdToModel(int staffId)
        {
            var result = await _context.Staff.Where(t => t.StaffId == staffId && t.DelFlg == false)
                                                .Select(s => new UserModel
                                                {
                                                    Id = s.StaffId,
                                                    DepartmentId = s.DepartmentId,
                                                    DepartmentNm = s.DepartmentNavigation.DepartmentNm,
                                                    Username = s.StaffNavigation.Username,
                                                    FullName = s.StaffNavigation.FullName,
                                                    DelFlg = s.DelFlg,
                                                    InsBy = s.InsBy,
                                                    InsDatetime = s.InsDatetime,
                                                    UpdBy = s.UpdBy,
                                                    UpdDatetime = s.UpdDatetime
                                                }).SingleOrDefaultAsync();
            return result;
        }

        public void Update(Staff staff)
        {
            throw new NotImplementedException();
        }
    }
}
