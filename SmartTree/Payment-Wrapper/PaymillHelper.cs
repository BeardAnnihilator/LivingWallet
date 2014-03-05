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
            transactionService = Paymill.GetService<TransactionService>();
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

        #region Transactions
        TransactionService transactionService = null;
        public Transaction CreateTransaction(Client customer, Payment creditCard, int amount, string currency = "EUR")
        {
            Transaction createdTransaction = transactionService.Create(
                new Transaction { Payment = creditCard, Client = customer, Amount = amount, Currency = currency}
                , null);
            return createdTransaction;
        }
        public Transaction GetTransaction(string transactionId)
        {
            Transaction retriviedTransaction = transactionService.Get(transactionId);
            return retriviedTransaction;
        }
        public List<Transaction> getTransactions()
        {
            List<Transaction> transactions = transactionService.GetTransactions();
            return transactions;
        }
        #endregion

        #region Utilities
        public static int DoubleToPaymillPrice(double price)
        {
            return (int)(price * 100);
        }
        #endregion
    }
    public static class Extensions
    {
        public static Payment AddCreditCard(this Client customer, string PaymentToken ,PaymillHelper ph)
        {
            return ph.CreateCreditCard(customer, PaymentToken);
        }

        public static Transaction Pay(this Client customer, PaymillHelper ph, double amount, Payment creditCard = null)
        {
            if (creditCard == null)
                creditCard = customer.Payment.FirstOrDefault();
            return ph.CreateTransaction(customer, creditCard, PaymillHelper.DoubleToPaymillPrice(amount));
        }
    }
}
