using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProjectAptitude.Controllers
{
    public class RolesController : Controller
    {
        // GET: Role
        public ActionResult RoleManagement()
        {
            Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities();
            var db = de.Roles.ToList();
            return View(db);
        }
        public ActionResult AddRole()
        {
            return View();
        }
        public ActionResult AddRole(Models.Entity.Role rol)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                db.Roles.Add(rol);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("AddRole");
        }
        public ActionResult DetailRole(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(de.Roles.Where(x => x.ID == id).FirstOrDefault());
            }
        }
        public ActionResult EditRole(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(db.Roles.Where(x => x.ID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult EditRole(int id, Models.Entity.Role rol)
        {
            try
            {
                var dateTime = DateTime.Now;
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    db.Entry(rol).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("RoleManagement");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteRole(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(db.Roles.Where(x => x.ID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult DeleteRole(int id, FormCollection collection)
        {
            try
            {
                Models.Entity.Role ps = new Models.Entity.Role();
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    Models.Entity.Role profileUsers = db.Roles.Where(x => x.ID == id).FirstOrDefault();
                    db.Roles.Remove(profileUsers);
                    db.SaveChanges();
                }
                return RedirectToAction("RoleManagement");
            }
            catch
            {
                return View();
            }
        }
    }
}