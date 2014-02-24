using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartTree.Models;
using SmartTree.Helpers;
using System.Data.Entity.Infrastructure;


namespace SmartTree.Controllers
{
    public class AdminController : Controller
    {

        //
        // GET: /Admin/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        // POST: /Admin/AddMultiCoins
        [HttpPost, Authorize(Roles = "Admin")]
        public bool AddMultiCoins(int number)
        {
            using (SmartTreeEntities db = new SmartTreeEntities())
            {
                var user = User.GetUserFromDb(db);
                user.MultiCoins += number;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return AddMultiCoins(number);
                }
                MarketController.re_sell(user);
            }
            
            return true;
        }
    }
}
