// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using ToDo_App.Models;
// using Microsoft.Data.Sqlite;


// namespace ToDo_App.Controllers
// {
//     [ApiController]
//     public class ApiController : Controller
//     {
//         private static int MapPriority(string priority)
//         {
//             switch(priority.ToUpperInvariant())
//             {
//                 case "Low":
//                     return 1;
//                 case "Medium":
//                     return 2;
//                 case "High":
//                     return 3;
//                 default:
//                     return 0;
//             }
//         }

//         [HttpGet("api/tasks/sortlowtohigh")]
//         public ActionResult<IEnumerable<ToDoItem>> LowToHighPriority()
//         {
//             // List<ToDoItem> SortedTasks = HomeController.AllToDos.OrderBy(i=>
//             //         i.Priority == "Low" 
//             //            ? 1 
//             //            : i.Priority == "Medium" 
//             //               ? 2 
//             //               : 3).ToList();

//             List<ToDoItem> SortedTaskList = new List<ToDoItem>();

//             using (var connection = new SqliteConnection("Data Source=/Users/rachelnguyen/Downloads/data/tasks.db"))
//             {
//                 connection.Open();

//                 var command = connection.CreateCommand();
//                 command.CommandText =
//                 @"
//                     select * from tasks;
//                 ";
//                 using (var reader = command.ExecuteReader())
//                 {
//                     while (reader.Read())
//                     {
//                         var task = new ToDoItem();
//                         task.ID = reader.GetInt32(reader.GetOrdinal("id"));
//                         task.Description = reader.GetString(reader.GetOrdinal("description"));
//                         task.User = reader.GetString(reader.GetOrdinal("user"));
//                         task.Priority = !reader.IsDBNull(reader.GetOrdinal("priority")) ? reader.GetString(reader.GetOrdinal("priority")) : "";
//                         task.DueDate = !reader.IsDBNull(reader.GetOrdinal("due_date")) ? reader.GetString(reader.GetOrdinal("due_date")) : "";
//                         int is_done = reader.GetInt16(reader.GetOrdinal("is_done"));
//                         if (is_done == 1){
//                             task.IsDone = true;
//                         }
//                         else
//                             task.IsDone = false;
//                         SortedTaskList.Add(task);
//                     }
//                 }
//             }   // $ using connection

//             // use order by in sql statement
//             SortedTaskList = SortedTaskList.OrderBy(i=>
//                     i.Priority == "Low" 
//                        ? 1 
//                        : i.Priority == "Medium" 
//                           ? 2 
//                           : 3).ToList();

//             return SortedTaskList;
//         }

//         [HttpGet("api/tasks/sorthightolow")]
//         public ActionResult<IEnumerable<ToDoItem>> HighToLowPriority()
//         {
//             // List<ToDoItem> SortedTasks = HomeController.AllToDos.OrderBy(i=>
//             //         i.Priority == "High" 
//             //            ? 1 
//             //            : i.Priority == "Medium" 
//             //               ? 2 
//             //               : 3).ToList();
//             // return SortedTasks;

//             List<ToDoItem> SortedTaskList = new List<ToDoItem>();

//             using (var connection = new SqliteConnection("Data Source=/Users/rachelnguyen/Downloads/data/tasks.db"))
//             {
//                 connection.Open();

//                 var command = connection.CreateCommand();
//                 command.CommandText =
//                 @"
//                     select * from tasks;
//                 ";
//                 using (var reader = command.ExecuteReader())
//                 {
//                     while (reader.Read())
//                     {
//                         var task = new ToDoItem();
//                         task.ID = reader.GetInt32(reader.GetOrdinal("id"));
//                         task.Description = reader.GetString(reader.GetOrdinal("description"));
//                         task.User = reader.GetString(reader.GetOrdinal("user"));
//                         task.Priority = !reader.IsDBNull(reader.GetOrdinal("priority")) ? reader.GetString(reader.GetOrdinal("priority")) : "";
//                         task.DueDate = !reader.IsDBNull(reader.GetOrdinal("due_date")) ? reader.GetString(reader.GetOrdinal("due_date")) : "";
//                         int is_done = reader.GetInt16(reader.GetOrdinal("is_done"));
//                         if (is_done == 1){
//                             task.IsDone = true;
//                         }
//                         else
//                             task.IsDone = false;
//                         SortedTaskList.Add(task);
//                     }
//                 }
//             }   // $ using connection

//             SortedTaskList = SortedTaskList.OrderBy(i=>
//                     i.Priority == "High" 
//                        ? 1 
//                        : i.Priority == "Medium" 
//                           ? 2 
//                           : 3).ToList();

//             return SortedTaskList;
//         }
//     }
// }
