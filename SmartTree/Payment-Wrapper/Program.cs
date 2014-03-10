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
            /*Paymill.ApiKey = "0e63b44b07fc9bfca24003be5da3d93e";
            Paymill.ApiUrl = "https://api.paymill.com/v2";


            TransactionService transactionService = Paymill.GetService<TransactionService>();

            List<Transaction> lstTransactions = transactionService.GetTransactions();
            */

            PaymillHelper payment = new PaymillHelper("https://api.paymill.com/v2", "0e63b44b07fc9bfca24003be5da3d93e");

            var client = payment.CreateCustomer(RandomString(10) + "@yopmail.com", "testCustomer");
            client.AddCreditCard("tok_7434d9cde1277ff96635", payment);
            client = payment.getCustomers().FirstOrDefault();
            client.Pay(payment, 1);
            client.Withdraw(payment, 3);
            payment.DeleteCreditCard(client.Payment.FirstOrDefault().Id);
            /*
            foreach (Client c in payment.getCustomers())
            {
                payment.DeleteCustomer(c);
            }*/
       }

        private static void createCustomers(int nb, PaymillHelper ph)
        {
            for (int i = 0; i < 10; i++)
            {
                var client = ph.CreateCustomer(RandomString(10)+ "@yopmail.com", "testCustomer" + i);
                
            }
        }

        static private readonly Random _rng = new Random();
        private const string _chars = "abcedefghijklmnopqrstuvwxz";

        private static string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }

    }
}
