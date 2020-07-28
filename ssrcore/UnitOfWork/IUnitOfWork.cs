using ssrcore.Repositories;
using System.Threading.Tasks;

namespace ssrcore.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IServiceRequestRepository ServiceRequestRepository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IFcmTokenRepository FcmTokenRepository { get; }
        ICommentRepository CommentRepository { get; }
        IStaffRepository StaffRepository { get; }
        IRequestHistoryRepository RequestHistoryRepository { get; }
        INotificationRepository NotificationRepository { get; }
        Task Commit();
    }
}
