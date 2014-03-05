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
            PaymillHelper payment = new PaymillHelper("https://api.paymill.com/v2", "0e63b44b07fc9bfca24003be5da3d93e");

            var client = payment.CreateCustomer("blabla@yopmail.com", "testCustomer");
            foreach (Client c in payment.getCustomers())
            {
                payment.DeleteCustomer(c);
            }
       }
    }
}
