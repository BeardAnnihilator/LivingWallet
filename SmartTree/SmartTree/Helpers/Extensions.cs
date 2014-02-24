using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartTree.Models;

namespace SmartTree.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// finds db user from controller user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static User GetUserFromDb(this System.Security.Principal.IPrincipal user, SmartTreeEntities db)
        {
            try
            {
                return user == null || String.IsNullOrEmpty(user.Identity.Name) ?
                    null
                    : db.Users.SingleOrDefault(u => u.Email == user.Identity.Name);
            }
            catch
            {
                return null;
            }
        }
    }
}