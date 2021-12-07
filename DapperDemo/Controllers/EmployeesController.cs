using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DapperDemo.Data;
using DapperDemo.Models;
using DapperDemo.Repository;

namespace DapperDemo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ICompanyRepository _compRepo;
        private readonly IEmployeeRepository _empRepo;
        private readonly IBonusRepository _bonRepo;

        [BindProperty] public Employee Employee { get; set; }

        public EmployeesController(ICompanyRepository compRepo, IEmployeeRepository empRepo, IBonusRepository bonRepo)
        {
            _compRepo = compRepo;
            _bonRepo = bonRepo;
            _empRepo = empRepo;
        }

        public async Task<IActionResult> Index(int companyId = 0)
        {
            //List<Employee> employees = _empRepo.GetAll();
            //foreach(Employee obj in employees)
            //{
            //    obj.Company = _compRepo.Find(obj.CompanyId);
            //}

            var employees = _bonRepo.GetEmployeeWithCompany(companyId);
            return View(employees);
        }


        public IActionResult Create()
        {
            var companyList = _compRepo.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.CompanyId.ToString()
            });
            ViewBag.CompanyList = companyList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePOST()
        {
            if (ModelState.IsValid)
            {
                await _empRepo.AddAsync(Employee);
                return RedirectToAction(nameof(Index));
            }

            return View(Employee);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            Employee = _empRepo.Find(id.GetValueOrDefault());
            var companyList = _compRepo.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.CompanyId.ToString()
            });
            ViewBag.CompanyList = companyList;
            if (Employee == null) return NotFound();
            return View(Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != Employee.EmployeeId) return NotFound();

            if (ModelState.IsValid)
            {
                _empRepo.Update(Employee);
                return RedirectToAction(nameof(Index));
            }

            return View(Employee);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            _empRepo.Remove(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));
        }
    }
}