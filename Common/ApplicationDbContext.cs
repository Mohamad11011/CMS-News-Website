using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Common
{
    
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Taxonomy> Taxonomy { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostType> PostType { get; set; }
        public DbSet<Term> Term { get; set; }
        public DbSet<PostTerm> PostTerm { get; set; }
        public DbSet<ContentCategory> ContentCategory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostTerm>()
                     .HasKey(pc => pc.PostTermId);

            modelBuilder.Entity<PostTerm>()
                    .HasOne(p => p.Post)
                    .WithMany(pc => pc.PostTerm)
                    .HasForeignKey(p => p.PostId);

            modelBuilder.Entity<PostTerm>()
                    .HasOne(p => p.Term)
                    .WithMany(pc => pc.PostTerm)
                    .HasForeignKey(c => c.TermId);


        }
    }
}