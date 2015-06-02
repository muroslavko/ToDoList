using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace test1.Models
{
    public class Subtask
    {
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Text { get; set; }
        public bool Complete { get; set; }
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}