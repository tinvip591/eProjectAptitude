using eProjectAptitude.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProjectAptitude.Controllers
{
    public class ExaminationController : Controller
    {
        // GET: Examination
        public ActionResult ExaminationManagement()
        {
            Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities();
            var db = de.Examinations.ToList();
            return View(db);
        }
        [HttpGet]
        public ActionResult AddExamination()
        {
            ViewBag.EmpList = new SelectList(GetEmp(), "IdE", "NameE");
            ViewBag.PositionList = new SelectList(GetPosition(), "ID", "PositionName");
            return View();
        }

        public List<Employee> GetEmp()
        {
            OnlineAptitudeTestEntities db = new OnlineAptitudeTestEntities();
            List<Employee> ls = db.Employees.ToList();
            return ls;
        }
        public List<Position> GetPosition()
        {
            OnlineAptitudeTestEntities db = new OnlineAptitudeTestEntities();
            List<Position> ls = db.Positions.ToList();
            return ls;
        }
        [HttpPost]
        public ActionResult AddExamination(Models.Entity.Examination exa)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                db.Examinations.Add(exa);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("AddExamination");
        }
        public ActionResult DetailExamination(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(de.Examinations.Where(x => x.ID == id).FirstOrDefault());
            }
        }
        public ActionResult EditExamination(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(db.Examinations.Where(x => x.ID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult EditExamination(int id, Models.Entity.Examination exa)
        {
            try
            {
                var dateTime = DateTime.Now;
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    db.Entry(exa).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("ExaminationManagement");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteExamination(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(db.Examinations.Where(x => x.ID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult DeleteExamination(int id, FormCollection collection)
        {
            try
            {
                Models.Entity.Examination ps = new Models.Entity.Examination();
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    Models.Entity.Examination profileUsers = db.Examinations.Where(x => x.ID == id).FirstOrDefault();
                    db.Examinations.Remove(profileUsers);
                    db.SaveChanges();
                }
                return RedirectToAction("ExaminationManagement");
            }
            catch
            {
                return View();
            }
        }
    }
}