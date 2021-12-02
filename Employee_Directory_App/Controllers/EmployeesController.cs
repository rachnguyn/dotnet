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
    public class EmployeesController : Controller
    {
        private static EmployeesViewModel allEmployees;

        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(ILogger<EmployeesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // ViewModel for list of employees
            EmployeesViewModel allEmp = new EmployeesViewModel();
            allEmp.Employees = new List<EmployeeModel>();

            // read file
            string[] employees = System.IO.File.ReadAllLines("employees.txt");

            // populate ViewModel with employees (<LineModel> which includes name, salary, age)
            for (int i = 0; i < employees.Length; i++) {
                EmployeeModel emp = new EmployeeModel();
                emp.formatLine(employees[i], i+1);
                allEmp.Employees.Add(emp);
            }
            allEmployees = allEmp;
            return View(allEmployees);
        }
        public IActionResult Edit(int id) // get
        {
            // display employee based on id
            EmployeeModel e = allEmployees.Employees.Where(x => x.LineNumber == id).FirstOrDefault();
 
            return View(e); 
        }

    
        [HttpPost]  // post attribute
        public IActionResult Edit(int id, [FromForm] string actionButton, [FromForm] string employeeName, [FromForm] string employeeAge, [FromForm] string employeeSalary, [FromForm] string employeeImg) // post
        {
            if (actionButton == "Add"){
                List<string> employees = new List<string>();
                employees.Add(employeeName + "," + employeeAge + "," + employeeSalary + "," + employeeImg);
                System.IO.File.AppendAllLines("employees.txt", employees);
                return View();
            }
            
            else if (actionButton =="Update") {
                // read file, save to array
                string[] arrLine = System.IO.File.ReadAllLines("employees.txt");

                // replace employee with updated info
                arrLine[id-1] = (employeeName + "," + employeeAge + "," + employeeSalary + "," + employeeImg);

                // rewrite file
                System.IO.File.WriteAllLines("employees.txt", arrLine);
                return View();
            }
            else{
                // read file, save to array
                List<string> arrLine = System.IO.File.ReadAllLines("employees.txt").ToList();

                // replace employee with updated info
                arrLine.RemoveAt(id-1);

                // rewrite file
                System.IO.File.WriteAllLines("employees.txt", arrLine);
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}