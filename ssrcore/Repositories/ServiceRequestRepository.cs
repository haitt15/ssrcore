
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
    public class ServiceRequestRepository :BaseRepository, IServiceRequestRepository
    {
        public ServiceRequestRepository(ApplicationDbContext context) :base(context)
        {

        }

        public async Task<ServiceRequest> Create(ServiceRequestModel model)
        {
            try
            {
                var serviceRequest = new ServiceRequest
                {
                    TicketId = GenerateTicketId(),
                    UserId = model.UserId,
                    ServiceId = model.ServiceId,
                    StaffId = model.StaffId,
                    Content = model.Content,
                    DueDateTime = model.DueDateTime,
                    Status = model.Status
                };

                await _context.ServiceRequest.AddAsync(serviceRequest);
                await _context.SaveChangesAsync();
                return serviceRequest;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public async Task<PagedList<ServiceRequestModel>> GetAllServiceRequests(SearchServiceRequestModel model)
        {
            var query = _context.ServiceRequest.Where(t => (t.DelFlg == false)
                                                            && (model.Student == null || t.User.FullName.Contains(model.Student))
                                                            && (model.Status == null || t.Status == model.Status))
                                               .Select(t => new ServiceRequestModel
                                               {
                                                   TicketId = t.TicketId,
                                                   UserId = t.UserId,
                                                   User = t.User.FullName,
                                                   Content = t.Content,
                                                   ServiceId = t.ServiceId,
                                                   ServiceNm = t.Service.ServiceNm,
                                                   StaffId = t.StaffId,
                                                   Staff = t.Staff.StaffNavigation.FullName,
                                                   Status = t.Status,
                                                   DueDateTime = t.DueDateTime,
                                                   DelFlg = t.DelFlg,
                                                   InsBy = t.InsBy,
                                                   InsDatetime = t.InsDatetime,
                                                   UpdBy = t.UpdBy,
                                                   UpdDatetime = t.UpdDatetime
                                               });

            var totalCount = await _context.ServiceRequest.CountAsync();
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


            return PagedList<ServiceRequestModel>.ToPagedList(result, totalCount, model.Page, model.Size);
        }

        public async Task<ServiceRequest> GetServiceRequest(string ticketId)
        {
            return await _context.ServiceRequest.FindAsync(ticketId);
        }

        public async Task<bool> Remove(string ticketId)
        {
            var serviceRequest = await _context.ServiceRequest.FindAsync(ticketId);
            if(serviceRequest != null)
            {
                serviceRequest.DelFlg = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public void Update(ServiceRequestModel model)
        {
            _context.SaveChanges();
        }

        public string GenerateTicketId()
        {
            ServiceRequest sericeRequest = null;
            string ticketId;
            do
            {
                ticketId = Utils.RandomString(8, true);
                sericeRequest = _context.ServiceRequest.Find(ticketId);
            }
            while (sericeRequest != null);
            return ticketId;
        }
    }
}
