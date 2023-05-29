using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Collections.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private static readonly Dictionary<string, string> employees = new Dictionary<string, string>()
    {
        {"Ali", "Ali"},
        {"Julia", "Julia"},
        {"Adam", "Adam"},
        {"Lily", "Lily"},
    };

    private static readonly List<string> employeeList = new List<string>(employees.Values);

    [HttpGet("{key}")]
    public ActionResult<string> GetDictionaryValue(string key)
    {
        if (employees.ContainsKey(key))
        {
            return employees[key];
        }
        return NotFound();
    }

    [HttpGet]
        [Route("GetEmployees")]
        public ActionResult<List<string>> GetEmployees()
        {
            return Ok(employeeList);
        }

        [HttpPost]
        public IActionResult AddEmployee(string key, string value)
        {
            if (!employees.ContainsKey(key))
            {
                employees.Add(key, value);
                employeeList.Add(value);
                return Ok();
            }
            return Conflict("Already Exists");
        }

        [HttpDelete("{key}")]
        public IActionResult DeleteEmployee(string key)
        {
            if (employees.ContainsKey(key))
            {
                employees.Remove(key);
                employeeList.Remove(employees[key]);
                return Ok();
            }
            return NotFound();
        }

        [HttpPut("{key}")]
        public IActionResult UpdateEmployee(string key, string value)
        {
            if (employees.ContainsKey(key))
            {
                employees[key] = value;
                employeeList[employeeList.IndexOf(employees[key])] = value;
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("ClearAllEmployees")]
        public IActionResult ClearAllEmployees()
                {
                    employees.Clear();
                    employeeList.Clear();
                    return Ok();
                }
}

//add, delete, get, and clear all.
