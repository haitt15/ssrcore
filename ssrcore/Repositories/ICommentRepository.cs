using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.ViewModels;
using System.Threading.Tasks;

namespace ssrcore.Repositories
{
    public interface ICommentRepository
    {
        Task<PagedList<CommentModel>> GetAllByTicketId(SearchCommentModel model);
        Task<CommentModel> GetByIdToModel(int commentId);
        Task<Comment> GetByIdToEntity(int commentId);
        Task Create(Comment comment);
        void Update(Comment comment);
        void Delete(Comment comment);
    }
}
