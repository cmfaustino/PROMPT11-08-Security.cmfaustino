using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace IWhoAmIacsSOAP.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WhoAmI" in code, svc and config file together.
    public class WhoAmI : IWhoAmI
    {
        public string Get()
        {
            return String.Format("Identity ToString: {0}", Thread.CurrentPrincipal.Identity.ToString())
                + "\n" + String.Format("Identity AuthenticationType: {0}", Thread.CurrentPrincipal.Identity.AuthenticationType)
                + "\n" + String.Format("Identity IsAuthenticated: {0}", Thread.CurrentPrincipal.Identity.IsAuthenticated.ToString())
                + "\n" + String.Format("Identity Name: {0}", Thread.CurrentPrincipal.Identity.Name)
                ;
        }
    }
}
