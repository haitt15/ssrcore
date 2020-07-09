
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
    public class ServiceRequestRepository : BaseRepository, IServiceRequestRepository
    {
        public ServiceRequestRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task Create(ServiceRequest serviceRequest)
        {
            serviceRequest.TicketId = GenerateTicketId();
            serviceRequest.DelFlg = false;
            serviceRequest.InsBy = Constants.Admin.ADMIN;
            serviceRequest.InsDatetime = DateTime.Now;
            serviceRequest.UpdBy = Constants.Admin.ADMIN;
            serviceRequest.UpdDatetime = DateTime.Now;
            await _context.ServiceRequest.AddAsync(serviceRequest);
        }

        public async Task<PagedList<ServiceRequestModel>> GetAll(SearchServiceRequestModel model)
        {
            var query = _context.ServiceRequest.Where(t => (t.DelFlg == false)
                                                            && (model.Student == null || t.User.FullName.Contains(model.Student))
                                                            && (model.Status == null || t.Status == model.Status)
                                                            && (model.DepartmentId == null || t.Service.DepartmentId == model.DepartmentId)
                                                            && (model.ServiceId == null || t.ServiceId == model.ServiceId))
                                               .Select(t => new ServiceRequestModel
                                               {
                                                   TicketId = t.TicketId,
                                                   UserId = t.UserId,
                                                   Username = t.User.Username,
                                                   FullName = t.User.FullName,
                                                   StudentPhoto = t.User.Photo,
                                                   Content = t.Content,
                                                   ServiceId = t.ServiceId,
                                                   ServiceNm = t.Service.ServiceNm,
                                                   StaffId = t.StaffId,
                                                   StaffNm = t.Staff.StaffNavigation.FullName,
                                                   StaffUsername = t.Staff.StaffNavigation.Username,
                                                   DepartmentId = t.Service.Department.DepartmentId,
                                                   DepartmentNm = t.Service.Department.DepartmentNm,
                                                   Status = t.Status,
                                                   DueDateTime = t.DueDateTime,
                                                   DelFlg = t.DelFlg,
                                                   InsBy = t.InsBy,
                                                   InsDatetime = t.InsDatetime,
                                                   UpdBy = t.UpdBy,
                                                   UpdDatetime = t.UpdDatetime
                                               });

            //var totalCount = await _context.ServiceRequest.CountAsync();
            List<ServiceRequestModel> result = null;

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
            var totalCount = result.Count;

            return PagedList<ServiceRequestModel>.ToPagedList(result, totalCount, model.Page, model.Size);
        }

        public async Task<ServiceRequestModel> GetByIdToModel(string ticketId)
        {
            var result = await _context.ServiceRequest.Where(t => t.TicketId == ticketId && t.DelFlg == false)
                                                .Select(t => new ServiceRequestModel
                                                {
                                                    TicketId = t.TicketId,
                                                    UserId = t.UserId,
                                                    Username = t.User.Username,
                                                    FullName = t.User.FullName,
                                                    StudentPhoto = t.User.Photo,
                                                    Content = t.Content,
                                                    ServiceId = t.ServiceId,
                                                    ServiceNm = t.Service.ServiceNm,
                                                    StaffId = t.StaffId,
                                                    StaffNm = t.Staff.StaffNavigation.FullName,
                                                    StaffUsername = t.Staff.StaffNavigation.Username,
                                                    DepartmentId = t.Service.Department.DepartmentId,
                                                    DepartmentNm = t.Service.Department.DepartmentNm,
                                                    Status = t.Status,
                                                    DueDateTime = t.DueDateTime,
                                                    DelFlg = t.DelFlg,
                                                    InsBy = t.InsBy,
                                                    InsDatetime = t.InsDatetime,
                                                    UpdBy = t.UpdBy,
                                                    UpdDatetime = t.UpdDatetime
                                                }).SingleOrDefaultAsync();
            return result;
        }

        public void Delete(ServiceRequest serviceRequest)
        {
            serviceRequest.DelFlg = true;
        }

        public void Update(ServiceRequest serviceRequest)
        {

        }

        public string GenerateTicketId()
        {
            ServiceRequest sericeRequest;
            string ticketId;
            do
            {
                ticketId = Utils.RandomString(8, true);
                sericeRequest = _context.ServiceRequest.Find(ticketId);
            }
            while (sericeRequest != null);
            return ticketId;
        }

        public async Task<ServiceRequest> GetByIdToEntity(string ticketId)
        {
            var result = await _context.ServiceRequest.FindAsync(ticketId);
            return result;
        }

        public async Task<IEnumerable<ServiceRequest>> GetExpiredRequest()
        {
            return await _context.ServiceRequest.Where(t => t.DelFlg == false).ToListAsync();
        }
    }
}
