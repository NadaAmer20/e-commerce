using first.Models;
using first.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace first.Controllers
{
	public class ReviewController : Controller
	{

		Context context;

		// GET: Review
		public ReviewController(Context _context)
		{
			context = _context;
		}
		public IActionResult Index()
		{
			var reviews = context.Reviews.ToList();
			return View(reviews);
		}

		// GET: Review/Details/5
		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var review = context.Reviews
				.FirstOrDefault(m => m.Id == id);
			if (review == null)
			{
				return NotFound();
			}

			return View(review);
		}

		// GET: Review/Create
		
		public IActionResult Create(int id)
		{			
			int userId = (int)HttpContext.Session.GetInt32("UserId");


			return View(new ReviewViewModel(id,userId));//object
		}

		// POST: Review/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ReviewViewModel _review)// form , p.id//object 
		{
			if (ModelState.IsValid)
			{
				
				// Create a new review object
				var review = new Review
				{
					Rating = _review.Rating,
					Comment = _review.Comment,
					ReviewDate = DateTime.Now,
					UserId = _review.UserId,
					ProductId = _review.ProductId,
				};

				// Save the review to the database
				context.Reviews.Add(review);
				context.SaveChanges();
				return RedirectToAction("UserHome","User");
			}
			return View(_review);
		}

		// GET: Review/Edit/5
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var review = context.Reviews.Find(id);
			if (review == null)
			{
				return NotFound();
			}
			return View(review);
		}

		// POST: Review/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, Review review)
		{
			if (id != review.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				context.Update(review);
				context.SaveChanges();
			}
			return RedirectToAction(nameof(Index));

		}

		// GET: Review/Delete/5
		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var review = context.Reviews.FirstOrDefault(m => m.Id == id);
			if (review == null)
			{
				return NotFound();
			}

			return View(review);
		}

		// POST: Review/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var review = context.Reviews.Find(id);
			context.Reviews.Remove(review);
			context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		private bool ReviewExists(int id)
		{
			return context.Reviews.Any(e => e.Id == id);
		}
	}
}