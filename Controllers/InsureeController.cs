using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private static List<Insuree> db = new List<Insuree>();

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Insuree insuree)
        {
            decimal quote = 50;

            int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
            if (DateTime.Now.DayOfYear < insuree.DateOfBirth.DayOfYear) age--;

            if (age <= 18) quote += 100;
            else if (age <= 25) quote += 50;
            else quote += 25;

            if (insuree.CarYear < 2000) quote += 25;
            if (insuree.CarYear > 2015) quote += 25;

            if (insuree.CarMake.ToLower() == "porsche")
            {
                quote += 25;
                if (insuree.CarModel.ToLower() == "911 carrera") quote += 25;
            }

            quote += insuree.SpeedingTickets * 10;
            if (insuree.DUI) quote *= 1.25m;
            if (insuree.CoverageType == "Full") quote *= 1.5m;

            insuree.Quote = quote;

            db.Add(insuree);

            return RedirectToAction("Admin");
        }

        // GET: Insuree/Admin
        public ActionResult Admin()
        {
            return View(db);
        }
    }
}
