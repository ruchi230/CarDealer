using CarDealer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealer.Controllers
{
    public class ManageCategoryController : Controller
    {
        private MyAppDBEntities2 db = new MyAppDBEntities2();
        // GET: ManageCategory
        public ActionResult Category()
        {
            ViewBag.data = db.tblcars;
            return View();
        }
        public ActionResult AddEditCategory(int id = 0)
        {
            ViewBag.data = db.tblcars;

            if (id > 0)
            {
                return View(db.tblcars.Find(id));
            }
            else
            {
                return View(new tblcar());
            }

        }
        [HttpPost]
        public ActionResult AddEditCategory(tblcar car, FormCollection fc, HttpPostedFileBase ufile)
        {
            var id = Convert.ToInt32(fc["cid"]);
            ViewBag.data = db.tblcars;

            if (id > 0)
            {
                tblcar ecar = db.tblcars.Find(id);

                ecar.CarPrice = car.CarPrice;
                ecar.OfferPrice = car.OfferPrice;
                ecar.City = car.City;
                ecar.Make = car.Make;
                ecar.Model = car.Model;
                ecar.FirstRegistration = car.FirstRegistration;
                ecar.Mileage = car.Mileage;
                ecar.Fuel = car.Fuel;
                ecar.EngineSize = car.EngineSize;
                ecar.Power = car.Power;
                ecar.GearBox = car.GearBox;
                ecar.NumberOfSeat = car.NumberOfSeat;
                ecar.Doors = car.Doors;
                ecar.Color = car.Color;
                ecar.Description = car.Description;
                ecar.Extra = car.Extra;

                if (ufile != null)
                {
                    ecar.Image = "/Image/" + ufile.FileName;
                    ufile.SaveAs(Server.MapPath(ecar.Image));
                }
                else
                {
                    ecar.Image = fc["imgval"].ToString();
                }


                db.SaveChanges();

                return RedirectToAction("Category");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    car.Image = "/Image/" + ufile.FileName;
                    ufile.SaveAs(Server.MapPath(car.Image));

                    db.tblcars.Add(car);
                    db.SaveChanges();

                    return View("Category");
                }
            }

            return View(new tblcar());


        }

        public ActionResult Delete(int id)
        {
            ViewBag.data = db.tblcars;
            tblcar cardetail = db.tblcars.Find(id);
            db.tblcars.Remove(cardetail);
            db.SaveChanges();
            return RedirectToAction("Category");
        }
    }
}