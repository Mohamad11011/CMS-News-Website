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
    public class TermController : Controller
    {
        public List<Term> ter { get; set; }

        private readonly ActionServices services;

        public TermController(ActionServices _services )
        {
            ter = new List<Term>();
            this.services = _services;
        }

        //List Term
        public async Task<IActionResult> Index()
        {
            var term = services.SortTermAsc();

            return View(await term);
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
        public async Task<IActionResult> Create(Term term)
        {
            ViewBag.TaxonomyId = new SelectList(services.SortTaxonomyViewBag(), "TaxonomyId", "Name");
            var target_ter = await services.FirstTermName(term.Name);
            if (target_ter != null)
            {
                ModelState.AddModelError("", "The Term already exists.");
                return View(term);
            }

            services.AddTerm(term);
            await services.DataSaveAsync();

            TempData["Success"] = "The Term has been added!";

            return RedirectToAction("Index");

        }
        //**--**--**--**//

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.TaxonomyId = new SelectList(services.SortTaxonomyViewBag(), "TaxonomyId", "Name");
            Term ter = await services.getTermIdAsync(id);
            if (ter == null)
            {
                return NotFound();
            }

            return View(ter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Term term)
        {
            ViewBag.TaxonomyId = new SelectList(services.SortTaxonomyViewBag(), "TaxonomyId", "Name");

            var ter = await services.WhereTermIdAsync(id);
            if (ter != null)
            {
                ModelState.AddModelError("", "The Taxonomy already Exists!");
                return View(term);
            }

            services.UpdateTerm(term);
            await services.DataSaveAsync();


            TempData["Success"] = "The Term has been edited!";

           return RedirectToAction("Index");
            
        }

        //**--**--**--**//

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            Term ter = await services.getTermIdAsync(id);

            if (ter == null)
            {
                TempData["Error"] = "The Term does not exist!";
            }
            else
            {
                services.RemoveTerm(ter);
                await services.DataSaveAsync();

                TempData["Success"] = "The Term has been deleted!";
            }

            return RedirectToAction("Index");
        }
        //**--**--**--**// 

    }
}
