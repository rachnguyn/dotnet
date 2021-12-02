using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using app1.Models;

namespace app1.Models
{
        public class EmployeeModel
        {
        public string Name {get; set;}
        public int Age {get; set;}
        public int Salary {get; set;}
        public int LineNumber {get; set;}
        public string ImageUrl {get; set;}

        public void formatLine (string line, int num){
            string[] values = line.Split(',');
            for(int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
            }
            Name = values[0];   
            Age = Int32.Parse(values[1]);
            Salary = Int32.Parse(values[2]); 
            ImageUrl = values[3];
            LineNumber = num;  
            }
        }

        public class EmployeesViewModel {
            public List<EmployeeModel> Employees { get; set; }

            public EmployeesViewModel(){
                Employees = new List<EmployeeModel>();
            }
        }
}
