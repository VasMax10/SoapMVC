using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoapMVC;

namespace SoapMVC.Controllers
{
    public class SoapsController : Controller
    {
        private readonly DBSoapContext _context;

        public SoapsController(DBSoapContext context)
        {
            _context = context;
        }

        // GET: Soaps
        public async Task<IActionResult> Index()
        {
            return View(await _context.Soaps.ToListAsync());
        }

        // GET: Soaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soap = await _context.Soaps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soap == null)
            {
                return NotFound();
            }

            return View(soap);
        }

        // GET: Soaps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Soaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Size,Category,Photo,Specifics")] Soap soap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(soap);
        }

        // GET: Soaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soap = await _context.Soaps.FindAsync(id);
            if (soap == null)
            {
                return NotFound();
            }
            return View(soap);
        }

        // POST: Soaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Size,Category,Photo,Specifics")] Soap soap)
        {
            if (id != soap.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoapExists(soap.Id))
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
            return View(soap);
        }

        // GET: Soaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soap = await _context.Soaps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soap == null)
            {
                return NotFound();
            }

            return View(soap);
        }

        // POST: Soaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soap = await _context.Soaps.FindAsync(id);
            _context.Soaps.Remove(soap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoapExists(int id)
        {
            return _context.Soaps.Any(e => e.Id == id);
        }
    }
}
