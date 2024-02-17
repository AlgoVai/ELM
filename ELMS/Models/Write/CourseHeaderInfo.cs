using System;
using System.Collections.Generic;

namespace ELearningWeb.Models.Write
{
    public partial class CourseHeaderInfo
    {
        public long CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public string TeacherEmail { get; set; } = null!;
        public long? TeacherId { get; set; }
        public decimal CourseFee { get; set; }
        public decimal DiscountFee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
