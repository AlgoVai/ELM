using System;
using System.Collections.Generic;

namespace ELearningWeb.Models.Read
{
    public partial class UserOtp
    {
        public long OtpId { get; set; }
        public long UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Otp { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
