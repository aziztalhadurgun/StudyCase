using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MiniTest.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("dataConnection")
        {
            
        }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<Field> Fields { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }

}