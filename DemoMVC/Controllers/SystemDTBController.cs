using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entity;
using DemoMVC.Models.Process;

namespace DemoMVC.Controllers
{
    public class SystemDTBController : Controller
    {
        private readonly ApplicationDbContext _context;
         private readonly ExcelProcess _excelProcess = new();

        public SystemDTBController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || (Path.GetExtension(file.FileName) != ".xls" && Path.GetExtension(file.FileName) != ".xlsx"))
        {
            ModelState.AddModelError("", "Vui lòng chọn file Excel (.xls hoặc .xlsx).");
            return View();
        }

        var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "HTPP.xlsx");
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var dt = _excelProcess.ExcelToDataTable(path);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            var ma = dt.Rows[i][0]?.ToString()?.Trim();
            var ten = dt.Rows[i][1]?.ToString()?.Trim();

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten)) continue;

            if (!_context.HeThongPhanPhois.Any(x => x.MaHTPP == ma))
            {
                _context.HeThongPhanPhois.Add(new HeThongPhanPhoi
                {
                    MaHTPP = ma,
                    TenHTPP = ten
                });
            }
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
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
