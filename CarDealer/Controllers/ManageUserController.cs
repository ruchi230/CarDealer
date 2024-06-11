using CarDealer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealer.Controllers
{
    public class ManageUserController : Controller
    {
        private MyAppDBEntities2 db = new MyAppDBEntities2();
        // GET: ManageUser
        public ActionResult AddEditUser()
        {
            ViewBag.list = db.tblusermessages;
            return View();
        }
        public ActionResult DeleteUser(int id)
        {
            ViewBag.list = db.tblusermessages;
            tblusermessage detail = db.tblusermessages.Find(id);
            db.tblusermessages.Remove(detail);
            db.SaveChanges();
            return RedirectToAction("AddEditUser");
        }
    }
}