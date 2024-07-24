using Department.Data;
using Department.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Department.Controllers
{
    public class HomeController : Controller
    {
        private readonly DepartmentContext _context;

        public HomeController(DepartmentContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var departments = _context.Department.Include(d => d.ParentDepartment);
            return View(await departments.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .Include(d => d.ParentDepartment)
                .Include(d => d.SubDepartments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            // Load all parent departments
            var current = department;
            while (current.ParentDepartmentId != null)
            {
                current = await _context.Department.Include(d => d.ParentDepartment).FirstOrDefaultAsync(d => d.Id == current.ParentDepartmentId);
                department.GetParentDepartments().Insert(0, current);
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            ViewData["ParentDepartmentId"] = new SelectList(_context.Department, "Id", "Name");
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Logo,ParentDepartmentId")] Department.Models.Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentDepartmentId"] = new SelectList(_context.Department, "Id", "Name", department.ParentDepartmentId);
            return View(department);
        }
        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Id == id);
        }
    }
}
