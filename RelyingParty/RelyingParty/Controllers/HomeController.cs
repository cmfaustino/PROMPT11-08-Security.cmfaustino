using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Claims;

namespace RelyingParty.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /DefaultTesting/

        public string Index()
        {
            var ident = this.User as IClaimsPrincipal;
            var emailClaim = ident.Identities[0].Claims.Where(c => c.ClaimType == ClaimTypes.Email).FirstOrDefault();
            return "Hi there " + (emailClaim != null ? emailClaim.Value : "stranger") + " _e tambem_ " + "Hello Sandra";
        }

        //
        // GET: /DefaultTesting/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /DefaultTesting/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /DefaultTesting/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /DefaultTesting/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DefaultTesting/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /DefaultTesting/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DefaultTesting/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
