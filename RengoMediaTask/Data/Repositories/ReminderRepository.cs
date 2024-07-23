using Microsoft.EntityFrameworkCore;
using RengoMediaTask.Data.DbContexts;
using RengoMediaTask.Data.DomianModels;

namespace RengoMediaTask.Data.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly ApplicationDbContext _context;

        public ReminderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reminder>> GetAllAsync()
        {
            return await _context.Reminders.ToListAsync();
        }

        public async Task<Reminder> GetByIdAsync(int id)
        {
            return await _context.Reminders.FindAsync(id);
        }

        public async Task AddAsync(Reminder reminder)
        {
            _context.Reminders.Add(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reminder reminder)
        {
            _context.Reminders.Update(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            _context.Reminders.Remove(reminder);
            await _context.SaveChangesAsync();
        }
    }
}
