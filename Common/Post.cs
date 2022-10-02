using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    public class Post
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

        
        [Display(Name = "VideoLink")]
        public string? VideoLink { get; set; }
        //FKey Term------------------------------//

        public virtual ICollection<PostTerm> PostTerm { get; set; }
       
        [ForeignKey("PostTermId")]
        public int PostTermId { get; set; }
        //FKey PostType---------------------------//
        [ForeignKey("PostTypeId")]
        public virtual PostType PostType { get; set; }

        [Display(Name = "PostType")]
        [Range(1, int.MaxValue, ErrorMessage = "Choose a PostType")]
        public int PostTypeId { get; set; }


        //----------------------------------------//
        [ForeignKey("CategoryId")]
        public virtual ContentCategory ContentCategory { get; set; }
        public int CategoryId { get; set; }

    }
}
