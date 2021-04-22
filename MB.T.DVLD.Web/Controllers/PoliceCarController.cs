using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MB.T.DVLD.Entities;
using MB.T.DVLD.Web.Data;

namespace MB.T.DVLD.Web.Controllers
{
    public class PoliceCarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PoliceCarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.PoliceCar.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policeCar = await _context.PoliceCar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policeCar == null)
            {
                return NotFound();
            }

            return View(policeCar);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PoliceCar policeCar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policeCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(policeCar);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policeCar = await _context.PoliceCar.FindAsync(id);
            if (policeCar == null)
            {
                return NotFound();
            }
            return View(policeCar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PoliceCar policeCar)
        {
            if (id != policeCar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policeCar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoliceCarExists(policeCar.Id))
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
            return View(policeCar);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policeCar = await _context.PoliceCar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policeCar == null)
            {
                return NotFound();
            }

            return View(policeCar);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policeCar = await _context.PoliceCar.FindAsync(id);
            _context.PoliceCar.Remove(policeCar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoliceCarExists(int id)
        {
            return _context.PoliceCar.Any(e => e.Id == id);
        }
    }
}
