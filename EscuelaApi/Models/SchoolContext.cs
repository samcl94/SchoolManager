using EscuelaApi.Models;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Models;
using System;
using System.Collections.Generic;

namespace SchoolApi.Models;

public partial class SchoolContext : DbContext
{
    public SchoolContext()
    {
    }

    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GroupStudent> GroupStudents { get; set; }

    public virtual DbSet<InfoGroup> InfoGroups { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<SubjectStudent> SubjectStudents { get; set; }

    public virtual DbSet<GroupSubject> GroupSubjects { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupStudent>(entity =>
        {
            entity.HasKey(e => new { e.CodeGroup, e.StudentDni }).HasName("pk_group_student");

            entity.ToTable("group_student");

            entity.Property(e => e.CodeGroup)
                .HasMaxLength(50)
                .HasColumnName("code_group");
            entity.Property(e => e.StudentDni)
                .HasMaxLength(15)
                .HasColumnName("student_dni");

            entity.HasOne(d => d.CodeGroupNavigation).WithMany(p => p.GroupStudents)
                .HasForeignKey(d => d.CodeGroup)
                .HasConstraintName("fk_group");

            entity.HasOne(d => d.StudentDniNavigation).WithMany(p => p.GroupStudents)
                .HasForeignKey(d => d.StudentDni)
                .HasConstraintName("fk_student");
        });

        modelBuilder.Entity<InfoGroup>(entity =>
        {
            entity.HasKey(e => e.CodeGroup).HasName("info_group_pkey");

            entity.ToTable("info_group");

            entity.Property(e => e.CodeGroup)
                .HasMaxLength(50)
                .HasColumnName("code_group");
            entity.Property(e => e.Label)
                .HasMaxLength(100)
                .HasColumnName("label");
            entity.Property(e => e.Ordre)
                .HasDefaultValue(0)
                .HasColumnName("ordre");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Dni).HasName("student_pkey");

            entity.ToTable("student");

            entity.Property(e => e.Dni)
                .HasMaxLength(15)
                .HasColumnName("dni");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.ParentEmergencyPhone1)
                .HasMaxLength(20)
                .HasColumnName("parent_emergency_phone1");
            entity.Property(e => e.ParentEmergencyPhone2)
                .HasMaxLength(20)
                .HasColumnName("parent_emergency_phone2");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.CodeSubject).HasName("subject_pkey");

            entity.ToTable("subject");

            entity.Property(e => e.CodeSubject)
                .HasMaxLength(50)
                .HasColumnName("code_subject");
            entity.Property(e => e.Label)
                .HasMaxLength(100)
                .HasColumnName("label");
            entity.Property(e => e.Ordre)
                .HasDefaultValue(0)
                .HasColumnName("ordre");
        });

        modelBuilder.Entity<SubjectStudent>(entity =>
        {
            entity.HasKey(e => new { e.StudentDni, e.CodeSubject }).HasName("pk_subject_student");

            entity.ToTable("subject_student");

            entity.Property(e => e.StudentDni)
                .HasMaxLength(15)
                .HasColumnName("student_dni");
            entity.Property(e => e.CodeSubject)
                .HasMaxLength(50)
                .HasColumnName("code_subject");

            entity.HasOne(d => d.CodeSubjectNavigation).WithMany(p => p.SubjectStudents)
                .HasForeignKey(d => d.CodeSubject)
                .HasConstraintName("fk_subject");

            entity.HasOne(d => d.StudentDniNavigation).WithMany(p => p.SubjectStudents)
                .HasForeignKey(d => d.StudentDni)
                .HasConstraintName("fk_student");
        });

        modelBuilder.Entity<GroupSubject>(entity =>
        {
            entity.HasKey(e => new { e.CodeGroup, e.CodeSubject })
                .HasName("group_subject_pkey");

            entity.ToTable("group_subject", "public");

            entity.Property(e => e.CodeGroup)
                .HasColumnName("code_group")
                .HasMaxLength(50);

            entity.Property(e => e.CodeSubject)
                .HasColumnName("code_subject")
                .HasMaxLength(50);

            entity.HasOne(d => d.InfoGroup)
                .WithMany(p => p.GroupSubjects)
                .HasForeignKey(d => d.CodeGroup)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("group_subject_code_group_fkey");

            entity.HasOne(d => d.Subject)
                .WithMany(p => p.GroupSubjects)
                .HasForeignKey(d => d.CodeSubject)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("group_subject_code_subject_fkey");
        });

            OnModelCreatingPartial(modelBuilder);


    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
