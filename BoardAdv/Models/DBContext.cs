using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BoardAdv.Models
{
    public class DBContext : DbContext
    {
        public DBContext() : base("DBContext") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Advert> Adverts { get; set; }
    }
}