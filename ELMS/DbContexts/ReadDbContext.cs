using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ELearningWeb.Models.Read;

namespace ELearningWeb.DbContexts
{
    public partial class ReadDbContext : DbContext
    {
        public ReadDbContext()
        {
        }

        public ReadDbContext(DbContextOptions<ReadDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CourseEnrolledRow> CourseEnrolledRows { get; set; } = null!;
        public virtual DbSet<CourseHeaderInfo> CourseHeaderInfos { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserOtp> UserOtps { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-73A64Q5;Initial Catalog=ELearn;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseEnrolledRow>(entity =>
            {
                entity.HasKey(e => e.EnrolledId);

                entity.ToTable("CourseEnrolledRow");

                entity.Property(e => e.CourseFee).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.EnrollDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentMedia).HasMaxLength(50);

                entity.Property(e => e.UserEmail).HasMaxLength(50);
            });

            modelBuilder.Entity<CourseHeaderInfo>(entity =>
            {
                entity.HasKey(e => e.CourseId);

                entity.ToTable("CourseHeaderInfo");

                entity.Property(e => e.CourseFee).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.CourseName).HasMaxLength(50);

                entity.Property(e => e.DiscountFee).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TeacherEmail).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.ConfirmPassword).HasMaxLength(250);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(250);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.UserRole).HasMaxLength(50);
            });

            modelBuilder.Entity<UserOtp>(entity =>
            {
                entity.HasKey(e => e.OtpId);

                entity.ToTable("UserOtp");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Otp).HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
