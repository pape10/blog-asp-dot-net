using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Details { get; set; }
        [Required]
        public string Author { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
