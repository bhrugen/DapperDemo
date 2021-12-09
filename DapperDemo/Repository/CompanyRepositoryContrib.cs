using Dapper.Contrib.Extensions;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DapperDemo.Repository
{
    public class CompanyRepositoryContrib : ICompanyRepository
    {
        private IDbConnection db;

        public CompanyRepositoryContrib(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Company Add(Company company)
        {
            var id = db.Insert(company);
            company.CompanyId = (int)id;
            return company;
        }

        public Company Find(int id)
        {
            return db.Get<Company>(id);
        }

        public List<Company> GetAll()
        {
            return db.GetAll<Company>().ToList();
        }

        public void Remove(int id)
        {
            db.Delete(new Company { CompanyId = id });
        }

        public Company Update(Company company)
        {
            db.Update(company);
            return company;
        }
    }
}