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
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepository _compRepo;
        private readonly IEmployeeRepository _empRepo;
        private readonly IBonusRepository _bonRepo;
        private readonly IDapperSprocRepo _dapperRepo;

        public CompaniesController(ICompanyRepository compRepo, IEmployeeRepository empRepo, IBonusRepository bonRepo,
            IDapperSprocRepo dapperRepo)
        {
            _compRepo = compRepo;
            _dapperRepo = dapperRepo;
            _empRepo = empRepo;
            _bonRepo = bonRepo;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View(_compRepo.GetAll());
            //return View(_dapperRepo.List<Company>("usp_GetALLCompany"));
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var company = _bonRepo.GetCompanyWithEmployees(id.GetValueOrDefault());
            if (company == null) return NotFound();

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,Name,Address,City,State,PostalCode")] Company company)
        {
            if (ModelState.IsValid)
            {
                _compRepo.Add(company);
                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            // var company = _dapperRepo.Single<Company>("usp_GetCompany", new  { CompanyId = id.GetValueOrDefault() });
            var company = _compRepo.Find(id.GetValueOrDefault());
            if (company == null) return NotFound();
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("CompanyId,Name,Address,City,State,PostalCode")] Company company)
        {
            if (id != company.CompanyId) return NotFound();

            if (ModelState.IsValid)
            {
                _compRepo.Update(company);
                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            _compRepo.Remove(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));
        }
    }
}