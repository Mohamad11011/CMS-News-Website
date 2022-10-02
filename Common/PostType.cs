using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    public class PostType
    {
        [Key]
        public int PostTypeId { get; set; }

        [Required, MinLength(3, ErrorMessage = "Minimum length is 3")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        //FK Taxonomy----------------------//

        [Display(Name = "Taxonomy")]
        [Range(1, int.MaxValue, ErrorMessage = "Choose a Taxonomy")]
        public int? TaxonomyId { get; set; }

        [ForeignKey("TaxonomyId")]
        public virtual Taxonomy? Taxonomy { get; set; }

        public ICollection<Post> Post { get; set; }

        //--------------------------------//
    }
}
