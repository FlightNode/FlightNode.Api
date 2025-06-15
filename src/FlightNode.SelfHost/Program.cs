using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Host.HttpListener;
using Owin;
using FlightNode.Service.App;

namespace FlightNode.Selfhost
{
    class Program
    {
        static void Main()
        {
            var address = "http://localhost:8000/";
            WebApp.Start<Startup>(url: address);

            Console.WriteLine("Listening on " + address);
            Console.ReadKey();
        }
    }
}
