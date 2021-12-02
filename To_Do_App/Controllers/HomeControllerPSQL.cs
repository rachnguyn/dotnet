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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private static NpgsqlConnection GetConnection(){
            return new NpgsqlConnection(@"Server=localhost;Port=5432;Database=pstasks;User Id=postgres;Password=chomper;");
        }
        public async Task<ActionResult<IEnumerable<ToDoItem>>> CreateTasks()
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                // Connect to the database
                var employees = await conn.QueryAsync<ToDoItem>("select * from tasks2");
                Console.Out.WriteLine("Tasks read.");
                return View(employees);               
            } // $ using psql connection
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] string actionButton, [FromForm] string item, [FromForm] string date, [FromForm] string user, [FromForm] string priority)
        {
            if (actionButton == "Add"){
                                    
                var p = (object)DBNull.Value;;
                if (!string.IsNullOrEmpty(priority))
                    p = priority;

                var d = (object)DBNull.Value;;    
                if (!string.IsNullOrEmpty(date))
                    d = date;

                using (NpgsqlConnection conn = GetConnection())
                {
                    var parameters = new { desc = item, username = user, is_done = false, due_date = d, priority = p};

                int count = conn.QueryFirst<int>("select count(*) from tasks2");
                if (count == 0) {
                    await conn.QueryAsync<ToDoItem>(
                    @"insert into tasks2 (id, description, username, priority, due_date, is_done) 
                    values (0, @desc, @username, @priority, @due_date, @is_done)", parameters);
                    Console.Out.WriteLine("Task written.");
                }
                else {
                    await conn.QueryAsync<ToDoItem>(
                    @"insert into tasks2 (id, description, username, priority, due_date, is_done) 
                    select id + 1, @desc, @username, @priority, @due_date, @is_done from tasks2 order by id desc limit 1", parameters);
                    Console.Out.WriteLine("Task written.");
                }
                return View();
                } // $ using psql connection
            }
            else if (actionButton == "Delete"){
                // ToDoItem TaskToDelete = AllToDos.Where(x => x.ID == id).FirstOrDefault();
                // AllToDos.Remove(TaskToDelete);
                // for (int i = 0; i < (AllToDos.Count-(id+1)); i++) {
                //     AllToDos[id+i].ID = id+i;
                // }
                return View();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] string item, [FromForm] string date, [FromForm] string user, [FromForm] string priority)
        {

            var p = (object)DBNull.Value;;
            if (!string.IsNullOrEmpty(priority))
            p = priority;

            var d = (object)DBNull.Value;;    
            if (!string.IsNullOrEmpty(date))
            d = date;

            using (NpgsqlConnection conn = GetConnection())
            {
                var parameters = new { desc = item, username = user, is_done = false, due_date = d, priority = p};

                int count = conn.QueryFirst<int>("select count(*) from tasks2");
                if (count == 0) {
                    await conn.QueryAsync<ToDoItem>(
                    @"insert into tasks2 (id, description, username, priority, due_date, is_done) 
                    values (0, @desc, @username, @priority, @due_date, @is_done)", parameters);
                    Console.Out.WriteLine("Task written.");
                }
                else {
                    await conn.QueryAsync<ToDoItem>(
                    @"insert into tasks2 (id, description, username, priority, due_date, is_done) 
                    select id + 1, @desc, @username, @priority, @due_date, @is_done from tasks2 order by id desc limit 1", parameters);
                    Console.Out.WriteLine("Task written.");
                }
                return RedirectToAction("Index");
            } // $ using psql connection
        }
        public async Task<IActionResult> Delete(int id)
        {
            using (NpgsqlConnection conn = GetConnection())
                {
                    var parameters = new {id_to_delete = id};

                    await conn.QueryAsync<ToDoItem>("delete from tasks2 where id = @id_to_delete", parameters);
                    Console.Out.WriteLine("Deleted task.");

                    return RedirectToAction("Index");  
                } // $ using psql connection
        }

        public async Task<IActionResult> ToggleIsDone(int id)
        {
            using (NpgsqlConnection conn = GetConnection())
                {
                    var parameters = new {id_to_update = id};

                    await conn.QueryAsync<ToDoItem>("update tasks2 set is_done = '1' where id = @id_to_update", parameters);
                    Console.Out.WriteLine("Task completed.");

                    return RedirectToAction("Delete", id);
                } // $ using psql connection
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDate(string date, int itemnum)
        {
            using (NpgsqlConnection conn = GetConnection())
            {
                var parameters = new {id_to_update = itemnum, due_date = date};

                await conn.QueryAsync<ToDoItem>("update tasks2 set due_date = @due_date where id = @id_to_update;", parameters);
                Console.Out.WriteLine("Date changed.");

                 return RedirectToAction("Index");
            } // $ using psql connection
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
