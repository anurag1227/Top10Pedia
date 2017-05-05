using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TopTenPedia.Data;
using TopTenPediaData;

namespace TopTenPedia.Controllers
{
    public class TopTenVotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TopTenVotesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: TopTenVotes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TopTenVotes.ToListAsync());
        }

        // GET: TopTenVotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topTenVote = await _context.TopTenVotes
                .SingleOrDefaultAsync(m => m.ID == id);
            if (topTenVote == null)
            {
                return NotFound();
            }

            return View(topTenVote);
        }

        // GET: TopTenVotes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TopTenVotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description")] TopTenVote topTenVote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topTenVote);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(topTenVote);
        }

        // GET: TopTenVotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topTenVote = await _context.TopTenVotes.SingleOrDefaultAsync(m => m.ID == id);
            if (topTenVote == null)
            {
                return NotFound();
            }
            return View(topTenVote);
        }

        // POST: TopTenVotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description")] TopTenVote topTenVote)
        {
            if (id != topTenVote.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topTenVote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopTenVoteExists(topTenVote.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(topTenVote);
        }

        // GET: TopTenVotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topTenVote = await _context.TopTenVotes
                .SingleOrDefaultAsync(m => m.ID == id);
            if (topTenVote == null)
            {
                return NotFound();
            }

            return View(topTenVote);
        }

        // POST: TopTenVotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topTenVote = await _context.TopTenVotes.SingleOrDefaultAsync(m => m.ID == id);
            _context.TopTenVotes.Remove(topTenVote);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TopTenVoteExists(int id)
        {
            return _context.TopTenVotes.Any(e => e.ID == id);
        }
    }
}
