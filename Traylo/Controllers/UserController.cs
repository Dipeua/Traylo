using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Traylo.Data;
using Traylo.Models;

namespace Traylo.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context) => _context = context;

        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .Include(u => u.City)
                .ToListAsync();
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Cities = await _context.Cities.ToListAsync();
            ViewBag.Roles = Enum.GetValues(typeof(UserRole));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cities = await _context.Cities.ToListAsync();
                ViewBag.Roles = Enum.GetValues(typeof(UserRole));
                return View(user);
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int UserId)
        {
            var u = await _context.Users.FindAsync(UserId);
            if (u == null) return NotFound();
            ViewBag.Cities = _context.Cities.ToList();
            ViewBag.Roles = Enum.GetValues(typeof(UserRole));
            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cities = _context.Cities.ToList();
                ViewBag.Roles = Enum.GetValues(typeof(UserRole));
                return View(user);
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int UserId)
        {
            var u = await _context.Users.FindAsync(UserId);
            if (u == null) return NotFound();
            return View(u);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int UserId)
        {
            var u = await _context.Users.FindAsync(UserId);
            if (u != null) _context.Users.Remove(u);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
