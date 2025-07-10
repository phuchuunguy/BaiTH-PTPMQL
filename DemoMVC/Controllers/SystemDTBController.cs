using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entity;

namespace DemoMVC.Controllers
{
    public class SystemDTBController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SystemDTBController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SystemDTB
        public async Task<IActionResult> Index()
        {
            return View(await _context.HeThongPhanPhois.ToListAsync());
        }

        // GET: SystemDTB/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heThongPhanPhoi = await _context.HeThongPhanPhois
                .FirstOrDefaultAsync(m => m.MaHTPP == id);
            if (heThongPhanPhoi == null)
            {
                return NotFound();
            }

            return View(heThongPhanPhoi);
        }

        // GET: SystemDTB/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemDTB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHTPP,TenHTPP")] HeThongPhanPhoi heThongPhanPhoi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(heThongPhanPhoi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(heThongPhanPhoi);
        }

        // GET: SystemDTB/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heThongPhanPhoi = await _context.HeThongPhanPhois.FindAsync(id);
            if (heThongPhanPhoi == null)
            {
                return NotFound();
            }
            return View(heThongPhanPhoi);
        }

        // POST: SystemDTB/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaHTPP,TenHTPP")] HeThongPhanPhoi heThongPhanPhoi)
        {
            if (id != heThongPhanPhoi.MaHTPP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(heThongPhanPhoi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeThongPhanPhoiExists(heThongPhanPhoi.MaHTPP))
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
            return View(heThongPhanPhoi);
        }

        // GET: SystemDTB/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heThongPhanPhoi = await _context.HeThongPhanPhois
                .FirstOrDefaultAsync(m => m.MaHTPP == id);
            if (heThongPhanPhoi == null)
            {
                return NotFound();
            }

            return View(heThongPhanPhoi);
        }

        // POST: SystemDTB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var heThongPhanPhoi = await _context.HeThongPhanPhois.FindAsync(id);
            if (heThongPhanPhoi != null)
            {
                _context.HeThongPhanPhois.Remove(heThongPhanPhoi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeThongPhanPhoiExists(string id)
        {
            return _context.HeThongPhanPhois.Any(e => e.MaHTPP == id);
        }
    }
}
