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
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task Create(Comment comment)
        {
            comment.DelFlg = false;
            comment.InsDatetime = DateTime.Now;
            comment.UpdDatetime = DateTime.Now;
            await _context.Comment.AddAsync(comment);
        }

        public void Delete(Comment comment)
        {
            comment.DelFlg = true;
        }

        public async Task<PagedList<CommentModel>> GetAllByTicketId(SearchCommentModel model)
        {
            var query = _context.Comment.Where(t => t.TicketId == model.TicketId && t.DelFlg == false)
                                               .Select(t => new CommentModel
                                               {
                                                   Id = t.Id,
                                                   TicketId = t.TicketId,
                                                   Username = t.User.Username,
                                                   FullName = t.User.FullName,
                                                   Content = t.Content,
                                                   DelFlg = t.DelFlg,
                                                   InsBy = t.InsBy,
                                                   InsDatetime = t.InsDatetime,
                                                   UpdBy = t.UpdBy,
                                                   UpdDatetime = t.UpdDatetime
                                               });
            var totalCount = await query.CountAsync();

            List<CommentModel> result = null;

            if (model.SortBy == Constants.SortBy.SORT_NAME_ASC)
            {
                query = query.OrderBy(t => t.InsDatetime);
            }
            else if (model.SortBy == Constants.SortBy.SORT_NAME_DES)
            {
                query = query.OrderByDescending(t => t.InsDatetime);
            }

            result = await query.Skip(model.Size * (model.Page - 1))
                                .Take(model.Size)
                                .ToListAsync();

            return PagedList<CommentModel>.ToPagedList(result, totalCount, model.Page, model.Size);
        }

        public async Task<Comment> GetByIdToEntity(int commentId)
        {
            var comment = await _context.Comment.FindAsync(commentId);
            return comment;
        }

        public async Task<CommentModel> GetByIdToModel(int commentId)
        {
            var result = await _context.Comment.Where(t => t.Id == commentId && t.DelFlg == false)
                                               .Select(t => new CommentModel
                                               {
                                                   Id = t.Id,
                                                   TicketId = t.TicketId,
                                                   Username = t.User.Username,
                                                   FullName = t.User.FullName,
                                                   Content = t.Content,
                                                   DelFlg = t.DelFlg,
                                                   InsBy = t.InsBy,
                                                   InsDatetime = t.InsDatetime,
                                                   UpdBy = t.UpdBy,
                                                   UpdDatetime = t.UpdDatetime
                                               }).SingleOrDefaultAsync();
            return result;
        }

        public void Update(Comment comment)
        {

        }
    }
}
