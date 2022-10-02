using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    public class Term
    {
        public virtual ICollection<PostTerm> PostTerm { get; set; }


        [Key]
        public int TermId { get; set; }

        [Required, MinLength(3, ErrorMessage = "Minimum length is 3")]
        [Display(Name = "Term")]
        public string Name { get; set; }


        [Display(Name = "Taxonomy")]
        [Range(1, int.MaxValue, ErrorMessage = "Choose a Taxonomy")]
        public int? TaxonomyId { get; set; }

        [ForeignKey("TaxonomyId")]
        public virtual Taxonomy? Taxonomy { get; set; }
    }
}
