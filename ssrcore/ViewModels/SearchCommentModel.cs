using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.ViewModels
{
    public class SearchCommentModel : ResourceParameters
    {
        [Required]
        public string TicketId { get; set; }
    }
}
