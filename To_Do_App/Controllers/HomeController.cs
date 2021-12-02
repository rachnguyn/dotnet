// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using ToDo_App.Models;
// using Microsoft.Data.Sqlite;
// using Npgsql;
// // need library

// namespace ToDo_App.Controllers
// {
//     public class HomeController : Controller
//     {
//         private readonly ILogger<HomeController> _logger;

//         public HomeController(ILogger<HomeController> logger)
//         {
//             _logger = logger;
//         }

//         public IActionResult Index()
//         {
//             List<ToDoItem> ToDoList = new List<ToDoItem>();
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
//                         ToDoList.Add(task);
//                     }
//                 }
//             }   // $ using connection
//             // if (AllToDos == null){
//             //     List<ToDoItem> ToDoList = new List<ToDoItem>();
//             // AllToDos = ToDoList;
//             // }
//             return View("Index", ToDoList);
//         }

//         public IActionResult Add()
//         {
//             return View();
//         }

//         [HttpPost]
//         public IActionResult Add([FromForm] string actionButton, [FromForm] string item, [FromForm] string date, [FromForm] string user, [FromForm] string priority)
//         {
//             if (actionButton == "Add"){
//                 List<ToDoItem> ToDoList = new List<ToDoItem>();
//                 using (var connection = new SqliteConnection("Data Source=/Users/rachelnguyen/Downloads/data/tasks.db"))
//                 {
//                     connection.Open();

//                     var command = connection.CreateCommand();
//                     int? id = 0;
//                     command.CommandText =
//                     @"
//                         select * from tasks order by id desc limit 1;
//                     ";

//                     // execute sql statement and read data from the db
//                     using (var reader = command.ExecuteReader())
//                     {
//                         while (reader.Read())
//                         {
//                             id = reader.GetInt16(0) + 1;
//                         }
//                     }

//                     if (string.IsNullOrEmpty(priority))
//                         command.Parameters.AddWithValue("$priority", (object)DBNull.Value);
//                     else
//                         command.Parameters.AddWithValue("$priority", priority);

                    
//                     if (string.IsNullOrEmpty(date))
//                         command.Parameters.AddWithValue("$due_date", (object)DBNull.Value);
//                     else
//                         command.Parameters.AddWithValue("$due_date", date);
                    

//                     command.Parameters.AddWithValue("$id", id);
//                     command.Parameters.AddWithValue("$description", item);
//                     command.Parameters.AddWithValue("$user", user);
//                     command.Parameters.AddWithValue("$is_done", 0);
//                     command.CommandText = 
//                     @"
//                         insert into tasks (id, description, user, priority, due_date, is_done)
//                         values ($id, $description, $user, $priority, $due_date, $is_done);
//                     ";

//                     // execute sql statement and read the data from the db
//                     command.ExecuteNonQuery();
//                 }   // $ using connection
//                     // AllToDos = ToDoList;
//                 return View();
//             }
            
//             else if (actionButton == "Delete"){
//                 // ToDoItem TaskToDelete = AllToDos.Where(x => x.ID == id).FirstOrDefault();
//                 // AllToDos.Remove(TaskToDelete);
//                 // for (int i = 0; i < (AllToDos.Count-(id+1)); i++) {
//                 //     AllToDos[id+i].ID = id+i;
//                 // }
//                 return View();
//             }
//             return View();

//         }

//         [HttpPost]
//         public IActionResult Index([FromForm] string item, [FromForm] string date, [FromForm] string user, [FromForm] string priority)
//         {
//                 // List<ToDoItem> ToDoList = new List<ToDoItem>();
//                 // ToDoList = AllToDos;
//                 // ToDoItem newItem = new ToDoItem();
//                 // newItem.Description = item;
//                 // newItem.DueDate = date;
//                 // newItem.User = user;
//                 // newItem.Priority = priority;
//                 // newItem.IsDone = false;
//                 // Console.WriteLine(newItem);
//                 // ToDoList.Add(newItem);
//                 // newItem.ID = ToDoList.IndexOf(newItem);
//                 // var id = ToDoList.IndexOf(newItem);     // can only get id by index 
//                 // ToDoList[id]=newItem;

//                 // int is_done = 0;
//                 // if (newItem.IsDone == true){
//                 //     is_done = 1;
//                 // }

//                 List<ToDoItem> ToDoList = new List<ToDoItem>();
//                 using (var connection = new SqliteConnection("Data Source=/Users/rachelnguyen/Downloads/data/tasks.db"))
//                 {
//                     connection.Open();

//                     var command = connection.CreateCommand();
//                     int? id = 0;
//                     command.CommandText =
//                     @"
//                         select * from tasks order by id desc limit 1;
//                     ";

//                     using (var reader = command.ExecuteReader())
//                     {
//                         while (reader.Read())
//                         {
//                             id = reader.GetInt16(0) + 1;
//                         }
//                     }

//                     if (string.IsNullOrEmpty(priority))
//                         command.Parameters.AddWithValue("$priority", (object)DBNull.Value);
//                     else
//                         command.Parameters.AddWithValue("$priority", priority);

                    
//                     if (string.IsNullOrEmpty(date))
//                         command.Parameters.AddWithValue("$due_date", (object)DBNull.Value);
//                     else
//                         command.Parameters.AddWithValue("$due_date", date);

//                     command.Parameters.AddWithValue("$id", id);
//                     command.Parameters.AddWithValue("$description", item);
//                     command.Parameters.AddWithValue("$user", user);
//                     command.Parameters.AddWithValue("$is_done", 0);
//                     command.CommandText = 
//                     @"
//                         insert into tasks (id, description, user, priority, due_date, is_done)
//                         values ($id, $description, $user, $priority, $due_date, $is_done);
//                     ";
//                     // can do it all in one sql statement ^^ "insert select"
//                     command.ExecuteNonQuery();
//                 }   // $ using connection
//                     // AllToDos = ToDoList;
//             // return View("Index", AllToDos);
//             return RedirectToAction("Index");
//         }
//         public IActionResult Delete(int id)
//         {
//             // ToDoItem TaskToDelete = AllToDos.Where(x => x.ID == id).FirstOrDefault();
//             // AllToDos.Remove(TaskToDelete);
//             // for (int i = 0; i < (AllToDos.Count-(id+1)); i++) {
//             //     AllToDos[id+i].ID = id+i;
//             // }

//             using (var connection = new SqliteConnection("Data Source=/Users/rachelnguyen/Downloads/data/tasks.db"))
//             {
//                     connection.Open();

//                     var command = connection.CreateCommand();
//                     command.Parameters.AddWithValue("$id", id);
//                     command.CommandText =
//                     @"
//                         delete from tasks where id = $id;
//                     ";
//                     command.ExecuteNonQuery();

//                     // command.CommandText =
//                     // @"
//                     //     select count(*) from tasks;
//                     // ";
                        
//                     // int? limit = 0;
//                     // using (var reader = command.ExecuteReader())
//                     // {
//                     //     while (reader.Read())
//                     //     {
//                     //         limit = reader.GetInt32(0);
//                     //     }
//                     // }

//                     // for (int i = id; i < limit; i++){
//                     //     int idplusone = i + 1;
//                     //     command.Parameters.AddWithValue("$idplusone", idplusone);
//                     //     command.Parameters.AddWithValue("$i", i);
//                     //     command.CommandText =
//                     //     @"
//                     //         update tasks set id = $i where id = $idplusone;
//                     //     ";
//                     //     command.ExecuteNonQuery();
//                     // }
//                 }   // $ using connection
//             return RedirectToAction("Index");
//         }

//         public IActionResult ToggleIsDone(int id)
//         {
//             // AllToDos[id].IsDone = true;

//             using (var connection = new SqliteConnection("Data Source=/Users/rachelnguyen/Downloads/data/tasks.db"))
//                 {
//                     connection.Open();

//                     var command = connection.CreateCommand();
//                     command.Parameters.AddWithValue("$id", id);
//                     command.CommandText =
//                     @"
//                         update tasks set is_done = '1' where id = $id;
//                     ";
                
//                     command.ExecuteNonQuery();
//                 }   // $ using connection
//             return RedirectToAction("Delete", id);
//         }

//         [HttpPost]
//         public IActionResult ChangeDate(string date, int itemnum)
//         {
//             // AllToDos[itemnum].DueDate = date;

//             using (var connection = new SqliteConnection("Data Source=/Users/rachelnguyen/Downloads/data/tasks.db"))
//                 {
//                     connection.Open();

//                     var command = connection.CreateCommand();
//                     command.Parameters.AddWithValue("$id", itemnum);
//                     command.Parameters.AddWithValue("$due_date", date);

//                     command.CommandText =
//                     @"
//                         update tasks set due_date = $due_date where id = $id;
//                     ";
                
//                     command.ExecuteNonQuery();
//                 }   // $ using connection
//             return RedirectToAction("Index");
//         }

//         [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//         public IActionResult Error()
//         {
//             return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//         }
//     }
// }
