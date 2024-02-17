using System;
using System.Collections.Generic;

namespace ELearningWeb.Models.Read
{
    public partial class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public long? Age { get; set; }
        public string Address { get; set; } = null!;
        public string? Phone { get; set; }
        public string UserRole { get; set; } = null!;
        public DateTime CreateDate { get; set; }
    }
}
