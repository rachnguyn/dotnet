using Dapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDo_App.Models;
using Microsoft.Data.Sqlite;
using Npgsql; 

namespace ToDo_App.Controllers
{
    [ApiController]
    public class ApiController : Controller
    {
        private static NpgsqlConnection GetConnection(){
            return new NpgsqlConnection(@"Server=localhost;Port=5432;Database=pstasks;User Id=postgres;Password=chomper;");
        }

        [HttpGet("api/tasks/sortlowtohigh")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> LHPriority()
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                var lhsortedemp = await conn.QueryAsync<ToDoItem>("select * from tasks2 order by case when priority = 'Low' then 1 when priority = 'Medium' then 2 else 3 end");
                Console.Out.WriteLine("Sorted low to high.");
                return lhsortedemp.ToList();
            } // $ using psql connection
        }

        [HttpGet("api/tasks/sorthightolow")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> HLPriority()
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                var hlsortedemp = await conn.QueryAsync<ToDoItem>("select * from tasks2 order by case when priority = 'High' then 1 when priority = 'Medium' then 2 else 3 end");
                Console.Out.WriteLine("Sorted low to high.");
                return hlsortedemp.ToList();
            } // $ using psql connection
        }
    }
}