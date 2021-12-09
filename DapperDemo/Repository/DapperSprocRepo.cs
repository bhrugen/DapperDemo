using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DapperDemo.Repository
{
    public class DapperSprocRepo : IDapperSprocRepo
    {
        public DapperSprocRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        private IConfiguration _configuration { get; set; }

        public string ConnectionString { get; set; }


        public void Execute(string name)
        {
            Execute(name, null);
        }


        public void Execute(string name, object param)
        {
            using var cnn = new SqlConnection(ConnectionString);
            cnn.Execute(name, param, commandType: CommandType.StoredProcedure);
        }


        public T Single<T>(string name, int id)
        {
            return Single<T>(name, new { id });
        }

        public T Single<T>(string name, object param)
        {
            using var cnn = new SqlConnection(ConnectionString);
            var result = cnn.Query<T>(name, param, commandType: CommandType.StoredProcedure);
            return result != null ? result.FirstOrDefault() : default;
        }


        public List<T> List<T>(string name, int id)
        {
            return List<T>(name, new { id });
        }

        public List<T> List<T>(string name, object param)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                var result = cnn.Query<T>(name, param, commandType: CommandType.StoredProcedure);
                if (result != null)
                    return result.ToList();
            }

            return new List<T>();
        }


        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string name, object param)
        {
            using var cnn = new SqlConnection(ConnectionString);
            var result = cnn.QueryMultiple(name, param, commandType: CommandType.StoredProcedure);
            var item1 = result.Read<T1>().ToList();
            var item2 = result.Read<T2>().ToList();
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(item1, item2);
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> List<T1, T2, T3>(string name, object param)
        {
            using var cnn = new SqlConnection(ConnectionString);
            var result = cnn.QueryMultiple(name, param, commandType: CommandType.StoredProcedure);
            var item1 = result.Read<T1>().ToList();
            var item2 = result.Read<T2>().ToList();
            var item3 = result.Read<T3>().ToList();
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(item1, item2, item3);
        }


        public List<T> List<T>(string name)
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                var result = cnn.Query<T>(name, commandType: CommandType.StoredProcedure);

                if (result != null)
                    return result.ToList();
            }

            return new List<T>();
        }


        public void QueryExecute(string name, object param)
        {
            using var cnn = new SqlConnection(ConnectionString);
            cnn.Execute(name, param, commandType: CommandType.Text);
        }

        public void QueryExecute(string name)
        {
            using var cnn = new SqlConnection(ConnectionString);
            cnn.Execute(name, null, commandType: CommandType.Text);
        }
    }
}