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
    public class TaxonomyController : Controller
    {

        public List<Taxonomy> tax { get; set; }
        public Taxonomy tax1 { get; set; }

        private readonly ActionServices services;

        public TaxonomyController(ActionServices _services)
        {
            tax = new List<Taxonomy>();
            tax1 = new Taxonomy();
            this.services = _services;
        }

        //List Taxonomy
        public async Task<IActionResult> Index()
        {
            var taxonomy = services.SortTaxonomy();
                                           
            return View(await taxonomy);
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
        public async Task<IActionResult> Create(Taxonomy taxonomy)
        {

            var target_tax = await services.FirstTaxonomyNameAsync(taxonomy.Name);
            if (target_tax != null)
            {
                ModelState.AddModelError("", "The Taxonomy already exists.");
                return View(taxonomy);
            }

            services.AddTaxonomy(taxonomy);
            await services.DataSaveAsync();

            TempData["Success"] = "The Taxonomy has been added!";

            return RedirectToAction("Index");

        }
        //**--**--**--**//

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            Taxonomy tax = await services.FindTaxonomyAsync(id);
            if (tax == null)
            {
                return NotFound();
            }

            return View(tax);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Taxonomy taxonomy)
        {

                var taxonomy1 = await services.WhereTaxonomyAsync(id);
                if(taxonomy1==null)
                {
                ModelState.AddModelError("", "The Taxonomy already Exists!");
                return View(taxonomy);
                }
                services.UpdateTaxonomy(taxonomy);
                await services.DataSaveAsync();


                TempData["Success"] = "The Taxonomy has been edited!";

                return RedirectToAction("Index");

        }

        //**--**--**--**//

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            Taxonomy tax = await services.FindTaxonomyAsync(id);

            if (tax == null)
            {
                TempData["Error"] = "The Taxonomy does not exist!";
            }
            else
            {
                services.RemoveTaxonomy(tax);
                await services.DataSaveAsync();

                TempData["Success"] = "The Taxonomy has been deleted!";
            }

            return RedirectToAction("Index");
        }
        //**--**--**--**// 

    }
}
