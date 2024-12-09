using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BYSProje.Models.Entity;

    public partial class BYSContext : DbContext
    {
        public BYSContext() // parametresiz kurucu fonksiyon
        {

        }
         public BYSContext(DbContextOptions<BYSContext>options):base(options)
         {
            //ef core ile çalışan bir dbcontextin çalışmasını sağlar ve ayarları optionstan alır.
         }

         public virtual DbSet<Courses> Course {get;set;}
         public virtual DbSet<Instructors> Instructor {get;set;}
         public virtual DbSet<Login> Login {get;set;}
         public virtual DbSet<Students> Student {get;set;}
         public virtual DbSet<Student_Courses> StudentCourse {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       
      => optionsBuilder.UseSqlServer("Server=KAAn;Database=BYS;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Courses>(entity =>
           {
            entity.HasKey(e =>e.CourseID).HasName("PK_Courses");
            
            entity.ToTable("Course");
            
            entity.Property(e => e.CourseID).HasColumnName("CourseID");
            entity.Property(e => e.CourseName).HasMaxLength(100);
            entity.Property(e =>e.Credits).HasColumnType("float");
            entity.Property(e => e.InstructorID).HasColumnName("InstructorID");
            
            entity.HasOne(d => d.Instructor)
            .WithMany(p => p.Courses)
            .HasForeignKey(d => d.InstructorID)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Course_Instructor");
           });

           modelBuilder.Entity<Instructors>(entity =>
           {
               entity.HasKey(e =>e.InstructorID).HasName("PK_Instructors");

               entity.ToTable("Instructor");
               
               entity.Property(e => e.InstructorID).HasColumnName("InstructerID");
               entity.Property(e => e.FirstName).HasMaxLength(50);
               entity.Property(e => e.LastName).HasMaxLength(50);
               entity.Property(e => e.Email).HasMaxLength(100);
               entity.Property(e => e.Department).HasMaxLength(50);
               entity.Property(e => e.Password).HasMaxLength(50);
           });

           modelBuilder.Entity<Students>( entity =>
           {
              entity.HasKey(e => e.StudentID).HasName("PK_Students");

              entity.ToTable("Student");

              entity.Property(e => e.StudentID).HasColumnName("StudentID");
              entity.Property(e => e.First_Name).HasMaxLength(50);
              entity.Property(e => e.Last_Name).HasMaxLength(50);
              entity.Property(e => e.Email).HasMaxLength(100);
              entity.Property(e => e.Major).HasMaxLength(50);
              entity.Property(e => e.Password).HasMaxLength(50);
           });
           
           modelBuilder.Entity<Login>(entity => 
           {
              entity.HasKey(e => e.UserNo).HasName("PK_Login");
              
              entity.ToTable("Login");
              entity.Property(e => e.UserNo).HasColumnName("UserNo");
              entity.Property(e => e.Password).HasMaxLength(50);
           });

           modelBuilder.Entity<Student_Courses>(entity =>
           {
              entity.HasKey(e => new {e.CourseID,e.StudentID}).HasName("PK_Student_Course");
              entity.ToTable("StudentCourse");
              entity.Property(e => e.StudentID).HasColumnName("StudentID");
              entity.Property(e => e.CourseID).HasColumnName("CourseID");
              

               entity.HasOne(d => d.Student)
                .WithMany(p => p.StudentCourse)
                .HasForeignKey(d => d.StudentID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentCourse_Student");

            entity.HasOne(d => d.Course)
                .WithMany(p => p.StudentCourse)
                .HasForeignKey(d => d.CourseID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentCourse_Course");


           });

                 OnModelCreatingPartial(modelBuilder);
        }
             partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
