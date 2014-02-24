using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartTree.Models
{
    public class SmartTreeEntities : DbContext
    {
        public SmartTreeEntities()
            : base("SmartTreeEntities")
        {
        }

        public DbSet<User> Users { get; set; }

        // Fluent API
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOptional(u => u.Father)
                .WithMany(u => u.Childs)
                .HasForeignKey(u => u.FatherId)
                .WillCascadeOnDelete(false);

           /* Time stamp to handle concurency :
            * http://www.asp.net/mvc/tutorials/getting-started-with-ef-5-using-mvc-4/handling-concurrency-with-the-entity-framework-in-an-asp-net-mvc-application
            */
            modelBuilder.Entity<User>()
                .Property(p => p.RowVersion)
                .IsConcurrencyToken();
        }
    }
}