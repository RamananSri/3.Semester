using System;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using DataAccessLayer.Modules;
using ServiceLayer;

namespace HostConsole
{
    internal class Program
    {
  
        private static void Main(string[] args)
        {
            using (var serviceHost = new ServiceHost(typeof(BikeService)))
            {
                serviceHost.Open();
                Console.WriteLine("Bike'n'Bike Service 1.0");
                Console.WriteLine(DateTime.Now);
                Console.WriteLine();
                Console.WriteLine("Press the Enter key to terminate service.");
                Console.WriteLine();
                Console.ReadLine();  
            }
        }
    }
}