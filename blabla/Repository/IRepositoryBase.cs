using System.Collections.Generic;
using PAD.Models;

namespace PAD.Repository
{
    public interface IRepositoryBase
    {
        List<Employee> GetEmployees();
        
        Employee GetEmployee(string id);
        void DeleteEmployee(string id);
        void AddEmployee(Employee employee);
        void EditEmployee(Employee employee);
    }
}