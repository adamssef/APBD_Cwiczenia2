using LegacyApp;
using System;

namespace LegacyAppConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * DO NOT CHANGE THIS FILE AT ALL
             */

            IClientRepository respository = new ClientRepository();
            var userService = new UserService(respository);
            var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
            if (addResult)
                Console.WriteLine($"Adding John Doe was successful");
            else
                Console.WriteLine($"Adding John Doe was unsuccessful");
        }
    }
}
