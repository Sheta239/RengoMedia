using RengoMediaTask.Data.DomianModels;
using RengoMediaTask.Data.Repositories;

namespace RengoMediaTask.Data.Services
{
    public class ReminderService
    {
        private readonly IReminderRepository _repository;

        public ReminderService(IReminderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Reminder>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Reminder> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Reminder reminder)
        {
            await _repository.AddAsync(reminder);
        }

        public async Task UpdateAsync(Reminder reminder)
        {
            await _repository.UpdateAsync(reminder);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
