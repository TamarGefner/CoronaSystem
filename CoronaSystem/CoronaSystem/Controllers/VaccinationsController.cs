using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoronaSystem.Data;
using CoronaSystem.Models;

namespace CoronaSystem.Controllers
{
    public class VaccinationsController : Controller
    {
        private readonly CoronaSystemContext _context;

        public VaccinationsController(CoronaSystemContext context)
        {
            _context = context;
        }

        // GET: Vaccinations
        public async Task<IActionResult> Index(int idNumber)
        {
            ViewData["IdNumber"] = idNumber;
            var member = _context.Member.FirstOrDefault(m => m.IdNumber == idNumber);
            ViewData["ID"] = member?.ID;
            if (_context.Vaccination != null)
            {
                var vaccinations = await _context.Vaccination.ToListAsync();
                var vaccinationId = vaccinations.Where<Vaccination>(x => x.IdNumber == idNumber);
                if (vaccinationId.Count() == 4)
                    ViewData["Finish"] = "True";
                return View(vaccinationId); 
            }
            else             
                return Problem("Entity set 'CoronaSystemContext.Vaccination'  is null.");
        }

        // GET: Vaccinations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vaccination == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccination
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vaccination == null)
            {
                return NotFound();
            }

            return View(vaccination);
        }

        // GET: Vaccinations/Create
        public IActionResult Create(int idNumber)
        {
            ViewData["IdNumber"] = idNumber;
            return View();
        }

        // POST: Vaccinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdNumber,ReleaseDate,Manufacturer")] Vaccination vaccination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccination);
                await _context.SaveChangesAsync();
                int id = vaccination.IdNumber;
                return RedirectToAction(nameof(Index), new { idNumber = id }); 
             
            }
            return View(vaccination);
        }

        // GET: Vaccinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vaccination == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccination.FindAsync(id);
            if (vaccination == null)
            {
                return NotFound();
            }
            ViewData["IdNumber"] = vaccination.IdNumber;
            return View(vaccination);
        }

        // POST: Vaccinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdNumber,ReleaseDate,Manufacturer")] Vaccination vaccination)
        {
            if (id != vaccination.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccinationExists(vaccination.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                int idN = vaccination.IdNumber;
                return RedirectToAction(nameof(Index), new { idNumber = idN });
            }
            return View(vaccination);
        }

        // GET: Vaccinations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vaccination == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccination
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vaccination == null)
            {
                return NotFound();
            }

            return View(vaccination);
        }

        // POST: Vaccinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vaccination == null)
            {
                return Problem("Entity set 'CoronaSystemContext.Vaccination'  is null.");
            }
            var vaccination = await _context.Vaccination.FindAsync(id);
            if (vaccination != null)
            {
                _context.Vaccination.Remove(vaccination);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationExists(int id)
        {
          return (_context.Vaccination?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public IActionResult GetVaccineData()
        {

            int totalMembers = _context.Member.ToList().Count;
            int membersWithoutVaccine = _context.Vaccination.ToList()
                                        .GroupBy(v => v.IdNumber) // Group by IdNumber
                                        .Select(g => g.Key) // Select distinct IdNumbers
                                        .Count();

            // Create an anonymous object to hold the data
            var vaccineData = new
            {
                TotalMembers = totalMembers,
                MembersWithoutVaccine = membersWithoutVaccine
            };

            // Return the data in JSON format
            return Json(vaccineData);
        }
    }
}
