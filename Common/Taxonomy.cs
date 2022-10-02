using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    public class Taxonomy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaxonomyId { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        [Display(Name = "Taxonomy")]
        public string Name { get; set; }

        public ICollection<Term> Term { get; set; }
        public ICollection<PostType> PostType { get; set; }


    }
}
