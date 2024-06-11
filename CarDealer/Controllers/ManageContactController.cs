using CarDealer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealer.Controllers
{
    public class ManageContactController : Controller
    {
        private MyAppDBEntities2 db = new MyAppDBEntities2();
        // GET: ManageContact
        public ActionResult ContactDetails()
        {
            if (Session["info"] == null)
            {
                Session["info"] = new tbllocation();
            }
            ViewBag.data = db.tbllocations;
            return View();

        }
        [HttpPost]
        public ActionResult ContactDetails(FormCollection fc)
        {
            var id = Convert.ToInt32(fc["conid"]);


            if (id > 0)
            {
                tbllocation condetail = db.tbllocations.Find(id);

                condetail.Phone = fc["Phone"];
                condetail.Email = fc["Email"];
                condetail.Address = fc["Address"];

                db.SaveChanges();

            }
            else
            {
                tbllocation condetail = new tbllocation()
                {
                    Phone = fc["Phone"],
                    Email = fc["Email"],
                    Address = fc["Address"]

                };
                db.tbllocations.Add(condetail);
                db.SaveChanges();

            }
            Session["info"] = new tbllocation();
            ViewBag.data = db.tbllocations;

            return View();

        }
        public ActionResult Delete(int id)
        {
            ViewBag.data = db.tbllocations;
            tbllocation contactdetail = db.tbllocations.Find(id);
            if (contactdetail != null)
            {
                db.tbllocations.Remove(contactdetail);
                db.SaveChanges();
            }

            return RedirectToAction("ContactDetails");
        }
        public ActionResult Edit(int id)
        {
            tbllocation contactdetail = db.tbllocations.Find(id);
            if (contactdetail != null)
            {
                Session["info"] = contactdetail;
            }
            return RedirectToAction("ContactDetails");
        }
    }
}