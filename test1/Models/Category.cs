using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace test1.Models
{
    public class Category 
    {
        public int Id { get; set; }
        [DisplayName("Category")]
        public string Text { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public Category()
        {
            Tasks = new List<Task>();
        }
    }

}