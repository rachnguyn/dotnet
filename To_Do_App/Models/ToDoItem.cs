using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ToDo_App.Models
{
    public class ToDoItem
    {
        public int ID { get; set;}
        public string Description { get; set; }
        public string User { get; set; }
        
    #nullable enable
        public string? Priority { get; set; }
        public string? Due_Date { get; set; }
        public bool Is_Done { get; set; }
    }
}
