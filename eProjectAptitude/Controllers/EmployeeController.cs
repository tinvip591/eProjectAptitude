using eProjectAptitude.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProjectAptitude.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult EmployeeManagement()
        {
            Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities();
            var db = de.Employees.ToList();
            return View(db);
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            ViewBag.RoleList = new SelectList(GetRole(), "ID", "RoleName");
            return View();
        }
        public List<Role> GetRole()
        {
            OnlineAptitudeTestEntities db = new OnlineAptitudeTestEntities();
            List<Role> ls = db.Roles.ToList();
            return ls;
        }
        [HttpPost]
        public ActionResult AddEmployee(Models.Entity.Employee emp)
        {
            string fileName = Path.GetFileName(emp.ImageFile.FileName);
            //string extension = Path.GetExtension(usr.ImageFile.FileName);
            //fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            emp.Image = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            emp.ImageFile.SaveAs(fileName);
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                db.Employees.Add(emp);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("AddEmployee");
        }
        public ActionResult DetailEmployee(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities de = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(de.Employees.Where(x => x.IdE == id).FirstOrDefault());
            }
        }
        public ActionResult EditEmployee(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(db.Employees.Where(x => x.IdE == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult EditEmployee(int id, Models.Entity.Employee emp)
        {
            try
            {
                var dateTime = DateTime.Now;
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("EmployeeManagement");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteEmployee(int id)
        {
            using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
            {
                return View(db.Employees.Where(x => x.IdE == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult DeleteEmployee(int id, FormCollection collection)
        {
            try
            {
                Models.Entity.Employee ps = new Models.Entity.Employee();
                using (Models.Entity.OnlineAptitudeTestEntities db = new Models.Entity.OnlineAptitudeTestEntities())
                {
                    Models.Entity.Employee profileUsers = db.Employees.Where(x => x.IdE == id).FirstOrDefault();
                    db.Employees.Remove(profileUsers);
                    db.SaveChanges();
                }
                return RedirectToAction("EmployeeManagement");
            }
            catch
            {
                return View();
            }
        }
    }
}