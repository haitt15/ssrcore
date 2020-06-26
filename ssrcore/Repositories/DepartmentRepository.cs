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
    public class DepartmentRepository : BaseRepository, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task Create(Department department)
        {
            department.DelFlg = false;
            department.InsBy = Constants.Admin.ADMIN;
            department.InsDatetime = DateTime.Now;
            department.UpdBy = Constants.Admin.ADMIN;
            department.UpdDatetime = DateTime.Now;
            await _context.AddAsync(department);
        }

        public async Task<PagedList<DepartmentModel>> Get(SearchDepartmentModel model)
        {
            var query = _context.Department.Where(d => (d.DelFlg == false)
                        && (model.DepartmentNm == null || d.DepartmentNm.Contains(model.DepartmentNm)))
                    .Select(s => new DepartmentModel
                    {
                        DepartmentId = s.DepartmentId,
                        DepartmentNm = s.DepartmentNm,
                        Hotline = s.Hotline,
                        RoomNum = s.RoomNum,
                        Manager = s.Manager.StaffNavigation.FullName,
                        DelFlg = s.DelFlg,
                        InsBy = s.InsBy,
                        InsDatetime = s.InsDatetime,
                        UpdBy = s.UpdBy,
                        UpdDatetime = s.UpdDatetime
                    });


            var totalCount = await query.CountAsync();
            List<DepartmentModel> result = null;

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

            return PagedList<DepartmentModel>.ToPagedList(result, totalCount, model.Page, model.Size);
        }

        public async Task<DepartmentModel> GetById(string departmentId)
        {
            var result = await _context.Department.Where(t => t.DepartmentId == departmentId && t.DelFlg == false)
                                                .Select(s => new DepartmentModel
                                                {
                                                    DepartmentId = s.DepartmentId,
                                                    DepartmentNm = s.DepartmentNm,
                                                    Hotline = s.Hotline,
                                                    RoomNum = s.RoomNum,
                                                    Manager = s.Manager.StaffNavigation.FullName,
                                                    DelFlg = s.DelFlg,
                                                    InsBy = s.InsBy,
                                                    InsDatetime = s.InsDatetime,
                                                    UpdBy = s.UpdBy,
                                                    UpdDatetime = s.UpdDatetime
                                                }).SingleOrDefaultAsync();
            return result;
        }

        public void Delete(Department department)
        {
            department.DelFlg = true;
        }


        public void Update(Department department)
        {
           // var entity = _context.Department.Find(department.DepartmentId);
        }
    }
}
