using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.ViewModels
{
    public class BlogEditModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Details { get; set; }
        [Required]
        public string Author { get; set; }
        public string Image { get; set; }
    }
}
