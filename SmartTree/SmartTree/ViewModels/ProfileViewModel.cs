using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartTree.Models;
using SmartTree.Helpers;
using SmartTree.Controllers;

namespace SmartTree.ViewModels
{
    public class ProfileViewModel : MarketViewModel
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public double Balance { get; set; }
        public double ReservedPart { get; set; }
        public int MultiCoins { get; set; }
        public User Seller { get; set; }
        public List<User> Buyers { get; set; }
        public int EmitedCalls { get; set; }
        public int ReceivedCalls { get; set; }

        public ProfileViewModel(int userId)
        {
            using (SmartTreeEntities db = new SmartTreeEntities())
            {
                var user = db.Users.Find(userId);

                Email = user.Email;
                Id = user.UserId;
                Balance = user.Balance;
                ReservedPart = user.ReservedPartOfBalance;
                MultiCoins = user.MultiCoins;
                Seller = user.Father;
                Buyers = user.Childs;
                EmitedCalls = user.EmitedCalls;
                ReceivedCalls = user.ReceivedCalls;
            }
        }

    }
}