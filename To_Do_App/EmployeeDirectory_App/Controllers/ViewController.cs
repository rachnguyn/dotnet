using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using app1.Models;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace app1.Controllers
{
    
    [ApiController]
    public class ViewController : Controller
    {
        [HttpGet("api/employees/all")]
        public ActionResult<IEnumerable<EmployeeModel>> GetAllEmployees()
        {
            // create List of Employees from file
            List<EmployeeModel> allEmps = new List<EmployeeModel>();
            string[] e = System.IO.File.ReadAllLines("employees.txt");
            for (int i = 0; i < e.Length; i++) {
                EmployeeModel emp = new EmployeeModel();
                emp.formatLine(e[i], i+1);
                allEmps.Add(emp);
            }

            // display all employees
            return allEmps;
        }

        [HttpGet("api/employees/{id}")]
        public ActionResult<EmployeeModel> GetEmployee(int id)
        {
            // create List of Employees from file
            List<EmployeeModel> allEmps = new List<EmployeeModel>();
            string[] e = System.IO.File.ReadAllLines("employees.txt");
            for (int i = 0; i < e.Length; i++) {
                EmployeeModel emp = new EmployeeModel();
                emp.formatLine(e[i], i+1);
                allEmps.Add(emp);
            }

            // find employee to display info
            EmployeeModel em = allEmps.Where(x => x.LineNumber == id).FirstOrDefault();
            return em;
        }

        [HttpPost("api/employees/{id}")]
        public ActionResult<EmployeeModel> UpdateEmployee(int id, EmployeeModel emp)
        {
            // read file, save to array
            string[] e = System.IO.File.ReadAllLines("employees.txt");

            // replace employee with updated info
            e[id-1] = (emp.Name + "," + emp.Age + "," + emp.Salary + "," + emp.ImageUrl);

            // rewrite file
            System.IO.File.WriteAllLines("employees.txt", e);
            return emp;
        }


        [HttpDelete("api/employees/{id}")]
        public string DeleteEmployee(int id)
        {
            // read file, save to List
            List<string> arrLine = System.IO.File.ReadAllLines("employees.txt").ToList();

            // save name of employee to delete
            EmployeeModel d = new EmployeeModel();
            d.formatLine(arrLine[id-1], id);
            string deletedEmployee = d.Name;

            // remove employee 
            arrLine.RemoveAt(id-1);

            // rewrite file
            System.IO.File.WriteAllLines("employees.txt", arrLine);
            return deletedEmployee + " has been deleted at ";
        }
    }
}