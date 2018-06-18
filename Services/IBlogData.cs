using blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Services
{
    public interface IBlogData
    {
        IEnumerable<Blog> GetAll();
        Blog Get(int id);
        Blog Add(Blog blog);
        Blog Update(Blog blog);
        string Delete(int BlogId);
    }
    
}
