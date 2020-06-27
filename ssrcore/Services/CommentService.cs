using AutoMapper;
using ssrcore.Helpers;
using ssrcore.Models;
using ssrcore.UnitOfWork;
using ssrcore.ViewModels;
using System;
using System.Threading.Tasks;

namespace ssrcore.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CommentModel> CreateComment(CommentModel comment)
        {
            var user = await _unitOfWork.UserRepository.GetByUsername(comment.Username);
            if(user != null)
            {
                comment.UserId = user.Id;
                var entity = _mapper.Map<Comment>(comment);
                entity.InsBy = comment.Username;
                entity.UpdBy = comment.Username;
                await _unitOfWork.CommentRepository.Create(entity);
                await _unitOfWork.Commit();
                var modelToReturn = await _unitOfWork.CommentRepository.GetByIdToModel(entity.Id);
                return modelToReturn;
            }
            return null;
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            var entity = await _unitOfWork.CommentRepository.GetByIdToEntity(commentId);
            if (entity != null)
            {
                _unitOfWork.CommentRepository.Delete(entity);
                await _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public async Task<PagedList<CommentModel>> GetAllCommentByTicketId(SearchCommentModel model)
        {
            var comments = await _unitOfWork.CommentRepository.GetAllByTicketId(model);
            return comments;
        }

        public async Task<CommentModel> GetCommentById(int commmentId)
        {
            var comment = await _unitOfWork.CommentRepository.GetByIdToModel(commmentId);
            if (comment == null)
            {
                throw new AppException("Cannot find " + commmentId);
            }
            return comment;
        }

        public async Task<CommentModel> UpdateComment(int commentId, CommentModel model)
        {
            var entity = await _unitOfWork.CommentRepository.GetByIdToEntity(commentId);
            entity.Id = entity.Id;
            entity.TicketId = entity.TicketId;
            entity.UserId = entity.UserId;
            entity.Content = model.Content != null ? model.Content : entity.Content;
            entity.DelFlg = false;
            entity.UpdBy = model.Username;
            entity.UpdDatetime = DateTime.Now;
            await _unitOfWork.Commit();
            var modelToReturn = await _unitOfWork.CommentRepository.GetByIdToModel(commentId);
            return modelToReturn;
        }
    }
}
