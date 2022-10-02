using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DgPadNews.Models;

namespace DgPadNews.Controllers
{
    public class PostController : Controller
    {
        private readonly ActionServices services;
        private readonly IWebHostEnvironment webHostEnvironment;
        public List<PostTerm> pterm { get; set; }
        public List<Post> postList { get; set; }
        public PostController(IWebHostEnvironment webHostEnvironment, ActionServices _services)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.services = _services;

        }

        //List Post
        public async Task<IActionResult> Index()
        {
            var postype_tar = services.CheckPostType();
            var target_tar = services.CheckAllTerm();
            var target_cat = services.CheckAllCategory();


            //Postypes list
            List<PostType> ptypes = new List<PostType>();
            foreach (PostType i in postype_tar)
            {
                int pid = i.PostTypeId;

                PostType ptype = services.getPostTypeId(pid);

                ptypes.Add(new PostType
                {
                    Title = ptype.Title,
                    PostTypeId = ptype.PostTypeId,
                    TaxonomyId = ptype.TaxonomyId

                });

            }
            ViewBag.Ptypes = ptypes;
            //-------->


            //Terms List
            List<Term> terms = new List<Term>();
            foreach (Term i in target_tar)
            {
                int pid = i.TermId;

                Term tertype = services.getTermId(pid);

                terms.Add(new Term
                {
                    Name = tertype.Name,
                    TermId = tertype.TermId,
                    TaxonomyId = tertype.TaxonomyId

                });

            }
            ViewBag.TermTags = terms;
            //-------->


            //Category 
            List<ContentCategory> categorize = new List<ContentCategory>();
            foreach (ContentCategory i in target_cat)
            {
                int pid = i.CategoryId;

                ContentCategory contentCategory = services.getCategoryId(pid);

                categorize.Add(new ContentCategory
                {
                    CategoryId=contentCategory.CategoryId,
                    CategoryName=contentCategory.CategoryName
                });

            }
            ViewBag.Category = categorize;
            //-------->

            //Posts & Terms through PostTerm Table
            List<PostTerm> postTerms = new List<PostTerm>();
            pterm = services.FindPostTermNotNull();
            foreach (PostTerm i in pterm)
            {
                int targetid = i.PostTermId;
                PostTerm t = services.FirstPostTerm(targetid);

                postTerms.Add(new PostTerm
                {
                    TermId = t.TermId, 
                    PostId = t.PostId,
                    PostTermId=t.PostTermId
                });
            }
            ViewBag.PostTerms = postTerms;
            //-------->

            var pos = services.OrderPost();

            return View(pos.ToList());


        }
        //**--**--**--**//


        // Details
        public async Task<IActionResult> Details(int id)
        {
            //Category 
            var target_cat = services.CheckAllCategory();
            List<ContentCategory> categorize = new List<ContentCategory>();
            foreach (ContentCategory i in target_cat)
            {
                int pid = i.CategoryId;

                ContentCategory contentCategory = services.getCategoryId(pid);

                categorize.Add(new ContentCategory
                {
                    CategoryId = contentCategory.CategoryId,
                    CategoryName = contentCategory.CategoryName
                });

            }
            ViewBag.Category = categorize;
            //-------->

            ViewBag.PostTypeId = new SelectList(services.SortPostTypeViewBag(), "PostTypeId", "Title");
            ViewBag.TermId = new SelectList(services.SortPostTermViewBag(), "TermId", "Name");
            pterm = services.FindPostTerms(id);


            List<Term> terms = new List<Term>();
            foreach (PostTerm i in pterm)
            {
                int termid = i.TermId;
                Term t = services.FindTermId(termid);

                terms.Add(new Term
                {
                    Name = t.Name,
                    TermId = t.TermId

                });

            }

            ViewBag.PTerm = terms;

            //Post
            postList = services.FindPost(id);

            ViewBag.Posts = postList;

            Post pos = await services.getPostIdAsync(id);
            if (pos == null)
            {
                return NotFound();
            }
            return View(pos);
        }
        //**--**--**--**// 

        //Posts Related to Tag on click
        public async Task<IActionResult> TagFiltering(int id)
        {
            //Category 
            var target_cat = services.CheckAllCategory();
            List<ContentCategory> categorize = new List<ContentCategory>();
            foreach (ContentCategory i in target_cat)
            {
                int pid = i.CategoryId;

                ContentCategory contentCategory = services.getCategoryId(pid);

                categorize.Add(new ContentCategory
                {
                    CategoryId = contentCategory.CategoryId,
                    CategoryName = contentCategory.CategoryName
                });

            }
            ViewBag.Category = categorize;
            //-------->
            
            var pos = services.FindRelatedPostsbyTag(id);
            if (pos == null)
            {
                return NotFound();
            }
            return View(pos.ToList());

        }

        //Posts Related to Category on click
        public async Task<IActionResult> CategoryFiltering(int id)
        {
            //Category 
            var target_cat = services.CheckAllCategory();
            List<ContentCategory> categorize = new List<ContentCategory>();
            foreach (ContentCategory i in target_cat)
            {
                int pid = i.CategoryId;

                ContentCategory contentCategory = services.getCategoryId(pid);

                categorize.Add(new ContentCategory
                {
                    CategoryId = contentCategory.CategoryId,
                    CategoryName = contentCategory.CategoryName
                });

            }
            ViewBag.Category = categorize;
            //-------->
           
            var pos = services.FindRelatedPostsbyCategory(id);

            if (pos == null)
            {
                return NotFound();
            }
            return View(pos.ToList());

        }

        public ActionResult Search(string postname)
        {
            //Category 
            var target_cat = services.CheckAllCategory();
            List<ContentCategory> categorize = new List<ContentCategory>();
            foreach (ContentCategory i in target_cat)
            {
                int pid = i.CategoryId;

                ContentCategory contentCategory = services.getCategoryId(pid);

                categorize.Add(new ContentCategory
                {
                    CategoryId = contentCategory.CategoryId,
                    CategoryName = contentCategory.CategoryName
                });

            }
            ViewBag.Category = categorize;
            //-------->
           
            var model = services.FindRelatedPostsbySearch(postname);
            ViewBag.Search = postname;
            return View(model);
        }

        //**--**--**--**//

        public class test
        {
            public int id;
            public string name;
            public test(int i, string n)
            {
                id = i;
                name = n;
            }
        }

    }
}
