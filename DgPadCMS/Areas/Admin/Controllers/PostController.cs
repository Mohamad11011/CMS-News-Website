using DgPadCMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Common;
using Microsoft.AspNetCore.Authorization;

namespace DgPadCMS.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly ActionServices services;
        private readonly IWebHostEnvironment webHostEnvironment;
        public List<PostTerm> pterm { get; set; }
        public List<ContentCategory> content { get; set; }
        public PostController( IWebHostEnvironment webHostEnvironment,ActionServices _services)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.services = _services;

        }

        //List Post
        public async Task<IActionResult> Index()
        {
            var postype_tar = services.CheckPostType();
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
           
            var pos =services.OrderPost();

            return View(await pos.ToListAsync());
        }
        //**--**--**--**//

        //Create ------>
        public IActionResult Create(int id)
        {
            createModel pos = new createModel();

            ViewBag.PostTypeId = new SelectList(services.SortPostTypeWhere(id), "PostTypeId", "Title");
            ViewBag.TermId = new SelectList(services.SortTerm(), "TermId", "Name");
            ViewBag.Category = new SelectList(services.GetContentCategory(), "CategoryId", "CategoryName");
            

            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(createModel pos)
        {
            Term ter = new Term();
            PostType ps = new PostType();
            PostTerm pterm = new PostTerm();
            ViewBag.PostTypeId = new SelectList(services.OrderPost(), "PostTypeId", "Title");
            ViewBag.Category = new SelectList(services.GetContentCategory(), "CategoryId", "CategoryName");

            var target_pos = await services.FirstPostTitleAsync(pos.Title);
            if (target_pos != null)
            {
                ModelState.AddModelError("", "This Post already exists.");
                return View(pos);
            }
            string imageName = "NewPNG.png";
            if (pos.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "UploadedPhotos");
                imageName = Guid.NewGuid().ToString() + "_" + pos.ImageUpload.FileName;
                string filePath = Path.Combine(uploadsDir, imageName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                await pos.ImageUpload.CopyToAsync(fs);
                fs.Close();
            }
          
            pos.Image = imageName;

            var localDateTime = DateTime.Now;
            pos.CreationDate = localDateTime;

            //Inserting from ModelView ==> Post Table
            Post post = new Post();
            post.Title = pos.Title;
            post.CreationDate = pos.CreationDate;
            post.PostTypeId = pos.PostTypeId;
            post.Summary = pos.Summary;
            post.Article = pos.Article;
            post.CategoryId = pos.CategoryId;
            post.VideoLink = pos.VideoLink;
            post.AuthorFirst = pos.AuthorFirst;
            post.AuthorLast = pos.AuthorLast;
            post.Image = pos.Image;
            //post.PostId = pos.PostId;
            services.AddPost(post);
            services.DataSave();

            foreach (var id in pos.TermId)
            {
                var poster = new PostTerm()
                {
                    PostId = post.PostId,
                    TermId = id
                };
                services.AddPostTerm(poster);
                services.DataSave();

                //Inserting to PostTerm Table
            }
            await services.DataSaveAsync();

            TempData["Success"] = "This Post has been added!";

            return RedirectToAction("Index");


        }

        //Getting Postype data from Create Post Page
        public async Task < JsonResult> GetSubCategoryByCategoryId(int categoryId)
        {
            List<Term> dat= new List<Term>();
            dat=  services.ListTerms(categoryId);

            List<test> res = new List<test>();
            foreach (Term i in dat)
            {
                String name = i.Name;
                int id = i.TermId;
                res.Add(new test(id, name));
            }
            var json = JsonConvert.SerializeObject(res);
            return Json(json);
        }
        //**--**--**--**//

        //Getting Postype data to Filter Index Page

        public int ShowPType(string PostTypeName)
        {
            var pname = services.FirstPostTypeName(PostTypeName);
            var pid = pname.PostTypeId;

            return pid ;
        }

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.PostTypeId = new SelectList(services.SortPostTypeViewBag(), "PostTypeId", "Title");
            ViewBag.TermId = new SelectList(services.SortTerm(), "TermId", "Name");
            ViewBag.PostTerms = new SelectList(services.SortPostTypeViewBag(), "TermId", "Name");
            ViewBag.Category = new SelectList(services.GetContentCategory(), "CategoryId", "CategoryName");
            var localDateTime = DateTime.Now;
            Post pos = await services.FindPostIdAsync(id);
            pos.CreationDate = localDateTime;
            if (pos == null)
            {
                return NotFound();
            }
            createModel mm = new createModel();
            pterm = await services.ListPostTerms(id);
            ViewBag.PTerm = pterm;
            //Getting from Post ==> ViewModel
            mm.PostId = id;
            mm.Title = pos.Title;
            mm.CreationDate = pos.CreationDate;
            mm.PostTypeId = pos.PostTypeId;
            mm.Summary = pos.Summary;
            mm.Article = pos.Article;
            mm.AuthorFirst = pos.AuthorFirst;
            mm.AuthorLast = pos.AuthorLast;
            mm.Image = pos.Image;
            mm.CategoryId = pos.CategoryId;
            mm.VideoLink = pos.VideoLink;
           

            return View(mm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Admin")]

        public async Task<IActionResult> Edit(int id, createModel pos)
        {

            ViewBag.PostTypeId = new SelectList(services.SortPostTypeViewBag(), "PostTypeId", "Title");
            ViewBag.TermId = new SelectList(services.SortPostTermViewBag(), "TermId", "Name");
            ViewBag.Category = new SelectList(services.GetContentCategory(), "CategoryId", "CategoryName");
            var target_pos = await services.getPostIdAsync(id);
            
                if (target_pos == null)
                {
                    ModelState.AddModelError("", "The Post doesn't exists.");
                    return View(pos);
                }
                string imageName = "NewPNG.png";
                if (pos.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "UploadedPhotos");

                    if (!string.Equals(pos.Image, "NewPNG.png"))
                    {
                        string oldImagePath = Path.Combine(uploadsDir, pos.Image);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    imageName = Guid.NewGuid().ToString() + "_" + pos.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await pos.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                }
                pos.Image = imageName;

                var localDateTime = DateTime.Now;
                pos.CreationDate = localDateTime;
                createModel mm = new createModel();
            //Getting from ModelView ==> Post Table

            target_pos.PostId = pos.PostId;
            target_pos.Title = pos.Title;
            target_pos.CreationDate = pos.CreationDate;
            target_pos.PostTypeId = pos.PostTypeId;
            target_pos.Summary = pos.Summary;
            target_pos.Article = pos.Article;
            target_pos.AuthorFirst = pos.AuthorFirst;
            target_pos.AuthorLast = pos.AuthorLast;
            target_pos.CategoryId = pos.CategoryId;
            target_pos.VideoLink = pos.VideoLink;
            target_pos.Image = pos.Image;
            target_pos.PostTermId = 1;
            services.UpdatePost(target_pos);
            await services.DataSaveAsync();

          /*  foreach (var id1 in pos.TermId)
            {
                PostTerm p = _context.PostTerm.Find(pos.PostId = id);
                p.TermId = id1;
                _context.PostTerm.Update(p);
                _context.SaveChanges();
                //Updating to PostTerm Table
            }*/
            TempData["Success"] = "This Post has been Edited!";
                return RedirectToAction("Index");

        }

        //**--**--**--**//

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            Post pos = await services.FindPostIdAsync(id);

            if (pos == null)
            {
                TempData["Error"] = "This Post does not exist!";
            }
            else
            {
                services.RemovePost(pos);
                await services.DataSaveAsync();

                TempData["Success"] = "This Post has been deleted!";
            }

            return RedirectToAction("Index");
        }
        //**--**--**--**// 

        // Details

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.PostTypeId = new SelectList(services.SortPostTypeViewBag(), "PostTypeId", "Title");
            ViewBag.TermId = new SelectList(services.SortPostTermViewBag(), "TermId", "Name");
            ViewBag.Category = new SelectList(services.GetContentCategory(), "CategoryId", "CategoryName");
            
            pterm =  services.FindPostTerms(id);
            List<Term> terms = new List<Term>();
            foreach (PostTerm i in pterm)
            {
                int termid = i.TermId;
                Term t = services.FindTermId(termid);
                terms.Add(new Term
                {
                    Name = t.Name,
                    TermId = t.TermId

                } );

            }

            ViewBag.PTerm = terms;

             content = services.FindCategoryName(id);
            
            ViewBag.CategoryFound = content;

            Post pos = await services.getPostIdAsync(id);


            if (pos == null)
            {
                return NotFound();
            }
            return View(pos);
        }
        //**--**--**--**// 

        public class test
        {
            public int id;
            public string name;
            public test(int i,string n)
            {
                id = i;
                name = n;
            }
        }

    }
}
