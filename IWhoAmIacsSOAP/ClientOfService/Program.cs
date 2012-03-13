using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ClientOfService
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WhoAmI.WhoAmIClient();
            client.ClientCredentials.UserName.UserName = "Alice";
            client.ClientCredentials.UserName.Password = "changeit";
            client.ClientCredentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            var resp = client.Get();
            Console.WriteLine("Resultado: {0}", resp);
            Console.ReadLine();
        }
    }
}
