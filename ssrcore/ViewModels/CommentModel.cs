using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.ViewModels
{
    public class CommentModel : BaseModel
    {
        public int Id { get; set; }
        public string TicketId { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
    }
}
