using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Data;
using blog.Models;

namespace blog.Services
{
    public class SqlCommentData : ICommentData
    {
        private ApplicationDbContext _context;

        public SqlCommentData(ApplicationDbContext context)
        {
            _context = context;

        }
        public Blog Add(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
            var blog = _context.Blogs.FirstOrDefault(r => r.Id == comment.BlogId);
            return blog;
        }
    }
}
