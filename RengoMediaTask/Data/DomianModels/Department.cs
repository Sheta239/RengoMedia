using System.ComponentModel.DataAnnotations;

namespace RengoMediaTask.Data.DomianModels
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string LogoPath { get; set; } = string.Empty;

        public int? ParentDepartmentId { get; set; }
        public Department ParentDepartment { get; set; }
        public ICollection<Department> SubDepartments { get; set; } = new List<Department>();
    }
}
