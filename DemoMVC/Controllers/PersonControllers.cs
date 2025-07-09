using System.Threading.Tasks;
using DemoMVC.Data;
using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers
{
    public class PersonController(ApplicationDbContext context, AutoCode autocode) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        private readonly AutoCode _autocode = autocode;
        
        public async Task<IActionResult> Index()
        {
            var model = await _context.Persons.ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var lastPerson = await _context.Persons
                .OrderByDescending(p => p.PersonID)
                .FirstOrDefaultAsync();

            var PersonID = lastPerson?.PersonID ?? "PS00";
            var newPersonID = _autocode.GeneratePersonID(PersonID);
            ViewBag.NewPersonID = newPersonID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonID,FullName,Address")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonID,FullName,Address")] Person person)
        {
            if (id != person.PersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonID))
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

            return View(person);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Persons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Persons' is null.");
            }

            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(string id)
        {
            return _context.Persons.Any(e => e.PersonID == id);
        }
    }
}
