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
    public class PaymillHelper
    {
        #region Inisalization
        public PaymillHelper(string apiUrl, string apiKey)
        {
            Connect(apiUrl, apiKey);
        }

        public void Connect(string apiUrl, string apiKey)
        {
            PaymillWrapper.Paymill.ApiKey = apiKey;
            PaymillWrapper.Paymill.ApiUrl = apiUrl;
            clientService = Paymill.GetService<ClientService>();
            paymentService = Paymill.GetService<PaymentService>();
        }
        #endregion

        #region Customers
        ClientService clientService = null;
        public Client CreateCustomer(string Email, string Description)
        {
            Client newClient = clientService.Create(Email, Description);
            return newClient;
        }

        public List<Client> getCustomers()
        {
            List<Client> clients = clientService.GetClients();
            return clients;
        }

        public Client UpdateCustomer(Client customer)
        {
            Client updatedClient = clientService.Update(customer);
            return updatedClient;
        }

        public bool DeleteCustomer(Client customer)
        {
            bool response = clientService.Remove(customer.Id);
            return response;
        }
        #endregion

        #region CreditCards
        PaymentService paymentService = null;
        public Payment CreateCreditCard(Client customer, string PaymentToken)
        {
            Payment payment = paymentService.Create(PaymentToken, customer.Id);
            return payment;
        }
        public Payment GetCreditCard(string PaymentId)
        {
            Payment payment = paymentService.Get(PaymentId);
            return payment;
        }
        public List<Payment> GetCreditCards()
        {
            List<Payment> payments = paymentService.GetPayments();
            return payments;
        }
        public bool DeleteCreditCard(string PaymentId)
        {
            bool response = paymentService.Remove(PaymentId);
            return response;
        }
        #endregion
    }
    public static class Extensions
    {
        public static Payment AddCreditCard(this Client customer, string PaymentToken ,PaymillHelper ph)
        {
            return ph.CreateCreditCard(customer, PaymentToken);
        }
    }
}
