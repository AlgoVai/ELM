using System;
using System.Collections.Generic;

namespace ELearningWeb.Models.Write
{
    public partial class Student
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long Age { get; set; }
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
