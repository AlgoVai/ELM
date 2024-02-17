namespace ELearningWeb.DTO
{
    public class SignUpDTO
    {
            public long UserId { get; set; }
            public string UserName { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
            public string ConfirmPassword { get; set; } = null!;
            public long Age { get; set; }
            public string Address { get; set; } = null!;
            public string? Phone { get; set; }
            public string UserRole { get; set; }
            public DateTime CreateDate { get; set; }

    }
}
