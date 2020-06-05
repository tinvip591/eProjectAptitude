using eProjectAptitude.Models.Entity;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProjectAptitude.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserManagement()
        {
            Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities();
            var db = de.Users.ToList();
            return View(db);
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            ViewBag.PositionList = new SelectList(GetPositons(), "ID", "PositionName");
            return View();
        }
        public List<Position> GetPositons()
        {
            OnlineAptitudeTestEntities db = new OnlineAptitudeTestEntities();
            List<Position> ls = db.Positions.ToList();
            return ls;
        }
        [HttpPost]
        public ActionResult AddUser(Models.Entity.User usr)
        {
            string fileName = Path.GetFileName(usr.ImageFile.FileName);
            //string extension = Path.GetExtension(usr.ImageFile.FileName);
            //fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            usr.Image = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            usr.ImageFile.SaveAs(fileName);
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                db.Users.Add(usr);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("AddUser");
        }
        public ActionResult DetailUser(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(de.Users.Where(x => x.UserID == id).FirstOrDefault());
            }
        }
        public ActionResult EditUser(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(db.Users.Where(x => x.UserID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult EditUser(int id, Models.Entity.User usr)
        {
            try
            {
                var dateTime = DateTime.Now;
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    db.Entry(usr).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("UserManagement");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteUser(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(db.Users.Where(x => x.UserID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult DeleteUser(int id, FormCollection collection)
        {
            try
            {
                Models.Entity.User ps = new Models.Entity.User();
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    Models.Entity.User profileUsers = db.Users.Where(x => x.UserID == id).FirstOrDefault();
                    db.Users.Remove(profileUsers);
                    db.SaveChanges();
                }
                return RedirectToAction("UserManagement");
            }
            catch
            {
                return View();
            }
        }
    }
}