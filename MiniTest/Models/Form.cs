using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniTest.Models
{
    public class Form
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedBy { get; set; }
        public int Field_Id { get; set; }
        public virtual Field Fields { get; set; }
        public virtual User Users { get; set; }
    }
}