using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RengoMediaTask.Data.Services;
using RengoMediaTask.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using RengoMediaTask.Data.DomianModels;
using RengoMediaTask.Models.RengoMediaTask.ViewModels;
using RengoMediaTask.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RengoMediaTask.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly DepartmentService _service;
        private readonly IWebHostEnvironment _environment;

        public DepartmentsController(DepartmentService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _service.GetAllAsync();
            return View(departments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var department = await _service.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            var subDepartments = GetSubDepartments(department);
            var parentDepartments = await GetParentDepartments(department);

            var model = new DepartmentDetailsViewModel
            {
                Department = department,
                SubDepartments = subDepartments,
                ParentDepartments = parentDepartments
            };

            return View(model);
        }

        private List<Department> GetSubDepartments(Department department)
        {
            var subDepartments = new List<Department>();
            foreach (var subDepartment in department.SubDepartments)
            {
                subDepartments.Add(subDepartment);
                subDepartments.AddRange(GetSubDepartments(subDepartment));
            }
            return subDepartments;
        }

        private async Task<List<Department>> GetParentDepartments(Department department)
        {
            var parents = new List<Department>();
            var current = department.ParentDepartment;
            while (current != null)
            {
                parents.Add(current);
                current = await _service.GetByIdAsync(current.ParentDepartmentId ?? 0);
            }
            return parents;
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _service.GetAllAsync();
            var model = new DepartmentCreateViewModel
            {
                ParentDepartments = departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                })
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department
                {
                    Name = model.Name,
                    ParentDepartmentId = model.ParentDepartmentId
                };

                if (model.Logo != null)
                {
                    var filePath = Path.Combine(_environment.WebRootPath, "images", model.Logo.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Logo.CopyToAsync(stream);
                    }
                    department.LogoPath = "/images/" + model.Logo.FileName;
                }

                await _service.AddAsync(department);
                return RedirectToAction(nameof(Index));
            }

            model.ParentDepartments = (await _service.GetAllAsync()).Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }); // In case of an error, reload the parent departments
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var department = await _service.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            var departments = await _service.GetAllAsync();
            var model = new DepartmentEditViewModel
            {
                Id = department.Id,
                Name = department.Name,
                ExistingLogoPath = department.LogoPath,
                ParentDepartmentId = department.ParentDepartmentId,
                ParentDepartments = departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var department = await _service.GetByIdAsync(model.Id);
                    if (department == null)
                    {
                        return NotFound();
                    }

                    department.Name = model.Name;
                    department.ParentDepartmentId = model.ParentDepartmentId;

                    if (model.Logo != null)
                    {
                        var filePath = Path.Combine(_environment.WebRootPath, "images", model.Logo.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.Logo.CopyToAsync(stream);
                        }
                        department.LogoPath = "/images/" + model.Logo.FileName;
                    }

                    await _service.UpdateAsync(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DepartmentExists(model.Id))
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

            model.ParentDepartments = (await _service.GetAllAsync()).Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }); // In case of an error, reload the parent departments
            return View(model);
        }


        private async Task<bool> DepartmentExists(int id)
        {
            var department = await _service.GetByIdAsync(id);
            return department != null;
        }
    }
}
