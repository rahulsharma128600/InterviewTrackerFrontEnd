using RestApiCRUDDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiCRUDDemo.EmployeeData
{
    public class SqlEmployeeData : IEmployeeData
    {
        private EmployeeContext empContext;

        public SqlEmployeeData(EmployeeContext EmpContext)
        {
            empContext = EmpContext;
        }
        public Employee AddEmployee(Employee emp)
        {
            emp.Id = Guid.NewGuid();
            empContext.Employees.Add(emp);
            empContext.SaveChanges();
            return emp;
        }

        public void DeleteEmployee(Employee emp)
        {
            empContext.Employees.Remove(emp);
            empContext.SaveChanges();
        }

        public Employee EditEmployee(Employee emp)
        {
            var existingEmp = empContext.Employees.Find(emp.Id);
            if (existingEmp != null)
            {
                existingEmp.Name = emp.Name;
                empContext.Update(existingEmp);
                empContext.SaveChanges();
            }
            return existingEmp;
        }

        public Employee GetEmployee(Guid id)
        {
            var emp = empContext.Employees.Find(id);
            return emp;
        }

        public List<Employee> GetEmployees()
        {
            return empContext.Employees.ToList();
        }
    }
}
