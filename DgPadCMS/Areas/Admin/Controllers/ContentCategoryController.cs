
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
    public class ContentCategoryController : Controller
    {

        public List<ContentCategory> tax { get; set; }
        public ContentCategory tax1 { get; set; }

        private readonly ActionServices services;

        public ContentCategoryController(ActionServices _services)
        {
            tax = new List<ContentCategory>();
            tax1 = new ContentCategory();
            this.services = _services;
        }

        //List Taxonomy
        public async Task<IActionResult> Index()
        {
            var c = services.SortContentCategory();

            return View(await c);
        }
        //**--**--**--**//

        //Create
        public IActionResult Create()
        {
            ViewBag.Categroy = new SelectList(services.GetContentCategory(), "CategoryId", "CategoryName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContentCategory category)
        {

            var target_tax = await services.FirstCategoryNameAsync(category.CategoryName);
            if (target_tax != null)
            {
                ModelState.AddModelError("", "The Category already exists.");
                return View(category);
            }

            services.AddCategory(category);
            await services.DataSaveAsync();

            TempData["Success"] = "The Category has been added!";

            return RedirectToAction("Index");

        }
        //**--**--**--**//

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            ContentCategory tax = await services.FindCategoryAsync(id);
            if (tax == null)
            {
                return NotFound();
            }

            return View(tax);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContentCategory taxonomy)
        {

            var taxonomy1 = await services.WhereCategoryAsync(id);
            if (taxonomy1 == null)
            {
                ModelState.AddModelError("", "The Category already Exists!");
                return View(taxonomy);
            }
            services.UpdateCategory(taxonomy);
            await services.DataSaveAsync();


            TempData["Success"] = "The Category has been edited!";

            return RedirectToAction("Index");

        }

        //**--**--**--**//

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            ContentCategory tax = await services.FindCategoryAsync(id);

            if (tax == null)
            {
                TempData["Error"] = "The Category does not exist!";
            }
            else
            {
                services.RemoveCategory(tax);
                await services.DataSaveAsync();

                TempData["Success"] = "The Category has been deleted!";
            }

            return RedirectToAction("Index");
        }
        //**--**--**--**// 

    }
}
