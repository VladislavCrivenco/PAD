using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PAD.Models;

namespace PAD.Repository
{
    public class RepositoryBase : IRepositoryBase
    {
        private readonly string _connectionString;

        public RepositoryBase(IOptions<ConnectionStringList> connectionStrings)
        {
            _connectionString = connectionStrings.Value.ConnectionString1;
        }

        public IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(_connectionString);
            }
        }

        public List<Employee> GetEmployees()
        {
            using (IDbConnection conn = Connection)
            {
                List<Employee> result;
                conn.Open();
                result = conn.Query<Employee>("SELECT * FROM Employees").ToList();

                return result;
            }
        }

        public void DeleteEmployee(string id)
        {
                        using (IDbConnection conn = Connection)
            {
                conn.Open();
                conn.Execute($"DElete FROM Employees WHERE Id='{id}'");
            }
        }

        public void AddEmployee(Employee employee)        
        {
                        using (IDbConnection conn = Connection)
            {
                conn.Open();
                conn.Execute($"Insert into Employees Values('{employee.Id}', '{employee.Firstname}', '{employee.Lastname}', '{employee.Job}')");
            }
        }

        public Employee GetEmployee(string id)        
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                return conn.QueryFirstOrDefault<Employee>($"SELECT * FROM Employees where id ='{id}'");
            }
        }

        public void EditEmployee(Employee employee)          
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                conn.Execute($"UPDATE Employees SET Firstname = '{employee.Firstname}', Lastname = '{employee.Lastname}', Job = '{employee.Job}' WHERE Id='{employee.Id}'");
            }
        }
    }
}