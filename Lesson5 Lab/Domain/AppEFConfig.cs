using Lesson5_Lab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson5_Lab.Domain
{
    public class AppEFConfig :DbContext
    {
        #region  DBSets Catalog
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        #endregion
    
        #region  DBSets Identity
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql("Server=localhost;" +
                "Port=5432;" +
                "Database=dbshop12;" +
                "Username=postgres;" +
                "Password=Qwerty1-");

            //optionsBuilder.UseSqlServer("Server=.;" +
            //   "Database=dbshop;" +
            //   "Trusted_Connection=True");
        }
    }
}
