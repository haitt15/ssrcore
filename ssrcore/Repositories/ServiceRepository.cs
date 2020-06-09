using Google.Apis.Util;
using Microsoft.EntityFrameworkCore;
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public class ServiceRepository : BaseRepository, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<bool> Create(ServiceModel model)
        {
            try
            {
                var service = new Service
                {
                    ServiceId = model.ServiceId,
                    ServiceNm = model.ServiceNm,
                    DescriptionService = model.DescriptionService,
                    FormLink = model.FormLink,
                    SheetLink = model.SheetLink,
                    ProcessMaxDay = model.ProcessMaxDay,
                    DepartmentId = model.DepartmentId,
                    DelFlg = false,
                    InsBy = Constants.Admin.ADMIN,
                    InsDatetime = DateTime.Now,
                    UpdBy = Constants.Admin.ADMIN,
                    UpdDatetime = DateTime.Now
                };

                await _context.Service.AddAsync(service);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public async Task<PagedList<ServiceModel>> GetAllServices(SearchServicModel model)
        {
            var query = _context.Service.Where(t => (t.DelFlg == false)
                                                   && (model.ServiceNm == null || t.ServiceNm.Contains(model.ServiceNm))
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
                            ProcessMaxDay = t.ProcessMaxDay
                        });

            var totalCount = await query.CountAsync();
            var result = await query
                .OrderBy(t => t.ServiceNm)
                .Skip(model.PageCount - (model.Page - 1))
                .Take(model.PageCount)
                .ToListAsync();

            return PagedList<ServiceModel>.ToPagedList(result, totalCount, model.Page, model.PageCount);
        }

        public async Task<Service> GetService(string serviceId)
        {
            return await _context.Service.FindAsync(serviceId);
        }

        public async Task<bool> Remove(string serviceId)
        {
            var service = await _context.Service.FindAsync(serviceId);
            if (service != null)
            {
                service.DelFlg = true;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public void Update(ServiceModel model)
        {
            _context.SaveChanges();
        }
    }
}
