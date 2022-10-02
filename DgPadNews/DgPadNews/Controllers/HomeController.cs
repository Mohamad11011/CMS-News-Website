using Common;
using DgPadNews.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DgPadNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly ActionServices services;
        private readonly IWebHostEnvironment webHostEnvironment;
        public List<PostTerm> pterm { get; set; }
        public List<Post> postList { get; set; }
        public HomeController( ActionServices _services)
        {
            this.services = _services;

        }


        public ActionResult Contactus()
        {
            //PostTypes
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

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}