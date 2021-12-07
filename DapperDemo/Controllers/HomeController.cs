using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DapperDemo.Models;
using DapperDemo.Repository;

namespace DapperDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBonusRepository _bonRepo;

        public HomeController(ILogger<HomeController> logger, IBonusRepository bonRepo)
        {
            _logger = logger;
            _bonRepo = bonRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Company> companies = _bonRepo.GetAllCompanyWithEmployees();
            return View(companies);
        }

        public IActionResult AddTestRecords()
        {
            var company = new Company()
            {
                Name = "Test" + Guid.NewGuid().ToString(),
                Address = "test address",
                City = "test city",
                PostalCode = "test postalCode",
                State = "test state",
                Employees = new List<Employee>()
            };

            company.Employees.Add(new Employee()
            {
                Email = "test Email",
                Name = "Test Name " + Guid.NewGuid().ToString(),
                Phone = " test phone",
                Title = "Test Manager"
            });

            company.Employees.Add(new Employee()
            {
                Email = "test Email 2",
                Name = "Test Name 2" + Guid.NewGuid().ToString(),
                Phone = " test phone 2",
                Title = "Test Manager 2"
            });
            _bonRepo.AddTestCompanyWithEmployeesWithTransaction(company);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveTestRecords()
        {
            var companyIdToRemove = _bonRepo.FilterCompanyByName("Test").Select(i => i.CompanyId).ToArray();
            _bonRepo.RemoveRange(companyIdToRemove);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}