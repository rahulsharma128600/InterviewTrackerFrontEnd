using RestApiCRUDDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiCRUDDemo.EmployeeData
{
    public class MockEmployeeData : IEmployeeData
    {

        private List<Employee> employees = new List<Employee>()
        {
            new Employee
            {
                Id= Guid.NewGuid(),
                Name="Employee1"

            },
            new Employee
            {
                Id= Guid.NewGuid(),
                Name="Employee2"

            },
            new Employee
            {
                Id= Guid.NewGuid(),
                Name="Employee3"

            }
        };
        public Employee AddEmployee(Employee emp)
        {
            emp.Id = Guid.NewGuid();
            employees.Add(emp);
            return emp;
        }

        public void DeleteEmployee(Employee emp)
        {
            employees.Remove(emp);
        }

        public Employee EditEmployee(Employee emp)
        {
            var existingEmp = GetEmployee(emp.Id);
            existingEmp.Name = emp.Name;
            return existingEmp;
        }

        public Employee GetEmployee(Guid id)
        {
            return employees.SingleOrDefault(x => x.Id == id);
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }
    }
}
