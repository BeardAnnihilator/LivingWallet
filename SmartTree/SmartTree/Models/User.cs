using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace SmartTree.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Email { get; set; }
        public int MultiCoins { get; set; }
        public double Balance { get; set; }
        public double ReservedPartOfBalance { get; set; }
        public int? Rank { get; set; }
        public int untaxed { get; set; }
        public int? FatherId { get; set; }
        public int ReceivedCalls { get; set; }
        public int EmitedCalls { get; set; }

        /* Time stamp to handle concurency :
         * http://www.asp.net/mvc/tutorials/getting-started-with-ef-5-using-mvc-4/handling-concurrency-with-the-entity-framework-in-an-asp-net-mvc-application
         */
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual User Father { get; set; }
        public virtual List<User> Childs { get; set; }

        /// <summary>
        /// check the User is an Elligible Seller
        /// </summary>
        /// <returns></returns>
        public Boolean isElligibleSeller()
        {
            return this.Childs.Count() < Constants.MAX_CHILDS && this.MultiCoins > 0;
        }

        
        

 

       

        #region static
        /// <summary>
        /// Get the list of Elligible sellers to display in Market.
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static List<User> GetElligiblesSellers(SmartTreeEntities db)
        {
            return db.Users
                .Where(u => u.MultiCoins > 0 && u.Childs.Count < Constants.MAX_CHILDS && u.FatherId != null)
                .OrderBy(u => u.Rank)
                .ToList();
        }
        #endregion
    }
}