using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace test1.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext()
            : base("DBConnection")
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
    }
}