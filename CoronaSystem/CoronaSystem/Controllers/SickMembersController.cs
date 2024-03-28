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
    public class SickMembersController : Controller
    {
        private readonly CoronaSystemContext _context;

        public SickMembersController(CoronaSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> CheckSickMember(int idNumber)
        {
            // Check if there are any SickMembers with the same IdNumber
            var hasSickMember = await _context.SickMember.AnyAsync(s => s.IdNumber == idNumber);

            // Return the result as JSON
            return Json(new { hasSickMember });
        }


        // GET: SickMembers
        public async Task<IActionResult> Index(int idNumber)
        {
            return _context.SickMember != null ?
                        View(await _context.SickMember.ToListAsync()) :
                        Problem("Entity set 'CoronaSystemContext.SickMember'  is null.");

        }

        // GET: SickMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SickMember == null)
            {
                return NotFound();
            }

            var sickMember = await _context.SickMember
                .FirstOrDefaultAsync(m => m.IdNumber == id);
            if (sickMember == null)
            {
                return NotFound();
            }
            var member = _context.Member.FirstOrDefault(m => m.IdNumber == id);
            ViewData["ID"] = member?.ID;
            return View(sickMember);
        }

        // GET: SickMembers/Create
        public IActionResult Create(int idNumber)
        {
            ViewData["IdNumber"] = idNumber;
            var member = _context.Member.FirstOrDefault(m=> m.IdNumber == idNumber);
            ViewData["ID"] = member?.ID;
            return View();
        }

        // POST: SickMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdNumber,ReceivingDate,RecoveryDate")] SickMember sickMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sickMember);
                await _context.SaveChangesAsync();
                var member = _context.Member.FirstOrDefault(m => m.IdNumber == sickMember.IdNumber);
                return RedirectToAction("Details", "Members", new { id = member?.ID });

            }
            return View(sickMember);
        }

        // GET: SickMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SickMember == null)
            {
                return NotFound();
            }

            var sickMember = await _context.SickMember.FindAsync(id);
            if (sickMember == null)
            {
                return NotFound();
            }
            var member = _context.Member.FirstOrDefault(m => m.IdNumber == sickMember.IdNumber);
            ViewData["ID"] = member?.ID;
            ViewData["IdNumber"] = sickMember?.IdNumber;
            return View(sickMember);
        }

        // POST: SickMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdNumber,ReceivingDate,RecoveryDate")] SickMember sickMember)
        {
            if (id != sickMember.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sickMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SickMemberExists(sickMember.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "SickMembers", new { id = sickMember.IdNumber });
            }
            return View(sickMember);
        }

        // GET: SickMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SickMember == null)
            {
                return NotFound();
            }

            var sickMember = await _context.SickMember
                .FirstOrDefaultAsync(m => m.IdNumber == id);
            if (sickMember == null)
            {
                return NotFound();
            }

            return View(sickMember);
        }

        // POST: SickMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SickMember == null)
            {
                return Problem("Entity set 'CoronaSystemContext.SickMember'  is null.");
            }
           
            var sickMember = await _context.SickMember.FindAsync(id);
            if (sickMember != null)
            {
                _context.SickMember.Remove(sickMember);
            }
            
            await _context.SaveChangesAsync();
            var member = _context.Member.FirstOrDefault(m => m.IdNumber == sickMember.IdNumber);
            return RedirectToAction("Details","Members", new { id = member?.ID });
        }

        private bool SickMemberExists(int id)
        {
          return (_context.SickMember?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        // GET: SickMembers/Graph
        public IActionResult Graph(int idNumber)
        {
            return View();
        }

        public ActionResult ShowGraph(int year = 0, int month = 0)
        {
            if (month == 0 || year == 0)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
            }

            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var activeSickMembers = _context?.SickMember?
                .Where(s => s.ReceivingDate <= endDate && s.RecoveryDate >= startDate)
                .ToList();

            int daysInMonth = DateTime.DaysInMonth(year, month);
            int[] daysArray = new int[daysInMonth];

            // Initialize daysArray with zeros
            for (int i = 0; i < daysArray.Length; i++)
            {
                daysArray[i] = 0;
            }

            // Calculate the number of active sick members for each day
            foreach (var member in activeSickMembers)
            {
                int startDay = Math.Max(member.ReceivingDate.Day, 1);
                int endDay = Math.Min(member.RecoveryDate.Day, daysInMonth);
                for (int i = startDay - 1; i < endDay; i++)
                {
                    daysArray[i]++;
                }
            }

            // Construct JSON object
            var jsonData = new
            {
                labels = Enumerable.Range(1, daysInMonth),
                data = daysArray
            };

            // Return JSON data
            return Json(jsonData);
        }


    }


}
