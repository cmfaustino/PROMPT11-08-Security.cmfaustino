using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web; // add reference system.web

namespace OAuthNativo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Client ID: 	1049245660536-fanhehjskijr7g2ctafv92jcpr6jinur.apps.googleusercontent.com
            // Client secret: 	YfVmKceZmCVYAC6cnsJ8qOSW
            // Redirect URIs: 	urn:ietf:wg:oauth:2.0:oob
            // http://localhost

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
                "1049245660536-fanhehjskijr7g2ctafv92jcpr6jinur.apps.googleusercontent.com", // "1049245660536.apps.googleusercontent.com",
                "urn:ietf:wg:oauth:2.0:oob", // "HttpUtility.UrlEncode("http://localhost/oauth2callback"),
                HttpUtility.UrlEncode("https://www.googleapis.com/auth/tasks.readonly"),
                ""
                //,
                //"offline",
                //"auto"
            );

            var processo = System.Diagnostics.Process.Start("IExplore.exe", urlRedirectLogin);
            while (processo == null || (processo.HasExited)) // processo pode ser null ou nao, conforme o browser, por exemplo.
            {
                ;
            }
            var codigo = processo.MainWindowTitle;
            Console.WriteLine("Codigo: {0}", codigo);
            Console.ReadKey();
        }
    }
}
