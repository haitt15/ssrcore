using ssrcore.Models;
using ssrcore.Repositories;
using System.Threading.Tasks;

namespace ssrcore.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private IDepartmentRepository _department;
        private IServiceRepository _service;
        private IServiceRequestRepository _serviceRequest;
        private IUserRepository _user;
        private IRoleRepository _role;
        private IFcmTokenRepository _fcmToken;
        private ICommentRepository _comment;
        private IStaffRepository _staff;
        private IRequestHistoryRepository _requestHistory;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IDepartmentRepository DepartmentRepository
        {
            get 
            { 
                return _department = _department ?? new DepartmentRepository(_context); 
            }
        }

        public IServiceRepository ServiceRepository
        {
            get
            {
                return _service = _service ?? new ServiceRepository(_context);
            }
        }

        public IServiceRequestRepository ServiceRequestRepository
        {
            get
            {
                return _serviceRequest = _serviceRequest ?? new ServiceRequestRepository(_context);
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _user = _user ?? new UserRepository(_context);
            }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                return _role = _role ?? new RoleRepository(_context);
            }
        }
        public IFcmTokenRepository FcmTokenRepository
        {
            get
            {
                return _fcmToken = _fcmToken ?? new FcmTokenRepository(_context);
            }
        }

        public ICommentRepository CommentRepository
        {
            get
            {
                return _comment = _comment ?? new CommentRepository(_context);
            }
        }

        public IStaffRepository StaffRepository
        {
            get
            {
                return _staff = _staff ?? new StaffRepository(_context);
            }
        }

        public IRequestHistoryRepository RequestHistoryRepository
        {
            get
            {
                return _requestHistory = _requestHistory ?? new RequestHistoryRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
