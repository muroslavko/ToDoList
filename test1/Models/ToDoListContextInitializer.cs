using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace test1.Models
{
    public class ToDoListContextInitializer : DropCreateDatabaseAlways<ToDoContext>
    {
        protected override void Seed(ToDoContext context)
        {
            //context.Categories.Add(new Category());
            context.SaveChanges();
            base.Seed(context);
        }
    }
}