using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test1.Models;

namespace test1.Controllers
{
    public class TasksController : Controller
    {
        private ToDoContext db = new ToDoContext();

        // GET: Tasks
        public ActionResult Index()
        {
            var tasks = db.Tasks.Include(t => t.Category);
            return View(tasks.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }
        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Text", task.CategoryId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Text,Complete,CategoryId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id=task.Id.ToString() });
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Text", task.CategoryId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index","Categories");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStateOfTask([Bind(Include = "Id,Complete")] IEnumerable<test1.Models.Task> task)
        {
            if (task == null)
            {
                return RedirectToAction("Index", "Categories"); ;
            }
            foreach (var item in task)
            {
                var t = db.Tasks.Where(ah => ah.Id == item.Id).Single<Task>();
                t.Complete = task.Where(a => a.Id == t.Id).Single<Task>().Complete;
                db.Entry(t).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index","Categories");
        }
        public ActionResult DeleteDone (int id)
        {
            List<Task> task = db.Tasks.Where(x => x.CategoryId == id && x.Complete == true).ToList();
            db.Tasks.RemoveRange(task);
            db.SaveChanges();
            return RedirectToAction("Index", "Categories");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
