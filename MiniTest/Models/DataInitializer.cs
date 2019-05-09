using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MiniTest.Models
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            User user = new User
            {
                Username = "root",
                Password = "1"
            };
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}