using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ssrcore.Services;
using ssrcore.ViewModels;

namespace ssrcore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments([FromQuery] SearchCommentModel model)
        {
            var result = await _commentService.GetAllCommentByTicketId(model);
            return Ok(new 
            { 
                data = result,
                totalCount = result.TotalCount,
                totalPages = result.TotalPages,
            });
        }

        [HttpGet("{commentId}", Name = "GetComment")]
        public async Task<ActionResult> GetComment(int commentId)
        {
            var comment = await _commentService.GetCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        public async Task<ActionResult> CreateComment(CommentModel model)
        {
            var result = await _commentService.CreateComment(model);
            if (result != null)
            {
                return Created("", result);
            }

            return BadRequest();
        }
        
        [HttpPut("{commentId}")]
        public async Task<ActionResult> UpdateComment(int commentId, [FromBody] CommentModel model)
        {
            var comment = await _commentService.GetCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _commentService.UpdateComment(commentId, model);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var result = await _commentService.DeleteComment(commentId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
        }


    }
}