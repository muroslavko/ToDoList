using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace test1.Models
{
    public class Task
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("Name")]
        public string Text { get; set; }
        public bool Complete { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Subtask> Subtasks { get; set; }
        public Task()
        {
            Subtasks = new List<Subtask>();
        }
    }
}