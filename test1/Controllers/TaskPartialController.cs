using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test1.Models
{
    public class TaskPartialController : Controller
    {
        private ToDoContext db = new ToDoContext();
        public PartialViewResult _GetForCategory(int id)
        {
            ViewBag.CategoryId = id;
            var list = db.Tasks.Where(p => p.CategoryId == id).ToList<Task>();
            return PartialView("_GetForCategory", list);
        }

        [ChildActionOnly]
        public PartialViewResult _TaskForm(int id)
        {
            var task = new Task { CategoryId = id };
            return PartialView("_TaskForm", task);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _AddTask(Task task)
        {
            task.Date = DateTime.Now;
            db.Tasks.Add(task);
            db.SaveChanges();

            ViewBag.CategoryId = task.CategoryId;
            var list = db.Tasks.Where(p => p.CategoryId == task.CategoryId).ToList<Task>();
            return PartialView("_GetForCategory", list);
        }

    }
}