using Microsoft.EntityFrameworkCore;
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

    public virtual DbSet<InfoGroup> InfoGroups { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<GroupStudent> GroupStudents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

            entity.HasMany(d => d.StudentDnis).WithMany(p => p.CodeGroups)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupStudent",
                    r => r.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentDni")
                        .HasConstraintName("fk_student"),
                    l => l.HasOne<InfoGroup>().WithMany()
                        .HasForeignKey("CodeGroup")
                        .HasConstraintName("fk_group"),
                    j =>
                    {
                        j.HasKey("CodeGroup", "StudentDni").HasName("pk_group_student");
                        j.ToTable("group_student");
                        j.IndexerProperty<string>("CodeGroup")
                            .HasMaxLength(50)
                            .HasColumnName("code_group");
                        j.IndexerProperty<string>("StudentDni")
                            .HasMaxLength(15)
                            .HasColumnName("student_dni");
                    });
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

        modelBuilder.Entity<GroupStudent>(entity =>
        {
            entity.HasKey(e => new { e.CodeGroup, e.StudentDni })
                .HasName("pk_group_student");

            entity.ToTable("group_student", "public");

            entity.Property(e => e.CodeGroup)
                .HasMaxLength(50)
                .HasColumnName("code_group");

            entity.Property(e => e.StudentDni)
                .HasMaxLength(15)
                .HasColumnName("student_dni");

            entity.HasOne(d => d.InfoGroup)
                .WithMany(p => p.GroupStudents)
                .HasForeignKey(d => d.CodeGroup)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_group");

            entity.HasOne(d => d.Student)
                .WithMany(p => p.GroupStudents)
                .HasForeignKey(d => d.StudentDni)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_student");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
