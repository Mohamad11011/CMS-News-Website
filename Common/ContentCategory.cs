using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ContentCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, MinLength(3, ErrorMessage = "Minimum length is 3")]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }
}
