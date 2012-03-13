using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OAuthCliente.Controllers
{
    public class HomeController : Controller
    {
        public string GetToken()
        {
            return ((HttpContext.Session != null) ? HttpContext.Session["OAuthToken"] : "") as string;
        }

        public void SetToken(string str)
        {
            if (HttpContext.Session != null)
            {
                HttpContext.Session["OAuthToken"] = str;
            }
        }

        //
        // GET: /Home/Tasks/

        public ActionResult Tasks()
        {
            string urlRedirect;
            if (GetToken().Length > 0) // HACK: Google - pedido das Tasks
            {
                throw new NotImplementedException();
            }
            else
            {
                var reqCode = HttpContext.Request.QueryString.Get("code");
                if (reqCode.Length > 0) // HACK: Google - code para token
                {
                    throw new NotImplementedException();
                }
                else // HACK: Google OAuth2Login
                {
                    var urlRedirectLogin = string.Format("https://accounts.google.com/o/oauth2/auth"
                        + "?response_type={0}"
                        + "&client_id={1}"
                        + "&redirect_uri={2}"
                        + "&scope={3}"
                        + "&state={4}"
                        //+ "&access_type={5}"
                        //+ "&approval_prompt={6}"
                        ,
                        "code",
                        "1049245660536.apps.googleusercontent.com",
                        HttpUtility.UrlEncode("http://localhost/oauth2callback"),
                        HttpUtility.UrlEncode("https://www.googleapis.com/auth/tasks.readonly"),
                        ""
                        //,
                        //"offline",
                        //"auto"
                    );
                    return new RedirectResult(urlRedirectLogin);
                }
            }
            // HACK: copia
            if (string.IsNullOrEmpty(Request.Params["code"]))
            {
                return new RedirectResult("https://accounts.google.com/o/oauth2/auth?" +
                    "response_type=code&client_id=646897896780.apps.googleusercontent.com&" +
                    "redirect_uri=" + HttpUtility.UrlEncode("http://localhost:49876/Tasks") +
                    "&scope=" + HttpUtility.UrlEncode("https://www.googleapis.com/auth/tasks.readonly"));
            }
            else
            {
                var queryParams = new List<KeyValuePair<string, string>>();
                queryParams.Add(new KeyValuePair<string, string>("code", Request.Params["code"]));
                queryParams.Add(new KeyValuePair<string, string>("redirect_uri", "http://localhost:49876/Tasks"));
                queryParams.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                queryParams.Add(new KeyValuePair<string, string>("client_id", "646897896780.apps.googleusercontent.com"));
                queryParams.Add(new KeyValuePair<string, string>("client_secret", "1HuwNjxEeNFIMd090yL1uR_X"));
                var form = new FormUrlEncodedContent(queryParams);
                var httpClient = new HttpClient();
                var postResult = httpClient.PostAsync("https://accounts.google.com/o/oauth2/token", form).Result;
                var stream = postResult.Content.ReadAsStreamAsync().Result;
                var value = JsonValue.Load(stream);

                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue((string)value["token_type"], (string)value["access_token"]);
                var stream1 = httpClient.GetAsync("https://www.googleapis.com/tasks/v1/users/sandrapatfer/lists?alt=json&prettyPrint=true").
                    Result.Content.ReadAsStreamAsync().Result;
                var value2 = JsonValue.Load(stream1);
                return Content((string)value2);
            }
        }

        //
        // GET: /Home/TasksImplicitExample/

        public ActionResult TasksImplicitExample()
        {
            return View();
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Home/Create

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
        // GET: /Home/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5

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
        // GET: /Home/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5

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
