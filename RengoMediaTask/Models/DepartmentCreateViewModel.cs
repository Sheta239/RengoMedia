using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RengoMediaTask.Models
{
    namespace RengoMediaTask.ViewModels
    {
        public class DepartmentCreateViewModel
        {
            [Required]
            [StringLength(100)]
            public string Name { get; set; }

            public IFormFile Logo { get; set; }

            public int? ParentDepartmentId { get; set; }

            public IEnumerable<SelectListItem>? ParentDepartments { get; set; }
        }
    }
}
