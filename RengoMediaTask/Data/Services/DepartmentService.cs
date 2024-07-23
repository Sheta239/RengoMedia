using RengoMediaTask.Data.DomianModels;
using RengoMediaTask.Data.Repositories;

namespace RengoMediaTask.Data.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Department department)
        {
            await _repository.AddAsync(department);
        }

        public async Task UpdateAsync(Department department)
        {
            await _repository.UpdateAsync(department);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
