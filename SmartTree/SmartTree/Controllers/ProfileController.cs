using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartTree.Models;
using SmartTree.ViewModels;
using SmartTree.Helpers;
using SmartTree.Filters;

namespace SmartTree.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/
        public ActionResult Index(int userId = -1)
        {        
            using (SmartTreeEntities db = new SmartTreeEntities())
            {
                var user = User.GetUserFromDb(db);
                userId = userId == -1 ? user.UserId : userId;
                ProfileViewModel model =  new ProfileViewModel(userId);
                return View(model);
            }
        }
            
            

    }
}
