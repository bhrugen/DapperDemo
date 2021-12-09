using System.Collections.Generic;
using DapperDemo.Models;

namespace DapperDemo.Repository
{
    public interface IBonusRepository
    {
        List<Employee> GetEmployeeWithCompany(int id);
        Company GetCompanyWithEmployees(int id);
        List<Company> GetAllCompanyWithEmployees();
        void AddTestCompanyWithEmployees(Company objComp);
        void AddTestCompanyWithEmployeesWithTransaction(Company objComp);
        void RemoveRange(int[] companyId);
        List<Company> FilterCompanyByName(string name);
    }
}