using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ex2AppWebFbiMostWanted.Models;

namespace Ex2AppWebFbiMostWanted.Controllers
{ 
    // HACK: criado novo controlador para o modelo e o data context com views e actions red/write EF, com Razor (CSHTML)
    public class FbiMostWantedController : Controller
    {
        private readonly FbiMostWantedContext _db = new FbiMostWantedContext(); // HACK: adicionado readonly (ReSharper)

        //
        // GET: /FbiMostWanted/

        public ViewResult Index()
        {
            return View(_db.FbiMostWantedCriminals.ToList());
        }

        //
        // GET: /FbiMostWanted/Details/5

        public ViewResult Details(int id)
        {
            FbiMostWanted fbimostwanted = _db.FbiMostWantedCriminals.Find(id);
            return View(fbimostwanted);
        }

        //
        // GET: /FbiMostWanted/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /FbiMostWanted/Create

        [HttpPost]
        public ActionResult Create(FbiMostWanted fbimostwanted)
        {
            if (ModelState.IsValid)
            {
                _db.FbiMostWantedCriminals.Add(fbimostwanted);
                _db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(fbimostwanted);
        }
        
        //
        // GET: /FbiMostWanted/Edit/5
 
        public ActionResult Edit(int id)
        {
            FbiMostWanted fbimostwanted = _db.FbiMostWantedCriminals.Find(id);
            return View(fbimostwanted);
        }

        //
        // POST: /FbiMostWanted/Edit/5

        [HttpPost]
        public ActionResult Edit(FbiMostWanted fbimostwanted)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(fbimostwanted).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fbimostwanted);
        }

        //
        // GET: /FbiMostWanted/Delete/5
 
        public ActionResult Delete(int id)
        {
            FbiMostWanted fbimostwanted = _db.FbiMostWantedCriminals.Find(id);
            return View(fbimostwanted);
        }

        //
        // POST: /FbiMostWanted/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            FbiMostWanted fbimostwanted = _db.FbiMostWantedCriminals.Find(id);
            _db.FbiMostWantedCriminals.Remove(fbimostwanted);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}