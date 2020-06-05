using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProjectAptitude.Controllers
{
    public class QuestionTypeController : Controller
    {
        // GET: QuestionType
        public ActionResult QuestionTypeManagement()
        {
            Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities();
            var db = de.QuestionTypes.ToList();
            return View(db);
        }
        [HttpGet]
        public ActionResult AddQuestionType()
        {
            return View();
        }

        public ActionResult AddQuestionType(Models.Entity.QuestionType qut)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                db.QuestionTypes.Add(qut);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("AddQuestionType");
        }

        //public ActionResult DetailQuestionType(int id)
        //{
        //    using (Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities())
        //    {
        //        return View(de.QuestionTypes.Where(x => x.ID == id).FirstOrDefault());
        //    }
        //}

        public ActionResult EditQuestionType(int? id)
        {
            if (id != null)
            {
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    return View(db.QuestionTypes.Where(x => x.ID == id).FirstOrDefault());
                }
            }
            return RedirectToAction("QuestionTypeManagement");
        }

        [HttpPost]
        public ActionResult EditQuestionType(int id, Models.Entity.QuestionType qut)
        {
            try
            {
                var dateTime = DateTime.Now;
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    db.Entry(qut).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("QuestionTypeManagement");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteQuestionType(int? id)
        {
            if (id != null)
            {
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    return View(db.QuestionTypes.Where(x => x.ID == id).FirstOrDefault());
                }
            }
            return RedirectToAction("QuestionTypeManagement");
        }

        [HttpPost]
        public ActionResult DeleteQuestionType(int id, FormCollection collection)
        {
            try
            {
                Models.Entity.QuestionType ps = new Models.Entity.QuestionType();
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    Models.Entity.QuestionType questiontypes = db.QuestionTypes.Where(x => x.ID == id).FirstOrDefault();
                    db.QuestionTypes.Remove(questiontypes);
                    db.SaveChanges();
                }
                return RedirectToAction("QuestionTypeManagement");
            }
            catch
            {
                return View();
            }
        }
    }
}