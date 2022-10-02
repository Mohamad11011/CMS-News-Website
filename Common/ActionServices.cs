using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace Common
{
    public class ActionServices
    {
        private readonly ApplicationDbContext _context;
        public List<Post> pos { get; set; }
        public List<PostTerm> pterm { get; set; }
        public List<Post> allpost { get; set; }
        public ActionServices(ApplicationDbContext context)
        {
            pos = new List<Post>();
            pterm = new List<PostTerm>();
            this._context = context;
        }

        public void DataSave()
        {
            _context.SaveChanges();
        }
        public async Task DataSaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        //Post---->
        public List<Post> FindPost(int id)
        {
            var post = _context.Post.First(x => x.PostId == id);
           var ptype = _context.ContentCategory.First(x => x.CategoryId == post.CategoryId);
            
            return _context.Post.Where(x => x.CategoryId == ptype.CategoryId).ToList();
            
        }
        public async Task<Post> getPostIdAsync(int id)
        {
            Post pos=await _context.Post.FirstOrDefaultAsync(x => x.PostId == id);
            
            return pos;
        }
        public async Task<Post> FirstPostTitleAsync(string pos)
        {
            var target_pos = await _context.Post.FirstOrDefaultAsync(x => x.Title == pos);
            return target_pos;
        }
        public async Task <Post> FindPostIdAsync(int id)
        {
            Post pos = await _context.Post.FindAsync(id);
            return pos;
        }
        public IOrderedQueryable<Post> OrderPost()
        {
            var pos = _context.Post.OrderByDescending(x => x.CreationDate);
            return pos;
        }
        public void RemovePost(Post pos)
        {
            _context.Post.Remove(pos);
        }
        public void UpdatePost(Post pos)
        {
            _context.Update(pos);
        }
        public void AddPost(Post pos)
        {

            _context.Add(pos);
        }

        //Taxonomy--->
        public void RemoveTaxonomy(Taxonomy pos)
        {
            _context.Taxonomy.Remove(pos);
        }
        public void UpdateTaxonomy(Taxonomy pos)
        {
            _context.Update(pos);
        }
        public void AddTaxonomy(Taxonomy pos)
        {

            _context.Add(pos);
        }
        public void TaxonomyDes()
        {

             _context.Taxonomy.OrderByDescending(x => x.TaxonomyId);
        }
        public async Task<Taxonomy> FindTaxonomyAsync(int id)
        {
            Taxonomy tax = await _context.Taxonomy.FindAsync(id);
            return tax;
        }
        public async Task<Taxonomy> WhereTaxonomyAsync(int id)
        {
            var tax = await _context.Taxonomy.Where(x => x.TaxonomyId != id).FirstOrDefaultAsync();
            return tax;
        }
        public async Task<Taxonomy> FirstTaxonomyAsync(string name)
        {
            var tax = await _context.Taxonomy.FirstOrDefaultAsync(x => x.Name == name);
            return tax;
        }
        public IEnumerable<Taxonomy> SortTaxonomyViewBag()
        {
            IQueryable<Taxonomy> pos = _context.Taxonomy.OrderBy(x => x.TaxonomyId);
            return pos;
        }
        public async Task<List<Taxonomy>> SortTaxonomy()
        {
            List<Taxonomy> taxonomy = await _context.Taxonomy.OrderBy(x => x.TaxonomyId).ToListAsync();
            return taxonomy;
        }
        public async Task<Taxonomy> FirstTaxonomyNameAsync(string name)
        {
            var tax = await _context.Taxonomy.FirstOrDefaultAsync(x => x.Name == name);
            return tax;
        }



        //Content Category-->
        public ContentCategory getCategoryId(int id)
        {
            ContentCategory c = _context.ContentCategory.First(x => x.CategoryId == id);
            return c;
        }
        public async Task<List<ContentCategory>> SortContentCategory()
        {
            List<ContentCategory> contents = await _context.ContentCategory.OrderBy(x => x.CategoryId).ToListAsync();
            return contents;
        }
        public IEnumerable<ContentCategory> GetContentCategory()
        {
            IQueryable<ContentCategory> categories = _context.ContentCategory.OrderBy(x => x.CategoryId);
            return categories;
        }
        public async Task<ContentCategory> FirstCategoryNameAsync(string name)
        {
            var category = await _context.ContentCategory.FirstOrDefaultAsync(x => x.CategoryName == name);
            return category;
        }
        public async Task<ContentCategory> FindCategoryAsync(int id)
        {
            ContentCategory tax = await _context.ContentCategory.FindAsync(id);
            return tax;
        }
        public void AddCategory(ContentCategory pos)
        {

            _context.Add(pos);
        }
        public void RemoveCategory(ContentCategory pos)
        {
            _context.ContentCategory.Remove(pos);
        }
        public void UpdateCategory(ContentCategory pos)
        {
            _context.Update(pos);
        }
        public async Task<ContentCategory> WhereCategoryAsync(int id)
        {
            var tax = await _context.ContentCategory.Where(x => x.CategoryId != id).FirstOrDefaultAsync();
            return tax;
        }
        public  List<ContentCategory> FindCategoryName(int id)
        {
             Post pos =  _context.Post.First(x => x.PostId == id);
             List<ContentCategory> content = _context.ContentCategory.Where(x => x.CategoryId == pos.CategoryId).ToList();

            return content;
        }

        public IQueryable<ContentCategory> CheckAllCategory()
        {
            var cat = _context.ContentCategory.Where(x => x.CategoryId != null);
            return cat;
        }

        //--------------------


        //Term---->
        public Term FindTermId(int id)
        {
           Term ter= _context.Term.First(x => x.TermId == id);
            return ter;
        }
        public void RemoveTerm(Term pos)
        {
            _context.Term.Remove(pos);
        }
        public void UpdateTerm(Term pos)
        {
            _context.Update(pos);
        }
        public void AddTerm(Term pos)
        {

            _context.Add(pos);
        }
        public Term getTermId(int id)
        {
            Term t = _context.Term.First(x => x.TermId == id);
            return t;
        }
        public async Task<Term> getTermIdAsync(int id)
        {
            Term ter = await _context.Term.FindAsync(id);
            return ter;
        }
        public List<Term> ListTerms(int id)
        {
            var pos = _context.PostType.First(x => x.PostTypeId == id);
            //Postype==> Taxonomy 
            var tax = _context.Taxonomy.First(x => x.TaxonomyId == pos.TaxonomyId);
            //Taxonomy==> Terms
            List<Term> terms = new List<Term>();
            return   _context.Term.Where(x => x.TaxonomyId == tax.TaxonomyId).ToList();
        }

        public async Task<List<Term>> SortTermAsc()
        {
            List<Term> ter = await _context.Term.OrderBy(x => x.TermId).ToListAsync();
            return ter;
        }

        public IEnumerable<Term> SortTerm()
        {
            IQueryable< Term> ter = _context.Term.OrderBy(x => x.TermId);
            return ter;
        }
        
        public async Task<Term> FindTermAsync(string name)
        {
            var target_ter = await _context.Term.FirstOrDefaultAsync(x => x.Name == name);
            return target_ter;
        }
        public async Task<Term> WhereTermIdAsync(int id)
        {
            var ter = await _context.Term.Where(x => x.TermId != id).FirstOrDefaultAsync();
            return ter;
        }
        public async Task<Term> FirstTermName(string name)
        {
            var ter = await _context.Term.FirstOrDefaultAsync(x => x.Name == name);
            return ter;
        }
	    public IQueryable<Term> CheckAllTerm()
        {
 		var term_tar = _context.Term.Where(x => x.TermId != null); 
           return term_tar;
        }

        //PostTerm---->
        public async Task<List<PostTerm>> ListPostTerms(int id)
        { 
            pterm = await _context.PostTerm.Where(x => x.PostId == id).ToListAsync();
            return pterm;
        }
        public void RemoverPostTerm(PostTerm pos)
        {
            _context.PostTerm.Remove(pos);
        }
        public void UpdatePostTerm(PostTerm pos)
        {
            _context.Update(pos);
        }
        public void AddPostTerm(PostTerm pos)
        {

            _context.Add(pos);
        }
        public IEnumerable<PostTerm> SortPostTermViewBag()
        {

            IQueryable<PostTerm> pos = _context.PostTerm.OrderBy(x => x.PostTermId);
            return pos;

        }
        public  List<PostTerm> FindPostTerms(int id)
        {
            List<PostTerm> pterm=_context.PostTerm.Where(x => x.PostId == id).ToList();

            return pterm;
        }

        public List<PostTerm> FindPostTermNotNull()
        {
            List<PostTerm> pterm = _context.PostTerm.Where(x => x.PostId != null).ToList();


            return pterm;
        }
        public PostTerm FirstPostTerm(int id)
        {
            PostTerm pterm = _context.PostTerm.First(x => x.PostTermId == id);


            return pterm;
        }
        //PostType---->
        public PostType FirstPostTypeName(string PostTypeName)
        {
            var pname = _context.PostType.First(x => x.Title == PostTypeName);
            return pname;
        }
        public async Task<PostType> FindPostTypeIdAsync(int id)
        {
            PostType pos = await _context.PostType.FindAsync(id);
            return pos;
        }
        public async Task<PostType> FirstPostTypeIdAsync(int id)
        {
            var postype = await _context.PostType.Where(x => x.PostTypeId != id).FirstOrDefaultAsync();
            return postype;
        }
        public async Task<PostType> FirstPostTypeTitleAsync(string pos)
        {
            var target_pos = await _context.PostType.FirstOrDefaultAsync(x => x.Title == pos);
            return target_pos;
        }

        public PostType getPostTypeId(int pid)
        {
            PostType ptype = _context.PostType.First(x => x.PostTypeId == pid);
            return ptype;
        }
        public IQueryable<PostType> CheckPostType()
        {
            var postype_tar = _context.PostType.Where(x => x.PostTypeId != null);
           
            return postype_tar;
        }
        public void RemovePostType(PostType pos)
        {
            _context.PostType.Remove(pos);
        }
        public void UpdatePostType(PostType pos)
        {
            _context.Update(pos);
        }
        public void AddPostType(PostType pos)
        {

            _context.Add(pos);
        }
        public IOrderedQueryable <PostType> SortPostType()
        {
           IOrderedQueryable< PostType> pos= _context.PostType.OrderBy(x => x.PostTypeId);
            return pos;

        }
        public IEnumerable<PostType> SortPostTypeViewBag()
        {
            IQueryable<PostType> pos= _context.PostType.OrderBy(x => x.PostTypeId);
            return pos;

        }
        public IEnumerable<PostType> SortPostTypeWhere(int id)
        {
            IQueryable<PostType> pos=_context.PostType.OrderBy(x => x.PostTypeId).Where(x => x.PostTypeId == id);
            return pos;
        }

        public IEnumerable<Post> FindRelatedPostsbyTag(int termid)
        {
            var terms = _context.Term.First(x => x.TermId == termid);

            pterm = _context.PostTerm.Where(x => x.TermId == terms.TermId).ToList();
            List<Post> pts = new List<Post>();
            pts = _context.Post.Where(x => x.PostId != null).ToList();

            IEnumerable<Post> result = pts.Where(x =>pterm.Any(y => y.PostId.Equals(x.PostId)));

            return result;

        }

        public IEnumerable<Post> FindRelatedPostsbyCategory(int catid)
        {
            var postype = _context.ContentCategory.First(x => x.CategoryId == catid);
            
            List<Post> pts = new List<Post>();
            pts = _context.Post.Where(x => x.PostId != null).ToList();
            
            IEnumerable<Post> result = pts.Where(x=>x.CategoryId==postype.CategoryId);
            
            return result;

        }
        public IEnumerable<Post> FindRelatedPostsbySearch(string postname)
        {
            List<Post> pts = new List<Post>();

            pts = _context.Post.Where(x => x.PostId != null).ToList();
            if (!String.IsNullOrEmpty(postname))
            {
                pts = _context.Post.Where(s => s.Title!.Contains(postname)).ToList();
                
            }
            else
            {
                pts = _context.Post.Where(x => x.Title.Contains(postname)).ToList();
            }
            

            return pts;

        }

    }
}
