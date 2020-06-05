using eProjectAptitude.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProjectAptitude.Controllers
{
    public class PositionController : Controller
    {
        // GET: Position
        public ActionResult PositionManagement()
        {
            Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities();
            var db = de.Positions.ToList();
            return View(db);
        }
        [HttpGet]
        public ActionResult AddPosition()
        {
            return View();
        }
        public ActionResult AddPosition(Models.Entity.Position pos)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                db.Positions.Add(pos);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("AddPosition");
        }
        public ActionResult DetailPosition(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(de.Positions.Where(x => x.ID == id).FirstOrDefault());
            }
        }
        public ActionResult EditPosition(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(db.Positions.Where(x => x.ID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult EditPosition(int id, Models.Entity.Position pos)
        {
            try
            {
                var dateTime = DateTime.Now;
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    db.Entry(pos).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("PositionManagement");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeletePosition(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(db.Positions.Where(x => x.ID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult DeletePosition(int id, FormCollection collection)
        {
            try
            {
                Models.Entity.Position ps = new Models.Entity.Position();
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    Models.Entity.Position profileUsers = db.Positions.Where(x => x.ID == id).FirstOrDefault();
                    db.Positions.Remove(profileUsers);
                    db.SaveChanges();
                }
                return RedirectToAction("PositionManagement");
            }
            catch
            {
                return View();
            }
        }
    }
}