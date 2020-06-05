using eProjectAptitude.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProjectAptitude.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        public ActionResult QuestionManagement()
        {
            Models.Entity.OnlineAptitudeTestEntities dtb = new Models.Entity.OnlineAptitudeTestEntities();
            var db = dtb.Questions.ToList();
            return View(db);
        }
        [HttpGet]
        public ActionResult AddQuestion()
        {
            ViewBag.QuestionList = new SelectList(GetQuestion(), "ID",  "QuestionTypeName" );
            return View();
        }
        
        public List<QuestionType> GetQuestion()
        {
            OnlineAptitudeTestEntities dtb = new OnlineAptitudeTestEntities();
            List<QuestionType> qt = dtb.QuestionTypes.ToList();
            return qt;
        }

        [HttpPost]
        public ActionResult AddQuestion(Models.Entity.Question ques)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                db.Questions.Add(ques);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("AddQuestion");
        }

        public ActionResult DetailQuestion(int? id)
        {
            if (id != null)
            {
                using (Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    return View(de.Questions.Where(x => x.ID == id).FirstOrDefault());
                }
            }
            return RedirectToAction("QuestionManagement");
        }

        public ActionResult EditQuestion(int? id)
        {
            if (id != null)
            {
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    ViewBag.QuestionList = new SelectList(GetQuestion(), "ID", "QuestionTypeName");
                    return View(db.Questions.Where(x => x.ID == id).FirstOrDefault());
                }
            }
            return RedirectToAction("QuestionManagement");
        }

        [HttpPost]
        public ActionResult EditQuestion(int id, Models.Entity.Question ques)
        {
            try
            {
                var dateTime = DateTime.Now;
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    db.Entry(ques).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("QuestionManagement");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteQuestion(int? id)
        {
            if (id != null)
            {
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    return View(db.Questions.Where(x => x.ID == id).FirstOrDefault());
                }
            }
            return RedirectToAction("QuestionManagement");
        }

        [HttpPost]
        public ActionResult DeleteQuestion(int id, FormCollection collection)
        {
            try
            {
                Models.Entity.Question ps = new Models.Entity.Question();
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    Models.Entity.Question questions = db.Questions.Where(x => x.ID == id).FirstOrDefault();
                    db.Questions.Remove(questions);
                    db.SaveChanges();
                }
                return RedirectToAction("QuestionType");
            }
            catch
            {
                return View();
            }
        }
    }
}