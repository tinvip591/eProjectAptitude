using eProjectAptitude.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProjectAptitude.Controllers
{
    public class ExamDetailsController : Controller
    {
        // GET: ExamDe
        public ActionResult ExamDeManagement()
        {
            Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities();
            var db = de.ExaminationDetails.ToList();
            return View(db);
        }
        [HttpGet]
        public ActionResult AddExamDe()
        {
            ViewBag.QuestionList = new SelectList(GetQuestion(),"ID", "QuestionName");
            ViewBag.ExaminationList = new SelectList(GetExamination(), "ID", "ExamCode");
            return View();
        }
        public List<Question> GetQuestion()
        {
            OnlineAptitudeTestEntities db = new OnlineAptitudeTestEntities();
            List<Question> ls = db.Questions.ToList();
            return ls;

        }
        public List<Examination> GetExamination()
        {
            OnlineAptitudeTestEntities db = new OnlineAptitudeTestEntities();
            List<Examination> ls = db.Examinations.ToList();
            return ls;

        }
        [HttpPost]
        public ActionResult AddExamDe(Models.Entity.ExaminationDetail exade)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                db.ExaminationDetails.Add(exade);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("AddExamDe");
        }
        public ActionResult DetailExamDe(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(de.ExaminationDetails.Where(x => x.ID == id).FirstOrDefault());
            }
        }
        public ActionResult EditExamDe(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(db.ExaminationDetails.Where(x => x.ID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult EditExamDe(int id, Models.Entity.ExaminationDetail exde)
        {
            try
            {
                var dateTime = DateTime.Now;
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    db.Entry(exde).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("ExamDeManagement");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteExamDe(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(db.ExaminationDetails.Where(x => x.ID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult DeleteExamDe(int id, FormCollection collection)
        {
            try
            {
                Models.Entity.ExaminationDetail ps = new Models.Entity.ExaminationDetail();
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    Models.Entity.ExaminationDetail profileUsers = db.ExaminationDetails.Where(x => x.ID == id).FirstOrDefault();
                    db.ExaminationDetails.Remove(profileUsers);
                    db.SaveChanges();
                }
                return RedirectToAction("ExamDeManagement");
            }
            catch
            {
                return View();
            }
        }
    }
}