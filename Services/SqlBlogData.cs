using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using blog.Data;
using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.Services
{
    public class SqlBlogData : IBlogData
    {
        private ApplicationDbContext _context;

        public SqlBlogData(ApplicationDbContext context)
        {
            _context = context;
        }

        public Blog Add(Blog blog)
        {
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return blog;
        }

        public string Delete(int BlogId)
        {
            var blog = _context.Blogs.FirstOrDefault(r => r.Id == BlogId);
            string s = blog.Image;
            _context.Blogs.Remove(blog);
            
            _context.SaveChanges();
            return s;
        }

        public Blog Get(int id)
        {

            var blog= _context.Blogs.FirstOrDefault(r => r.Id == id);
            _context.Comments.Where(c => c.BlogId == id).Load();
            return blog;
        }

        public IEnumerable<Blog> GetAll()
        {
            return _context.Blogs.OrderBy(r => r.Id);
        }

        
        public Blog Update(Blog blog)
        {
            _context.Attach(blog).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return blog;
        }
    }
}
