using Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DgPadNews.Models
{
    public class ViewModel
    {
        [Key]
        public int PostId { get; set; }

        [Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        [Display(Name = "Post Title")]
        public string Title { get; set; }

        [Required, MinLength(3, ErrorMessage = "Minimum length is 3")]
        [Display(Name = "Author First Name")]
        public string AuthorFirst { get; set; }

        [Required, MinLength(3, ErrorMessage = "Minimum length is 3")]
        [Display(Name = "Author Last Name")]
        public string AuthorLast { get; set; }

        [Required]
        [Display(Name = "Post Date")]
        public DateTime CreationDate { get; set; }

        public string? Article { get; set; }

        [Required]
        [Display(Name = "Summary")]
        public string Summary { get; set; }

        public string? Image { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile? ImageUpload { get; set; }

        //FKey Term------------------------------//

        [Display(Name = "Term")]
        [Range(1, int.MaxValue, ErrorMessage = "Choose a Term")]
        public IEnumerable<int> TermId { get; set; }
        //FKey PostType---------------------------/

        [Display(Name = "PostType")]
        [Range(1, int.MaxValue, ErrorMessage = "Choose a PostType")]
        public int PostTypeId { get; set; }
    }
}
