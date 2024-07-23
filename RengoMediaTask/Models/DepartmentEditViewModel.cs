using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RengoMediaTask.ViewModels
{
    public class DepartmentEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public IFormFile? Logo { get; set; }

        public string? ExistingLogoPath { get; set; }


        public int? ParentDepartmentId { get; set; }

        public IEnumerable<SelectListItem>? ParentDepartments { get; set; }
    }
}
