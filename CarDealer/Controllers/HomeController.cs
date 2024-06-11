using CarDealer.EF;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealer.Controllers
{
    public class HomeController : Controller
    {
        private MyAppDBEntities2 db = new MyAppDBEntities2();
        public ActionResult Index()
        {
            ViewBag.info = db.tblcars;
            ViewBag.contact = db.tbllocations;
            return View();
        }
        
        public ActionResult Cars(string search)
        {
            ViewBag.detail = db.tblcars;
            if (search == "Surat")
            {
                var model = db.tblcars.Where(emp => emp.City.StartsWith(search)).ToList();
                ViewBag.detail = db.tblcars.Where(x => x.City == "Surat");
                return View();
            }
            else if (search == "Vadodara")
            {
                var model = db.tblcars.Where(emp => emp.City.StartsWith(search)).ToList();
                ViewBag.detail = db.tblcars.Where(x => x.City == "Vadodara");
                return View();
            }
            else if (search == "Ahmedabad")
            {
                var model = db.tblcars.Where(emp => emp.City.StartsWith(search)).ToList();
                ViewBag.detail = db.tblcars.Where(x => x.City == "Ahmedabad");
                return View();
            }
            else
            {
                return View();
            }
        }
        public ActionResult CarsDetail(int id = 0)
        {
            ViewBag.info = db.tblcars;
            ViewBag.contact = db.tbllocations;
            ViewBag.detail = db.tblcars;
            if (id > 0)
            {
                
                return View(db.tblcars.Find(id));
            }
            
            return View("Cars");
        }
        public ActionResult ContactUs()
        {
            ViewBag.contact = db.tbllocations;
            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(FormCollection fc)
        {
            ViewBag.info = db.tblcars;
            ViewBag.contact = db.tbllocations;
            tblusermessage umessage = new tblusermessage();
            umessage.Name = fc["name"];
            umessage.Email = fc["email"];
            umessage.Subject = fc["subject"];
            umessage.Message = fc["message"];

            tbluser user = db.tblusers.Where(x => x.Email == umessage.Email).FirstOrDefault();
            if (user != null)
            {
                db.tblusermessages.Add(umessage);
                db.SaveChanges();
                ViewBag.msg = "Your message sent !!";
            }
            else
            {
                ViewBag.msg = "Please Registration first !!";
                return View();
            }
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }        
        public ActionResult Team()
        {
            return View();
        }
        public ActionResult Testimonials()
        {
            return View();
        }
        
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(FormCollection fc)
        {
            tbluser user = new tbluser();

            user.Name = fc["name"];
            user.Email = fc["email"];
            user.Username = fc["username"];
            user.Password = fc["password"];

            db.tblusers.Add(user);
            db.SaveChanges();

            return View("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            ViewBag.info = db.tblcars;
            ViewBag.contact = db.tbllocations;
            string username = fc["username"];
            string password = fc["password"];

            tbladmin admin = db.tbladmins.Where(x => x.Name == username && x.Password == password).FirstOrDefault();
            if (admin != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                tbluser user = db.tblusers.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
                if (user != null)
                {
                    Session["username"] = user.Username;
                    Session["password"] = user.Password;
                    return View("Index");
                }
                else
                {
                    ViewBag.error = "Please enter correct email and password";
                }

                return View();
            }
        }

    }
}