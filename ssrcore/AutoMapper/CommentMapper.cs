using AutoMapper;
using ssrcore.Models;
using ssrcore.ViewModels;

namespace ssrcore.AutoMapper
{
    public class CommentMapper : Profile
    {
        public CommentMapper()
        {
            CreateMap<Comment, CommentModel>();
            CreateMap<CommentModel, Comment>();
        }
    }
}
