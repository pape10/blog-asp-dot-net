using blog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.ViewModels
{
    public class CommentEditModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Com { get; set; }
        [Required]
        public int BlogId { get; set; }
    }
}
