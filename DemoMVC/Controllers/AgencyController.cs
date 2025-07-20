using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entity;
using DemoMVC.Models.ViewModels;
using DemoMVC.Models.Process;

namespace DemoMVC.Controllers
{
    public class AgencyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExcelProcess _excelProcess = new();

        public AgencyController(ApplicationDbContext context)
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

        var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Book2.xlsx");
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var dt = _excelProcess.ExcelToDataTable(path);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            var ma = dt.Rows[i][0]?.ToString()?.Trim();
            var ten = dt.Rows[i][1]?.ToString()?.Trim();
            var diachi = dt.Rows[i][2]?.ToString()?.Trim();
            var nguoidd = dt.Rows[i][3]?.ToString()?.Trim();
            var dienthoai = dt.Rows[i][4]?.ToString()?.Trim();
            var mahtpp = dt.Rows[i][5]?.ToString()?.Trim();

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(mahtpp)) continue;

            if (!_context.DaiLys.Any(x => x.MaDaiLy == ma))
            {
                _context.DaiLys.Add(new DaiLy
                {
                    MaDaiLy = ma,
                    TenDaiLy = ten,
                    DiaChi = diachi ?? "",
                    NguoiDaiDien = nguoidd ?? "",
                    DienThoai = dienthoai ?? "",
                    MaHTPP = mahtpp
                });
            }
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

        public async Task<IActionResult> Index2()
        {
            var daiLyList = await _context.DaiLys
                .Include(d => d.HTPP)
                .Select(d => new DaiLyVM
                {
                    MaDaiLy = d.MaDaiLy,
                    TenDaiLy = d.TenDaiLy,
                    TenHTPP = d.HTPP != null ? d.HTPP.TenHTPP : "Không có hệ thống phân phối"
                })
                .ToListAsync();
            return View(daiLyList);
        }


        // GET: Agency
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DaiLys.Include(d => d.HTPP);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Agency/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // var daiLy = await _context.DaiLys
            var daiLy = await _context.DaiLys
                .Include(d => d.HTPP)
                .FirstOrDefaultAsync(m => m.MaDaiLy == id);
            if (daiLy == null)
            {
                return NotFound();
            }

            return View(daiLy);
        }

        // GET: Agency/Create
        public IActionResult Create()
        {
            // ViewData["MaHTPP"] = new SelectList(_context.HeThongPhanPhois, "MaHTPP", "MaHTPP");
            ViewData["MaHTPP"] = new SelectList(_context.HeThongPhanPhois, "MaHTPP", "TenHTPP");
            return View();
        }

        // POST: Agency/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDaiLy,TenDaiLy,DiaChi,NguoiDaiDien,DienThoai,MaHTPP")] DaiLy daiLy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(daiLy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHTPP"] = new SelectList(_context.HeThongPhanPhois, "MaHTPP", "MaHTPP", daiLy.MaHTPP);
            return View(daiLy);
        }

        // GET: Agency/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daiLy = await _context.DaiLys.FindAsync(id);
            if (daiLy == null)
            {
                return NotFound();
            }
            ViewData["MaHTPP"] = new SelectList(_context.HeThongPhanPhois, "MaHTPP", "MaHTPP", daiLy.MaHTPP);
            return View(daiLy);
        }

        // POST: Agency/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaDaiLy,TenDaiLy,DiaChi,NguoiDaiDien,DienThoai,MaHTPP")] DaiLy daiLy)
        {
            if (id != daiLy.MaDaiLy)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(daiLy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DaiLyExists(daiLy.MaDaiLy))
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
            ViewData["MaHTPP"] = new SelectList(_context.HeThongPhanPhois, "MaHTPP", "MaHTPP", daiLy.MaHTPP);
            return View(daiLy);
        }

        // GET: Agency/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daiLy = await _context.DaiLys
                .Include(d => d.HTPP)
                .FirstOrDefaultAsync(m => m.MaDaiLy == id);
            if (daiLy == null)
            {
                return NotFound();
            }

            return View(daiLy);
        }

        // POST: Agency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var daiLy = await _context.DaiLys.FindAsync(id);
            if (daiLy != null)
            {
                _context.DaiLys.Remove(daiLy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DaiLyExists(string id)
        {
            return _context.DaiLys.Any(e => e.MaDaiLy == id);
        }
    }
}
