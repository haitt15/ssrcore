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
    public class ServiceRepository : BaseRepository, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task Create(Service service)
        {
            service.DelFlg = false;
            service.InsBy = Constants.Admin.ADMIN;
            service.InsDatetime = DateTime.Now;
            service.UpdBy = Constants.Admin.ADMIN;
            service.UpdDatetime = DateTime.Now;
            await _context.Service.AddAsync(service);
        }

        public async Task<PagedList<ServiceModel>> GetAll(SearchServicModel model)
        {
            var query = _context.Service.Where(t => (t.DelFlg == false)
                                                   && (model.ServiceNm == null || t.ServiceNm.Contains(model.ServiceNm))
                                                   && (model.DepartmentNm == null || t.Department.DepartmentNm == model.DepartmentNm)
                                                   && (model.DepartmentId == null || t.DepartmentId == model.DepartmentId))
                        .Select(t => new ServiceModel
                        {
                            ServiceId = t.ServiceId,
                            ServiceNm = t.ServiceNm,
                            DescriptionService = t.DescriptionService,
                            DepartmentId = t.DepartmentId,
                            DepartmentNm = t.Department.DepartmentNm,
                            FormLink = t.FormLink,
                            SheetLink = t.SheetLink,
                            ProcessMaxDay = t.ProcessMaxDay,
                            DelFlg = t.DelFlg,
                            InsBy = t.InsBy,
                            InsDatetime = t.InsDatetime,
                            UpdBy = t.UpdBy,
                            UpdDatetime = t.UpdDatetime
                        });

            var totalCount = await query.CountAsync();

            List<ServiceModel> result = null;

            if (model.SortBy == Constants.SortBy.SORT_NAME_ASC)
            {
                query = query.OrderBy(t => t.ServiceNm);
            }
            else if (model.SortBy == Constants.SortBy.SORT_NAME_DES)
            {
                query = query.OrderByDescending(t => t.ServiceNm);
            }

            result = await query.Skip(model.Size * (model.Page - 1))
            .Take(model.Size)
            .ToListAsync();

            return PagedList<ServiceModel>.ToPagedList(result, totalCount, model.Page, model.Size);
        }

        public async Task<ServiceModel> GetByIdToModel(string serviceId)
        {
            var result = await  _context.Service.Where(t => t.ServiceId == serviceId && t.DelFlg == false)
                                                .Select(t => new ServiceModel {
                                                    ServiceId = t.ServiceId,
                                                    ServiceNm = t.ServiceNm,
                                                    DescriptionService = t.DescriptionService,
                                                    DepartmentId = t.DepartmentId,
                                                    DepartmentNm = t.Department.DepartmentNm,
                                                    FormLink = t.FormLink,
                                                    SheetLink = t.SheetLink,
                                                    ProcessMaxDay = t.ProcessMaxDay,
                                                    DelFlg = t.DelFlg,
                                                    InsBy = t.InsBy,
                                                    InsDatetime = t.InsDatetime,
                                                    UpdBy = t.UpdBy,
                                                    UpdDatetime = t.UpdDatetime
                                                }).SingleOrDefaultAsync();
            return result;
        }

        public void Delete(Service service)
        {
            service.DelFlg = true;
        }

        public void Update(Service service)
        {
 
        }

        public async Task<Service> GetByIdToEntity(string serviceId)
        {
            var service = await _context.Service.FindAsync(serviceId);
            return service;
        }
    }
}
