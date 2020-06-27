using ssrcore.Helpers;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public interface ICommentService
    {
        Task<PagedList<CommentModel>> GetAllCommentByTicketId(SearchCommentModel model);
        Task<CommentModel> GetCommentById(int commmentId);
        Task<CommentModel> CreateComment(CommentModel comment);
        Task<bool> DeleteComment(int commentId);
        Task<CommentModel> UpdateComment(int commentId, CommentModel model);

    }
}
