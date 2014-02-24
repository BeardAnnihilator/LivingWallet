using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartTree.Models;

namespace SmartTree.ViewModels
{
    public class MarketViewModel
    {

        public List<User> Sellers { get; set; }

        public MarketViewModel()
        {
            using (SmartTreeEntities db = new SmartTreeEntities())
            {
                List<User> sellers = User.GetElligiblesSellers(db);
                Sellers = sellers;
            }
        }
    }
}