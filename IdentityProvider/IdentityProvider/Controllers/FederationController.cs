using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Configuration;
using Microsoft.IdentityModel.Protocols.WSFederation;
using Microsoft.IdentityModel.Protocols.WSTrust;
using Microsoft.IdentityModel.SecurityTokenService;
using Microsoft.IdentityModel.Web;

namespace IdentityProvider.Controllers
{
    public class SimpleSecurityTokenServiceConfiguration : SecurityTokenServiceConfiguration
    {
        public SimpleSecurityTokenServiceConfiguration()
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            var credCert = store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, "sign.idp.prompt11.local",
                                               true)[0];
            SigningCredentials = new X509SigningCredentials(credCert); // HACK: fazer tambem Add Reference - System.IdentityModel
        }
    }

    public class SimpleSecurityTokenService : SecurityTokenService
    {
        public SimpleSecurityTokenService(SecurityTokenServiceConfiguration securityTokenServiceConfiguration) : base(securityTokenServiceConfiguration)
        {
        }

        protected override Scope GetScope(IClaimsPrincipal principal, RequestSecurityToken request)
        {
            throw new NotImplementedException();

            var scope = new Scope();
            return scope;
        }

        protected override IClaimsIdentity GetOutputClaimsIdentity(IClaimsPrincipal principal, RequestSecurityToken request, Scope scope)
        {
            throw new NotImplementedException();
        }
    }

    public class FederationController : Controller
    {
        [Authorize]
        public ActionResult Issue() // HACK: fazer tambem Add Reference - Microsoft.IdentityModel
        {
            var req = WSFederationMessage.CreateFromUri(Request.Url);
            var resp = FederatedPassiveSecurityTokenServiceOperations.ProcessSignInRequest(
                req as SignInRequestMessage,
                User,
                new SimpleSecurityTokenService(new SimpleSecurityTokenServiceConfiguration()));
            resp.Write(Response.Output);
            Response.Flush();
            Response.End();
            
            return View();
        }

        //
        // GET: /Federation/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Federation/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Federation/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Federation/Create

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
        // GET: /Federation/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Federation/Edit/5

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
        // GET: /Federation/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Federation/Delete/5

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
