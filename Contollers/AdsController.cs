using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LostAndFound.Data;
using LostAndFound.Models;

namespace LostAndFound.Contollers
{
    public class AdsController : Controller
    {
        private readonly LostAndFoundContext _context;

        public AdsController(LostAndFoundContext context)
        {
            _context = context;
        }

        // GET: Ads
        public async Task<IActionResult> Index()
        {
            var lostAndFoundContext = _context.Ad.Include(a => a.PetCategory).Include(a => a.PetType);
            return View(await lostAndFoundContext.ToListAsync());
        }

        // GET: Ads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ad
                .Include(a => a.PetCategory)
                .Include(a => a.PetType)
                .FirstOrDefaultAsync(m => m.AddId == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // GET: Ads/Create
        public IActionResult Create()
        {
            ViewData["PetCategoryId"] = new SelectList(_context.Set<PetCategory>(), "PetCategoryId", "PetCategoryId");
            ViewData["PetTypeId"] = new SelectList(_context.Set<PetType>(), "PetTypeId", "PetTypeId");
            return View();
        }

        // POST: Ads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddId,PetName,Location,AdDescription,IsPopular,PetCategoryId,PetTypeId,GenderType,PhotoPath,DateOfPublish,PublisherEmail,PublisherPhone,UserId")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetCategoryId"] = new SelectList(_context.Set<PetCategory>(), "PetCategoryId", "PetCategoryId", ad.PetCategoryId);
            ViewData["PetTypeId"] = new SelectList(_context.Set<PetType>(), "PetTypeId", "PetTypeId", ad.PetTypeId);
            return View(ad);
        }

        // GET: Ads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ad.FindAsync(id);
            if (ad == null)
            {
                return NotFound();
            }
            ViewData["PetCategoryId"] = new SelectList(_context.Set<PetCategory>(), "PetCategoryId", "PetCategoryId", ad.PetCategoryId);
            ViewData["PetTypeId"] = new SelectList(_context.Set<PetType>(), "PetTypeId", "PetTypeId", ad.PetTypeId);
            return View(ad);
        }

        // POST: Ads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddId,PetName,Location,AdDescription,IsPopular,PetCategoryId,PetTypeId,GenderType,PhotoPath,DateOfPublish,PublisherEmail,PublisherPhone,UserId")] Ad ad)
        {
            if (id != ad.AddId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdExists(ad.AddId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetCategoryId"] = new SelectList(_context.Set<PetCategory>(), "PetCategoryId", "PetCategoryId", ad.PetCategoryId);
            ViewData["PetTypeId"] = new SelectList(_context.Set<PetType>(), "PetTypeId", "PetTypeId", ad.PetTypeId);
            return View(ad);
        }

        // GET: Ads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ad
                .Include(a => a.PetCategory)
                .Include(a => a.PetType)
                .FirstOrDefaultAsync(m => m.AddId == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ad = await _context.Ad.FindAsync(id);
            _context.Ad.Remove(ad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdExists(int id)
        {
            return _context.Ad.Any(e => e.AddId == id);
        }
    }
}
