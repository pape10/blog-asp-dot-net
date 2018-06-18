using blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Services
{
    public interface ICommentData
    {
       Blog Add(Comment comment);
    }
}
