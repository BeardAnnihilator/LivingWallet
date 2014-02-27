using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartTree.Models;
using SmartTree.Helpers;
using SmartTree.ViewModels;
using System.Data.Entity.Infrastructure;
using SmartTree.Filters;

namespace SmartTree.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class MarketController : Controller
    {
        //
        // GET: /Market/
        public ActionResult Index()
        {
            using (SmartTreeEntities db = new SmartTreeEntities())
            {
                var user = User.GetUserFromDb(db);
                if (user == null || user.Father != null)
                    return RedirectToAction("Index", "Profile");
            }
            MarketViewModel model = new MarketViewModel();
            return View(model);
        }

        #region -------------------------------- Buy --------------------------------
        /// <summary>
        /// Automaticly re-sell to the maximum number of Multicoins
        /// to childs who made reservations.
        /// </summary>
        public static void re_sell(User _me)
        {        
            if (_me.ReceivedCalls > 0)
            {
                using (SmartTreeEntities db = new SmartTreeEntities())
                {
                    _me = db.Users.Find(_me.UserId);

                    while (_me.MultiCoins != 0 && _me.ReceivedCalls != 0)
                    {
                        foreach (User child in _me.Childs.Where(u => u.EmitedCalls > 0).ToList())
                        {
                            var amounth = Math.Min(child.EmitedCalls, _me.MultiCoins);
                            if (!buy(child, child.Father, amounth))
                                break;
                            _me.MultiCoins -= amounth;
                        }
                        _me = db.Users.Find(_me.UserId);
                    }
                }
            }
        }

        /// <summary>
        /// Reception of taxes on a taxed sell
        /// Split the money in Rank + 1 parts
        /// takes his part and give the rest to his father
        /// (Recurvise)
        /// </summary>
        /// <param name="balance"></param>
        private static void give(User Father, double balance)
        {
            if (Father.Rank == 0)
                Father.Balance += balance;
            else
            {
                double my_part = (double)(balance / (Father.Rank + 1));
                Father.Balance += my_part;
                give(Father.Father, balance - my_part);
            }
        }

        /// <summary>
        /// Update the Balance after a sell
        /// for an untaxed sell, user takes 100%
        /// for a taxed sell, user takes 50%
        /// and give the rest to his father.
        /// </summary>
        /// <param name="amounth"></param>
        private static void sell(User Seller, int amounth)
        {
            var untaxedPart = Math.Min(Seller.untaxed, amounth);
            var taxedPart = amounth - untaxedPart;

            Seller.untaxed -= untaxedPart;
            Seller.Balance += Constants.MULTI_COIN_PRICE * untaxedPart;
            Seller.Balance += Constants.MULTI_COIN_PRICE * (double)taxedPart / 2;
            give(Seller.Father, Constants.MULTI_COIN_PRICE * (double)taxedPart / 2);
        }

        /// <summary>
        /// Buy to an other user
        /// </summary>
        /// <param name="_other"> The other User we want to buy to</param>
        /// <param name="amounth"> The amounth of multicoins we want to buy</param>
        /// <returns></returns>
        private static Boolean buy(User _me, User _other, int amounth = 1)
        {
            // First check if _other is the father of the user or
            // if user has no father, an elligible seller
            if (_me.Balance >= Constants.MULTI_COIN_PRICE * amounth 
                && _other.MultiCoins >= amounth
                && (_me.Father == _other
                || (_me.Father == null && _other.isElligibleSeller())))
            {
                // Proceed the sell
                using (SmartTreeEntities db = new SmartTreeEntities())
                {
                    var other = db.Users.Find(_other.UserId);
                    var me = db.Users.Find(_me.UserId);

                    //Update seller infos
                    other.MultiCoins -= amounth;
                    sell(other, amounth);

                    //Update user infos
                    me.Balance -= Constants.MULTI_COIN_PRICE * amounth;
                    me.MultiCoins += amounth * 5;
                    me.untaxed += amounth;

                    // update infos about calls
                    var nbCallToCancel = Math.Min(_me.EmitedCalls, amounth);
                    me.EmitedCalls -= nbCallToCancel;
                    me.ReservedPartOfBalance -= Constants.MULTI_COIN_PRICE * nbCallToCancel;
                    other.ReceivedCalls -= nbCallToCancel;

                    // bind seller & buyer if it is not the case yet
                    if (me.Father == null)
                    {
                        me.Father = other;
                        other.Childs.Add(me);
                        me.Rank = other.Rank + 1;
                    }
                    try { db.SaveChanges(); }
                    catch (DbUpdateConcurrencyException e) { return buy(_me, _other, amounth); }
                }
                re_sell(_me);
                return true;
            }
            return false;
        }

        public ActionResult Buy(int userId)
        {
            using (SmartTreeEntities db = new SmartTreeEntities())
            {
                var seller = db.Users.Find(userId);
                var user = User.GetUserFromDb(db);
                if ((seller != null && user.Father == null)
                    || (user.FatherId == seller.UserId))
                {
                    if (!buy(user, seller))
                    {
                        return RedirectToAction("Index", "Market");
                    }
                }
            }
            return RedirectToAction("Index","Profile");
        }


        #endregion

        #region -------------------------------- Call -------------------------------
        /// <summary>
        /// Call the father
        /// </summary>
        /// <param name="amounth"></param>
        /// <returns></returns>
        private static Boolean call(User _me, int amounth = 1)
        {
            //Check if the user has enough money to call
            if (_me.Balance - _me.ReservedPartOfBalance >= Constants.MULTI_COIN_PRICE * amounth)
            {
                // if the amounth is 0 or less, do nothing and return true
                if (amounth > 0)
                {
                    //check if the father has some Multicoins to sell before calling him
                    if (_me.Father.MultiCoins != 0)
                    {
                        // buy the available multicoins.
                        var amounthBuy = Math.Min(_me.Father.MultiCoins, amounth);
                        if (buy(_me, _me.Father, amounthBuy))
                        {
                            // Call for the difference
                            return call(_me, amounth - amounthBuy);
                        }
                        // If buy failed recall call function
                        return call(_me, amounth);
                    }

                    // If there is no available multicoins, proceed call
                    using (SmartTreeEntities db = new SmartTreeEntities())
                    {
                        var user = db.Users.Find(_me.UserId);
                        var father = db.Users.Find(_me.FatherId);

                        user.EmitedCalls += amounth;
                        user.ReservedPartOfBalance += Constants.MULTI_COIN_PRICE * amounth;
                        father.ReceivedCalls += amounth;
                        try{ db.SaveChanges(); }
                        catch (DbUpdateConcurrencyException e) { return call(_me, amounth); }
                    }
                }
                return true;
            }
            return false;
        }



        public ActionResult Call()
        {
            using (SmartTreeEntities db = new SmartTreeEntities())
            {
                var user = User.GetUserFromDb(db);
                if (user.FatherId != null)
                {
                    if (!call(user))
                    {
                        //TODO
                    }
                }
            }
            return RedirectToAction("Index", "Profile");
        }

        #endregion

        public ActionResult Deposit()
        {
            using (SmartTreeEntities db = new SmartTreeEntities())
            {
                var user = User.GetUserFromDb(db);
                user.Balance += Constants.MULTI_COIN_PRICE;
                try { db.SaveChanges(); }
                catch (DbUpdateConcurrencyException e){ Deposit(); }

            }
            return RedirectToAction("Index", "Profile");
        }

        public ActionResult Withdraw()
        {
            using (SmartTreeEntities db = new SmartTreeEntities())
            {
                var user = User.GetUserFromDb(db);
                user.Balance -= Constants.MULTI_COIN_PRICE;
                try { db.SaveChanges(); }
                catch (DbUpdateConcurrencyException e) { Withdraw(); }
            }
            return RedirectToAction("Index", "Profile");
        }
    }
}
