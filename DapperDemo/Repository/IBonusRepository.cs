using DapperDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Repository
{
    public interface IBonusRepository
    {
        List<Employee> GetEmployeeWithCompany(int id);

        Company GetCompanyWithEmployees(int id);

        List<Company> GetAllCompanyWithEmployees();
    }
}
 