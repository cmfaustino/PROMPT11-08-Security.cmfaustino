using System;
using System.Web.Mvc;
using System.Web.Security;
using Ex2AppWebFbiMostWanted.Models;
using Microsoft.IdentityModel.Protocols.WSFederation;
using Microsoft.IdentityModel.Web;

namespace Ex2AppWebFbiMostWanted.Controllers
{
    public class AccountController : Controller
    {

        // HACK: metodo/action fica comentado, para esconder a sua existencia de acesso directo externo
        //public ActionResult LogOnFederated()
        //{
        //    //throw new NotImplementedException();
        //    return View();
        //}

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            // HACK: constante para seleccao do home realm na aplicacao web e nao no ACS

            // da' erro/nao servem de nada, pois e' necessario .cshtml ou .aspx a servir de intermediario
            //const string localLoginPageUrl = "~/Account/applocal2LoginPageCode.html";
            //const string localLoginPageUrl = "applocal2LoginPageCode.html";

            // view em .aspx a servir de intermediario para applocal2LoginPageCode.html
            //const string localLoginPageUrl = "~/Account/LogOnFederated";
            const string localLoginPageIntermediateViewName = "LogOnFederated";

            // HACK: em principio, sempre verdadeiro neste metodo (ou com !User.Identity.IsAuthenticated)
            //if (!Request.IsAuthenticated)
            //{
            //}

            // HACK: utilizar FederatedAuthentication, como alternativa ao pre-definido return View();
            // HACK: try..catch realizado,para caso do web.config ser alterado para nao ter WS-Federation
            try
            {
                /* 1 - tentar utilizar federacao:
                 * se conseguir (wsfam nao e' null, e nao ocorrem erros), faz redirect... */
                var wsfam = FederatedAuthentication.WSFederationAuthenticationModule;
                if (wsfam != null)
                {
                    var signInRequest = new SignInRequestMessage(new Uri(wsfam.Issuer), wsfam.Realm,
                                                                                            wsfam.Reply)
                    {
                        AuthenticationType = wsfam.AuthenticationType,
                        Context = wsfam.Realm,
                        Freshness = wsfam.Freshness,
                        HomeRealm = wsfam.HomeRealm
                    };

                    // 1.1 - seleccao do home realm no ACS
                    //return Redirect(signInRequest.WriteQueryString());

                    // 1.2 - seleccao do home realm na aplicacao web
                    
                    //return View("applocal2LoginPageCode"); // da' erro
                    
                    // HACK: action fica comentada, para esconder existencia de acesso directo externo,
                    // logo, redirect's tambem ficam comentados, para nao haver action com acesso externo
                    //return Redirect(localLoginPageUrl); // nao da' erro, se for a action LogOnFederated
                    //return RedirectToAction(localLoginPageIntermediateViewName, "Account");
                    
                    /* retorna-se a View, e assim, nao ha' acesso externo directo/explicto
                     * a esta view, pois nao existe action que corresponda apenas a esta view */
                    return View(localLoginPageIntermediateViewName);
                }
            }
            catch (Exception)
            {
                // 2 - ...em caso de erro, nao faz nada, pois...
                //throw;
            }
            // 3 - ...caso wsfam seja igual a null, ou em caso de erro, processa normalmente o LogOn
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            // HACK: em principio, sempre verdadeiro neste metodo (ou com User.Identity.IsAuthenticated)
            //if (Request.IsAuthenticated)
            //{
            //}

            // HACK: utilizar FederatedAuthentication, para alem do normal FormsAuthentication.SignOut();
            // HACK: try..finally realizado, caso do web.config ser alterado para nao ter WS-Federation
            SignOutRequestMessage signOutRequestMessage = null;
            try
            {
                /* 1 - tentar utilizar federacao: se conseguir (wsfam nao e' null, e nao ocorrem erros),
                 * tem-se signOutRequestMessage diferente de null */
                var wsfam = FederatedAuthentication.WSFederationAuthenticationModule;
                if (wsfam != null)
                {
                    /* sign-out local, para o caso de o utilizador poder estar a navegar, ao mesmo tempo,
                     * em algum site relacionado com o identity provider */
                    wsfam.SignOut(true);

                    /* 3 hipoteses distintas de sign-out remoto, utilizadas apenas
                     * durante uma versao intermedia da implementacao -
                     * - para verificar que o ACS nao regressa ao replyUrl,
                     * pois mostra uma pagina HTML contendo mensagem
                     * para apagar cookies e fechar janelas de browser -
                     * - mas ja' nao utilizadas na versao final da implementacao */

                    //signOutRequestMessage = new SignOutRequestMessage(new Uri(wsfam.Issuer),
                    //                                                    wsfam.Realm);

                    //WSFederationAuthenticationModule
                    //                    .FederatedSignOut(new Uri(wsfam.Issuer), new Uri(wsfam.Realm));

                    //var returnUrl2 = WSFederationAuthenticationModule
                    //                    .GetFederationPassiveSignOutUrl(wsfam.Issuer, wsfam.Realm, "");
                    //return Redirect(returnUrl2);
                }
            }
            finally
            {
                // 2 - em qualquer caso, fazer sign-out pre'-definido em ASP.NET MVC
                FormsAuthentication.SignOut();
            }
            // 3 - se signOutRequestMessage e' diferente de null, faz redirect especifico da federacao...
            if (signOutRequestMessage != null)
            {
                var returnUrl1 = signOutRequestMessage.WriteQueryString();
                return Redirect(returnUrl1);
            }

            // 4 - ...caso contrario, faz redirect normal
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true,
                                                                                null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName,
                                                                    false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership
                        .GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser
                                                .ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("",
                        "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
