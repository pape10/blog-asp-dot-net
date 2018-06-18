using blog.Models;
using blog.Services;
using blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Controllers
{
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private IBlogData _blogData;
        private ICommentData _commentData;

        public CommentController(ICommentData commentData, IBlogData blogData)
        {
            _blogData = blogData;
            _commentData = commentData;
        }

        [Route("[action]/{blogId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CommentEditModel comment, int blogId)
        {
            if (ModelState.IsValid)
            {
                var newComment = new Comment();
                newComment.Name = comment.Name;
                newComment.Com = comment.Com;
                newComment.BlogId = blogId;
                var newBlog = _commentData.Add(newComment);
                return RedirectToAction("Detail", "Home", new { id = newBlog.Id });
            }
            else
            {
                return RedirectToAction("Detail", "Home", new { id = blogId });
            }
        }
    }
}
