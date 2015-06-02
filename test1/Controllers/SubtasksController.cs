using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test1.Models;

namespace test1.Controllers
{
    public class SubtasksController : Controller
    {
        private ToDoContext db = new ToDoContext();

        public PartialViewResult _GetSubtaskFor(int id)
        {
            ViewBag.TaskId = id;
            var list = db.Subtasks.Where(p => p.TaskId == id).ToList<Subtask>();
            return PartialView("_GetSubtaskFor", list);
        }
        [ChildActionOnly]
        public PartialViewResult _SubtasksForm(int id)
        {
            var task = new Subtask { TaskId = id };
            return PartialView("_SubtasksForm", task);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _AddSubtask(Subtask subt)
        {
            db.Subtasks.Add(subt);
            db.SaveChanges();

            ViewBag.TaskId = subt.TaskId;
            var list = db.Subtasks.Where(p => p.TaskId == subt.TaskId).ToList<Subtask>();
            return PartialView("_GetSubtaskFor",list);
        }
        public ActionResult ChangeStateOfSubtask(IEnumerable<test1.Models.Subtask> subtask)
        {
            //перевірка чи не порожня
            //важливо
            //TODO: gthtdshbn
            var t = new Subtask();
            bool stateOfTask = false;
            foreach (var item in subtask)
            {
                t = db.Subtasks.Where(ah => ah.Id == item.Id).Single<Subtask>();
                t.Complete = subtask.Where(a => a.Id == t.Id).Single<Subtask>().Complete;
                stateOfTask = t.Complete;
                db.Entry(t).State = EntityState.Modified;
            }
            if(stateOfTask == true)
            {
                db.Tasks.Where(ah => ah.Id == t.TaskId).Single<Task>().Complete = stateOfTask;  
            }
            db.SaveChanges();
            return RedirectToAction("Details", "Tasks", new { id= t.TaskId });
        }
        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subtask subtask = db.Subtasks.Find(id);
            if (subtask == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Text", subtask.TaskId);
            return View(subtask);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Subtask subtask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subtask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details","Tasks", new { id=subtask.TaskId.ToString() });
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Text", subtask.TaskId);
            return View(subtask);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subtask sub = db.Subtasks.Find(id);
            if (sub == null)
            {
                return HttpNotFound();
            }
            return View(sub);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subtask sub = db.Subtasks.Find(id);
            db.Subtasks.Remove(sub);
            db.SaveChanges();
            return RedirectToAction("Details", "Tasks", new { id = sub.TaskId.ToString() });
        }
    }
}