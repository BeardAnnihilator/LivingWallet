using PaymillWrapper;
using PaymillWrapper.Models;
using PaymillWrapper.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Wrapper
{
    class Program
    {
        static void Main(string[] args)
        {

            string apiUrl = "https://api.paymill.com/v2/";
            string apiKey = "0e63b44b07fc9bfca24003be5da3d93e";

            Paymill.ApiKey = apiKey;
            Paymill.ApiUrl = apiUrl;

            ClientService clientService = Paymill.GetService<ClientService>();

            Client c = new Client();
            c.Description = "Prueba API";
            c.Email = "javicantos22@hotmail.es";

            Client newClient = clientService.Create(c.Email, c.Description);   

       }
    }
}
