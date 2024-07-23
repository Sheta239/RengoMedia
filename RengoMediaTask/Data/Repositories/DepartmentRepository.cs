using Microsoft.EntityFrameworkCore;
using RengoMediaTask.Data.DbContexts;
using RengoMediaTask.Data.DomianModels;
namespace RengoMediaTask.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Departments
                .Include(d => d.SubDepartments)
                .Include(d => d.ParentDepartment)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
    }

}
