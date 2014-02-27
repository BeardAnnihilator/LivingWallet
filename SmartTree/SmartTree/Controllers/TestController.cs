using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartTree.Models;

namespace SmartTree.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public bool Index()
        {
            string apiUrl = "https://api.paymill.com/v2/";
            string apiKey = "0e63b44b07fc9bfca24003be5da3d93e";

            PaymillWrapper.Paymill.ApiKey = apiKey;
            PaymillWrapper.Paymill.ApiUrl = apiUrl;

            PaymillWrapper.Service.ClientService clientService = PaymillWrapper.Paymill.GetService<PaymillWrapper.Service.ClientService>();

            PaymillWrapper.Models.Client c = new PaymillWrapper.Models.Client();
            c.Description = "Prueba API";
            c.Email = "javicantos22@hotmail.es";

            PaymillWrapper.Models.Client newClient = clientService.Create(c.Email, c.Description);
            return true;
        }

    }
}
