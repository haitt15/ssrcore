
using Microsoft.EntityFrameworkCore;
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public class ServiceRequestRepository :BaseRepository, IServiceRequestRepository
    {
        public ServiceRequestRepository(ApplicationDbContext context) :base(context)
        {

        }

        public async Task<bool> Create(ServiceRequestModel model)
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
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public async Task<PagedList<ServiceRequestModel>> GetAllServiceRequests(SearchServiceRequestModel model)
        {
            var query = _context.ServiceRequest.Where(t => (t.DelFlg == false)
                                                            && (model.Student == null || t.User.FullName.Contains(model.Student))
                                                            && (model.Status == null || t.Status == model.Status))
                                               .Select(t => new ServiceRequestModel
                                               {
                                                   TicketId = t.TicketId,
                                                   User = t.User.FullName,
                                                   Content = t.Content,
                                                   ServiceNm = t.Service.ServiceNm,
                                                   Staff = t.Staff.StaffNavigation.FullName,
                                                   Status = t.Status,
                                                   DueDateTime = t.DueDateTime
                                               });

            var totalCount = await _context.ServiceRequest.CountAsync();
            var result = await query
                    .OrderBy(t => t.ServiceNm)
                    .Skip(model.PageCount * (model.Page - 1))
                    .Take(model.PageCount)
                    .ToListAsync();

            return PagedList<ServiceRequestModel>.ToPagedList(result, totalCount, model.Page, model.PageCount);
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
