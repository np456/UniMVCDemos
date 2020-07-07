using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using CodeFirstA2.Models;

namespace CodeFirstA2.DataAccessLayer
{
    public class UniversityContext : DbContext
    {
        public UniversityContext() : base("UniversityContext")
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder); Just adds option to stop pluralising of tables when creating
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}