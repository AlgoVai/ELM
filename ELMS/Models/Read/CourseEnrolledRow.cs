using System;
using System.Collections.Generic;

namespace ELearningWeb.Models.Read
{
    public partial class CourseEnrolledRow
    {
        public long EnrolledId { get; set; }
        public long? UserId { get; set; }
        public string UserEmail { get; set; } = null!;
        public long CourseId { get; set; }
        public bool IsPayment { get; set; }
        public decimal CourseFee { get; set; }
        public DateTime EnrollDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMedia { get; set; } = null!;
    }
}
