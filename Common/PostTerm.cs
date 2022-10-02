using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class PostTerm
    {
        [Key]
        public int PostTermId { get; set; }

        public int PostId { get; set; }
        public Post Post {get; set;}

        public int TermId { get; set; }
        public Term Term { get; set; }

    }
}
