using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RengoMediaTask.Data.DomianModels;
using RengoMediaTask.Data.Services;

namespace RengoMediaTask.Controllers
{
    public class RemindersController : Controller
    {
        private readonly ReminderService _service;

        public RemindersController(ReminderService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var reminders = await _service.GetAllAsync();
            return View(reminders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reminder reminder)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(reminder);
                // Schedule email notification logic here
                return RedirectToAction(nameof(Index));
            }

            return View(reminder);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reminder = await _service.GetByIdAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }

            return View(reminder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reminder reminder)
        {
            if (id != reminder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(reminder);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await ReminderExists(reminder.Id)))
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

            return View(reminder);
        }

        private async Task<bool> ReminderExists(int id)
        {
            var reminder = await _service.GetByIdAsync(id);
            return reminder != null;
        }
    }
}
