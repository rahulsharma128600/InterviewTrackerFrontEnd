using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiCRUDDemo.EmployeeData;
using RestApiCRUDDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiCRUDDemo.Controller
{
    
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeData employeeData;

        public EmployeesController(IEmployeeData empData)
        {
            employeeData = empData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetEmployees()
        {
            return Ok(employeeData.GetEmployees());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetEmployee(Guid id)
        {
            var emp = employeeData.GetEmployee(id);

            if (emp != null)
            {
                return Ok(emp);
            }
            return NotFound($"Employee with Id: {id} not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult AddEmployee(Employee emp)
        {
            employeeData.AddEmployee(emp);
            return Created(HttpContext.Request.Scheme + ":/" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + emp.Id,emp);
            
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var emp = employeeData.GetEmployee(id);

            if (emp != null)
            {
                employeeData.DeleteEmployee(emp);
                return Ok("Employee Deleted");
            }
            return NotFound($"Employee with Id: {id} not found");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult EditEmployee(Guid id, Employee emp)
        {
            var existingEmp = employeeData.GetEmployee(id);

            if (existingEmp != null)
            {
                emp.Id = existingEmp.Id;
                employeeData.EditEmployee(emp);
                return Ok(emp);
            }
            return NotFound($"Employee with Id: {id} not found");
        }
    }
}
