using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TopTenPedia.Data;
using TopTenPediaData;
using Newtonsoft.Json;
using TopTenPedia.ViewModels;

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

            var topTenVote = await _context.TopTenVotes.Include(o=>o.Options)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (topTenVote == null)
            {
                return NotFound();
            }

			var model = new TopTenVoteViewModel()
			{
				ID = topTenVote.ID,
				Name = topTenVote.Name,
				Description = topTenVote.Description,
				Options = topTenVote.Options,
				SelectedOptionId = 0,
				TotalVotes = topTenVote.Options.Sum(o => o.VoteCount)
			};
			if (model.TotalVotes == 0)
			{
				model.TotalVotes = 1;
			}
			return View(model);
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
        public async Task<IActionResult> Create([Bind("ID,Name,Description")] TopTenVote topTenVote, string Options)
        {
			List<Option> options = JsonConvert.DeserializeObject<List<Option>>(Options);
			

			if (ModelState.IsValid)
            {
				topTenVote.Options = options;
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


		[HttpPost]
		public async Task<ActionResult> Details(TopTenVoteViewModel  viewmodel)
		{
			if (viewmodel.SelectedOptionId == 0)
			{
				ModelState.AddModelError("SelectOption","You must Select Atleast One Option");
				
			}

			if (ModelState.IsValid)
			{
				try
				{
					Option newOption = _context.Options.Single(o=>o.Id == viewmodel.SelectedOptionId);
					newOption.VoteCount++;
					_context.Update(newOption);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					//if (!TopTenVoteExists(topTenVote.ID))
					//{
					//	return NotFound();
					//}
					//else
					//{
					//	throw;
					//}
				}
				return RedirectToAction("Index");
			}
			return View();
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
