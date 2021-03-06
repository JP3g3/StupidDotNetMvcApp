﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassicGarage.DAL;
using ClassicGarage.Models;

namespace ClassicGarage.Controllers
{
    [Authorize]
    public class RepairController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Repair
        public ActionResult Index()
        {
            var repair = db.Repair.Include(r => r.Car);
            return View(repair.ToList());
        }

        // GET: Repair/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairModels repairModels = db.Repair.Find(id);
            if (repairModels == null)
            {
                return HttpNotFound();
            }
            return View(repairModels);
        }

        // GET: Repair/Create
        public ActionResult Create()
        {
            ViewBag.CarID = new SelectList(db.Car, "ID", "Mark");
            return View();
        }

        // POST: Repair/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CarID,Name,Desc,PriceOfRepair")] RepairModels repairModels)
        {
            if (ModelState.IsValid)
            {
                db.Repair.Add(repairModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarID = new SelectList(db.Car, "ID", "Mark", repairModels.CarID);
            return View(repairModels);
        }

        // GET: Repair/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairModels repairModels = db.Repair.Find(id);
            if (repairModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarID = new SelectList(db.Car, "ID", "Mark", repairModels.CarID);
            return View(repairModels);
        }

        // POST: Repair/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarID,Name,Desc,PriceOfRepair")] RepairModels repairModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repairModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarID = new SelectList(db.Car, "ID", "Mark", repairModels.CarID);
            return View(repairModels);
        }

        // GET: Repair/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairModels repairModels = db.Repair.Find(id);
            if (repairModels == null)
            {
                return HttpNotFound();
            }
            return View(repairModels);
        }

        // POST: Repair/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RepairModels repairModels = db.Repair.Find(id);
            db.Repair.Remove(repairModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
