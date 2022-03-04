﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyPoli.DataAccess;

namespace MyPoli.DataAccess.Migrations
{
    [DbContext(typeof(MyPoliContext))]
    partial class FifthTry2ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyPoli.Entities.Certificate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Certificate");
                });

            modelBuilder.Entity("MyPoli.Entities.Circumstance", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Accepted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Circumstance");
                });

            modelBuilder.Entity("MyPoli.Entities.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdStudent")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdSubject")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LectureGrade")
                        .HasColumnType("int");

                    b.Property<string>("LectureOpinion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SeminarGrade")
                        .HasColumnType("int");

                    b.Property<string>("SeminarOpinion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdStudent", "IdSubject")
                        .IsUnique();

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("MyPoli.Entities.Gender", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Gender");
                });

            modelBuilder.Entity("MyPoli.Entities.Grade", b =>
                {
                    b.Property<Guid>("IdStudent")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdSubject")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GradeValue")
                        .HasColumnType("int")
                        .HasColumnName("Grade");

                    b.Property<Guid>("IdGroup")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdTeacher")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("IdStudent", "IdSubject")
                        .HasName("Pk_Grade");

                    b.HasIndex("IdTeacher", "IdGroup");

                    b.HasIndex("IdTeacher", "IdSubject");

                    b.ToTable("Grade");
                });

            modelBuilder.Entity("MyPoli.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)");

                    b.Property<Guid?>("SpecializationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SpecializationId");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("MyPoli.Entities.Nationality", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Nationality");
                });

            modelBuilder.Entity("MyPoli.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NationalityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("GenderId");

                    b.HasIndex("NationalityId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("MyPoli.Entities.PersonRole", b =>
                {
                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("PersonId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("PersonRoles");
                });

            modelBuilder.Entity("MyPoli.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("MyPoli.Entities.Secretary", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Experience")
                        .HasColumnType("int");

                    b.Property<decimal?>("Salary")
                        .HasColumnType("decimal(15,2)");

                    b.HasKey("Id");

                    b.ToTable("Secretary");
                });

            modelBuilder.Entity("MyPoli.Entities.SecretarySpecialization", b =>
                {
                    b.Property<Guid>("IdSecretary")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdSpecialization")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdSecretary", "IdSpecialization")
                        .HasName("Pk_SecretarySpecialization");

                    b.HasIndex("IdSpecialization");

                    b.ToTable("SecretarySpecialization");
                });

            modelBuilder.Entity("MyPoli.Entities.Specialization", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specialization");
                });

            modelBuilder.Entity("MyPoli.Entities.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("MyPoli.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<Guid?>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("StatusId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("MyPoli.Entities.StudentSubject", b =>
                {
                    b.Property<Guid>("IdStudent")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdSubject")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdStudent", "IdSubject")
                        .HasName("Pk_StudentSubject");

                    b.HasIndex("IdSubject");

                    b.ToTable("StudentSubject");
                });

            modelBuilder.Entity("MyPoli.Entities.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("MyPoli.Entities.SubjectTeacher", b =>
                {
                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TeacherId", "SubjectId")
                        .HasName("Pk_SubjectTeacher");

                    b.HasIndex("SubjectId");

                    b.ToTable("SubjectTeacher");
                });

            modelBuilder.Entity("MyPoli.Entities.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Experience")
                        .HasColumnType("int");

                    b.Property<decimal?>("Salary")
                        .HasColumnType("decimal(15,2)");

                    b.HasKey("Id");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("MyPoli.Entities.TeacherGroup", b =>
                {
                    b.Property<Guid>("IdTeacher")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdGroup")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdTeacher", "IdGroup")
                        .HasName("Pk_TeacherGroup");

                    b.HasIndex("IdGroup");

                    b.ToTable("TeacherGroup");
                });

            modelBuilder.Entity("MyPoli.Entities.Thesis", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ApprovedByTeacher")
                        .HasColumnType("bit");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.HasIndex("TeacherId");

                    b.ToTable("Thesis");
                });

            modelBuilder.Entity("MyPoli.Entities.Certificate", b =>
                {
                    b.HasOne("MyPoli.Entities.Student", "Student")
                        .WithMany("Certificates")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_StudentCertificate")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("MyPoli.Entities.Circumstance", b =>
                {
                    b.HasOne("MyPoli.Entities.Student", "Student")
                        .WithMany("Circumstances")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_StudentCircumstance")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("MyPoli.Entities.Feedback", b =>
                {
                    b.HasOne("MyPoli.Entities.StudentSubject", "StudentSubject")
                        .WithOne("Feedback")
                        .HasForeignKey("MyPoli.Entities.Feedback", "IdStudent", "IdSubject")
                        .HasConstraintName("FK_Feedback_StudentSubject")
                        .IsRequired();

                    b.Navigation("StudentSubject");
                });

            modelBuilder.Entity("MyPoli.Entities.Grade", b =>
                {
                    b.HasOne("MyPoli.Entities.StudentSubject", "StudentSubject")
                        .WithOne("Grade")
                        .HasForeignKey("MyPoli.Entities.Grade", "IdStudent", "IdSubject")
                        .HasConstraintName("FK_Grade_StudentSubject")
                        .IsRequired();

                    b.HasOne("MyPoli.Entities.TeacherGroup", "TeacherGroup")
                        .WithMany("Grades")
                        .HasForeignKey("IdTeacher", "IdGroup")
                        .HasConstraintName("FK_Grade_TeacherGroup")
                        .IsRequired();

                    b.HasOne("MyPoli.Entities.SubjectTeacher", "SubjectTeacher")
                        .WithMany("Grades")
                        .HasForeignKey("IdTeacher", "IdSubject")
                        .HasConstraintName("FK_Grade_SubjectTeacher")
                        .IsRequired();

                    b.Navigation("StudentSubject");

                    b.Navigation("SubjectTeacher");

                    b.Navigation("TeacherGroup");
                });

            modelBuilder.Entity("MyPoli.Entities.Group", b =>
                {
                    b.HasOne("MyPoli.Entities.Specialization", "Specialization")
                        .WithMany("Groups")
                        .HasForeignKey("SpecializationId")
                        .HasConstraintName("FK_SpecializationGroup");

                    b.Navigation("Specialization");
                });

            modelBuilder.Entity("MyPoli.Entities.Person", b =>
                {
                    b.HasOne("MyPoli.Entities.Gender", "Gender")
                        .WithMany("People")
                        .HasForeignKey("GenderId")
                        .HasConstraintName("Fk_Gender_Person")
                        .IsRequired();

                    b.HasOne("MyPoli.Entities.Nationality", "Nationality")
                        .WithMany("People")
                        .HasForeignKey("NationalityId")
                        .HasConstraintName("Fk_Nationality_Person")
                        .IsRequired();

                    b.Navigation("Gender");

                    b.Navigation("Nationality");
                });

            modelBuilder.Entity("MyPoli.Entities.PersonRole", b =>
                {
                    b.HasOne("MyPoli.Entities.Person", "Person")
                        .WithMany("PersonRoles")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPoli.Entities.Role", "Role")
                        .WithMany("PersonRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MyPoli.Entities.Secretary", b =>
                {
                    b.HasOne("MyPoli.Entities.Person", "Person")
                        .WithOne("Secretary")
                        .HasForeignKey("MyPoli.Entities.Secretary", "Id")
                        .HasConstraintName("FK_SecretaryPerson")
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("MyPoli.Entities.SecretarySpecialization", b =>
                {
                    b.HasOne("MyPoli.Entities.Secretary", "IdSecretaryNavigation")
                        .WithMany("SecretarySpecializations")
                        .HasForeignKey("IdSecretary")
                        .HasConstraintName("Fk_SecretarySpecialization_Secretary")
                        .IsRequired();

                    b.HasOne("MyPoli.Entities.Specialization", "IdSpecializationNavigation")
                        .WithMany("SecretarySpecializations")
                        .HasForeignKey("IdSpecialization")
                        .HasConstraintName("Fk_SecretarySpecialization_Specialization")
                        .IsRequired();

                    b.Navigation("IdSecretaryNavigation");

                    b.Navigation("IdSpecializationNavigation");
                });

            modelBuilder.Entity("MyPoli.Entities.Student", b =>
                {
                    b.HasOne("MyPoli.Entities.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK_StudentGroup");

                    b.HasOne("MyPoli.Entities.Person", "Person")
                        .WithOne("Student")
                        .HasForeignKey("MyPoli.Entities.Student", "Id")
                        .HasConstraintName("FK_StudentPerson")
                        .IsRequired();

                    b.HasOne("MyPoli.Entities.Status", "Status")
                        .WithMany("Students")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("Fk_Status_Student");

                    b.Navigation("Group");

                    b.Navigation("Person");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("MyPoli.Entities.StudentSubject", b =>
                {
                    b.HasOne("MyPoli.Entities.Student", "Student")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("IdStudent")
                        .HasConstraintName("Fk_StudentSubject_Student")
                        .IsRequired();

                    b.HasOne("MyPoli.Entities.Subject", "Subject")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("IdSubject")
                        .HasConstraintName("Fk_StudentSubject_Subject")
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("MyPoli.Entities.SubjectTeacher", b =>
                {
                    b.HasOne("MyPoli.Entities.Subject", "Subject")
                        .WithMany("SubjectTeachers")
                        .HasForeignKey("SubjectId")
                        .HasConstraintName("FK_SubjectTeacher_Subject")
                        .IsRequired();

                    b.HasOne("MyPoli.Entities.Teacher", "Teacher")
                        .WithMany("SubjectTeachers")
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("FK_SubjectTeacher_Teacher")
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("MyPoli.Entities.Teacher", b =>
                {
                    b.HasOne("MyPoli.Entities.Person", "Person")
                        .WithOne("Teacher")
                        .HasForeignKey("MyPoli.Entities.Teacher", "Id")
                        .HasConstraintName("FK_TeacherPerson")
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("MyPoli.Entities.TeacherGroup", b =>
                {
                    b.HasOne("MyPoli.Entities.Group", "IdGroupNavigation")
                        .WithMany("TeacherGroups")
                        .HasForeignKey("IdGroup")
                        .HasConstraintName("Fk_TeacherGroup_Group")
                        .IsRequired();

                    b.HasOne("MyPoli.Entities.Teacher", "IdTeacherNavigation")
                        .WithMany("TeacherGroups")
                        .HasForeignKey("IdTeacher")
                        .HasConstraintName("Fk_TeacherGroup_Teacher")
                        .IsRequired();

                    b.Navigation("IdGroupNavigation");

                    b.Navigation("IdTeacherNavigation");
                });

            modelBuilder.Entity("MyPoli.Entities.Thesis", b =>
                {
                    b.HasOne("MyPoli.Entities.Student", "Student")
                        .WithOne("Thesis")
                        .HasForeignKey("MyPoli.Entities.Thesis", "StudentId")
                        .HasConstraintName("FK_StudentThesis")
                        .IsRequired();

                    b.HasOne("MyPoli.Entities.Teacher", "Teacher")
                        .WithMany("Theses")
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("FK_TeacherThesis")
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("MyPoli.Entities.Gender", b =>
                {
                    b.Navigation("People");
                });

            modelBuilder.Entity("MyPoli.Entities.Group", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("TeacherGroups");
                });

            modelBuilder.Entity("MyPoli.Entities.Nationality", b =>
                {
                    b.Navigation("People");
                });

            modelBuilder.Entity("MyPoli.Entities.Person", b =>
                {
                    b.Navigation("PersonRoles");

                    b.Navigation("Secretary");

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("MyPoli.Entities.Role", b =>
                {
                    b.Navigation("PersonRoles");
                });

            modelBuilder.Entity("MyPoli.Entities.Secretary", b =>
                {
                    b.Navigation("SecretarySpecializations");
                });

            modelBuilder.Entity("MyPoli.Entities.Specialization", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("SecretarySpecializations");
                });

            modelBuilder.Entity("MyPoli.Entities.Status", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("MyPoli.Entities.Student", b =>
                {
                    b.Navigation("Certificates");

                    b.Navigation("Circumstances");

                    b.Navigation("StudentSubjects");

                    b.Navigation("Thesis");
                });

            modelBuilder.Entity("MyPoli.Entities.StudentSubject", b =>
                {
                    b.Navigation("Feedback");

                    b.Navigation("Grade");
                });

            modelBuilder.Entity("MyPoli.Entities.Subject", b =>
                {
                    b.Navigation("StudentSubjects");

                    b.Navigation("SubjectTeachers");
                });

            modelBuilder.Entity("MyPoli.Entities.SubjectTeacher", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("MyPoli.Entities.Teacher", b =>
                {
                    b.Navigation("SubjectTeachers");

                    b.Navigation("TeacherGroups");

                    b.Navigation("Theses");
                });

            modelBuilder.Entity("MyPoli.Entities.TeacherGroup", b =>
                {
                    b.Navigation("Grades");
                });
#pragma warning restore 612, 618
        }
    }
}
