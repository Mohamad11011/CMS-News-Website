using Common;
using DgPadCMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DgPadCMS.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PostTypeController : Controller
    {
        public List<PostType> pos { get; set; }

        private readonly ActionServices services;

        public PostTypeController(ActionServices _services)
        {
            pos = new List<PostType>();
            this.services = _services;
        }

        //List PostType
        public async Task<IActionResult> Index()
        {
            var pos = services.SortPostType();

            return View(await pos.ToListAsync());
        }
        //**--**--**--**//

        //Create
        public IActionResult Create()
        {
            ViewBag.TaxonomyId = new SelectList(services.SortTaxonomyViewBag(), "TaxonomyId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostType pos )
        {
            ViewBag.TaxonomyId = new SelectList(services.SortTaxonomyViewBag(), "TaxonomyId", "Name");
            var target_pos = await services.FirstPostTypeTitleAsync(pos.Title);
            if (target_pos != null)
            {
                ModelState.AddModelError("", "The PostType already exists.");
                return View(pos);
            }
            services.AddPostType(pos);
            await services.DataSaveAsync();

            TempData["Success"] = "The PostType has been added!";

            return RedirectToAction("Index");

        }
        //**--**--**--**//

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.TaxonomyId = new SelectList(services.SortTaxonomyViewBag(), "TaxonomyId", "Name");
            PostType pos = await services.FindPostTypeIdAsync(id);
            if (pos == null)
            {
                return NotFound();
            }

            return View(pos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostType pos)
        {
            ViewBag.TaxonomyId = new SelectList(services.SortTaxonomyViewBag(), "TaxonomyId", "Name");
            var postype = await services.FirstPostTypeIdAsync(id);
            if (postype == null)
            {
                ModelState.AddModelError("", "The PostType already Exists!");
                return View(pos);
            }
            services.UpdatePostType(pos);
            await services.DataSaveAsync();

            TempData["Success"] = "The PostType has been edited!";

            return RedirectToAction("Index");

        }

        //**--**--**--**//

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            PostType pos = await services.FindPostTypeIdAsync(id);

            if (pos == null)
            {
                TempData["Error"] = "The Postype does not exist!";
            }
            else
            {
                services.RemovePostType(pos);
                await services.DataSaveAsync();

                TempData["Success"] = "The Postype has been deleted!";
            }

            return RedirectToAction("Index");
        }
        //**--**--**--**// 

    }
}
