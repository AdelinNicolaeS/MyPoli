using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPoli.DataAccess;
using MyPoli.Entities;

namespace MyPoli.WebApp.Controllers
{
    public class PeopleController : Controller
    {
        private readonly FifthTry2Context _context;

        public PeopleController(FifthTry2Context context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var fifthTry2Context = _context.People.Include(p => p.Gender).Include(p => p.Nationality);
            return View(await fifthTry2Context.Take(10).ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Gender)
                .Include(p => p.Nationality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");
            ViewData["NationalityId"] = new SelectList(_context.Nationalities, "Id", "Name");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Birthday,PasswordHash,RoleId,Address,Phone,Email,NationalityId,GenderId,IsDeleted")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid();
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name", person.GenderId);
            ViewData["NationalityId"] = new SelectList(_context.Nationalities, "Id", "Name", person.NationalityId);
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name", person.GenderId);
            ViewData["NationalityId"] = new SelectList(_context.Nationalities, "Id", "Name", person.NationalityId);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,LastName,Birthday,PasswordHash,RoleId,Address,Phone,Email,NationalityId,GenderId,IsDeleted")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   // _context.Update(person);
                    foreach(var cir in _context.Circumstances)
                    {
                        if(cir.EndDate < cir.StartDate)
                        {
                            cir.EndDate = cir.StartDate.AddDays(1);
                            _context.Update(cir);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name", person.GenderId);
            ViewData["NationalityId"] = new SelectList(_context.Nationalities, "Id", "Name", person.NationalityId);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Gender)
                .Include(p => p.Nationality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var person = await _context.People.FindAsync(id);
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(Guid id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
