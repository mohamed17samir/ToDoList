using System;
using System.Collections.Generic;

namespace Done.Models
{
    public partial class Todo
    {
        public int Id { get; set; }
        public string TaskName { get; set; } = null!;
        public string Ttask { get; set; } = null!;
    }
}
