using DotNetProjects.Data;
using DotNetProjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DotNetProjects.Controllers
{
	public class DeptsController : Controller
	{

		private readonly MyAppContext _context;

		public DeptsController(MyAppContext context) { _context = context; }
		public async Task<IActionResult> Index()
		{
			var myDepts = await _context.depts.ToListAsync();
			return View(myDepts);
		}


		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create([Bind("Id", "Description", "Price", "Location")] Dept dep)
		{
			if (ModelState.IsValid)
			{
				await _context.depts.AddAsync(dep);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(dep);

		}

		public async Task<IActionResult> Edit(int id)
		{
			var dep = await _context.depts.FirstOrDefaultAsync(x => x.id == id);
			return View(dep);
		}

		[HttpPost]
		
		public async Task<IActionResult> Edit(int id, [Bind("Id", "Description", "Price", "Location")] Dept dept)
		{
			if (ModelState.IsValid)
			{
				var existingDept = await _context.depts.FirstAsync(x => x.id == id);

				if (existingDept == null)
				{
					return NotFound(); // Handle the case where the department is not found
				}

				// Update the properties of the existing department
				existingDept.Description = dept.Description;
				existingDept.Price = dept.Price;
				existingDept.Location = dept.Location;

				try
				{
					_context.Update(existingDept);  // Update the existing entity
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					// Handle concurrency issues if multiple users are editing the same record
					
				}

				return RedirectToAction("Index");
			}

			return View(dept);
		}


		public async Task<IActionResult> Delete(int id)
		{
			var dept = await _context.depts.FindAsync(id);
			return View(dept);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult>DeleteConfirmed(int id)
		{
			var dept = await _context.depts.FindAsync(id);
			_context.depts.Remove(dept);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");

		}


		public IActionResult test() {
			return View();
		}
	}
}