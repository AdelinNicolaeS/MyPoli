﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyPoli.DataAccess;

namespace MyPoli.DataAccess.Migrations
{
    [DbContext(typeof(MyPoliContext))]
    [Migration("20210812103708_add default subjects")]
    partial class adddefaultsubjects
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyPoli.Entities.Gender", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Gender");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3a2f97ed-9088-4e22-a407-66954895c372"),
                            Name = "male"
                        },
                        new
                        {
                            Id = new Guid("9792182e-6eb6-4a5f-9455-859c061a36a6"),
                            Name = "female"
                        },
                        new
                        {
                            Id = new Guid("3f69e8b8-e161-4e33-b94c-361e82f57a6b"),
                            Name = "other"
                        });
                });

            modelBuilder.Entity("MyPoli.Entities.Grade", b =>
                {
                    b.Property<Guid>("IdStudent")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdSubject")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Grade1")
                        .HasColumnType("int")
                        .HasColumnName("Grade");

                    b.HasKey("IdStudent", "IdSubject")
                        .HasName("Pk_Grade");

                    b.ToTable("Grade");
                });

            modelBuilder.Entity("MyPoli.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)");

                    b.Property<Guid?>("SpecializationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Group");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5b144eaf-fef8-4e09-b7ff-b898efb233ba"),
                            Name = "321CA"
                        },
                        new
                        {
                            Id = new Guid("08342fcd-8c23-46ef-a3f1-3113fe3d8980"),
                            Name = "321CB"
                        },
                        new
                        {
                            Id = new Guid("bc159116-cc98-457e-b1ea-9e74fb81824b"),
                            Name = "321CC"
                        },
                        new
                        {
                            Id = new Guid("7e82d29b-2144-4cc9-9ef4-bb37e6e41aba"),
                            Name = "331CA"
                        },
                        new
                        {
                            Id = new Guid("a6750a62-531d-48c6-ac74-ad7a981f9a45"),
                            Name = "321CD"
                        },
                        new
                        {
                            Id = new Guid("56a6c5ba-8721-4c12-b323-fe5a210e7720"),
                            Name = "332CA"
                        });
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

                    b.HasData(
                        new
                        {
                            Id = new Guid("e1183758-2844-4eed-8415-ff03841bb0ab"),
                            Name = "roman"
                        },
                        new
                        {
                            Id = new Guid("182b49e4-4513-4bb0-a2b2-260913151dac"),
                            Name = "moldovean"
                        },
                        new
                        {
                            Id = new Guid("55322079-bf4d-409e-99d0-c448a3f5f8ed"),
                            Name = "tigan de matase"
                        });
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GenderId")
                        .HasColumnType("uniqueidentifier");

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

                    b.HasIndex("GenderId");

                    b.HasIndex("NationalityId");

                    b.ToTable("Person");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a2df34df-b17f-45cf-b233-feaf61cd2dac"),
                            Address = "Bdul Ala",
                            Birthday = new DateTime(1975, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "cristina@gribinic.ro",
                            FirstName = "Cristina",
                            GenderId = new Guid("9792182e-6eb6-4a5f-9455-859c061a36a6"),
                            LastName = "Gribinic",
                            NationalityId = new Guid("e1183758-2844-4eed-8415-ff03841bb0ab"),
                            PasswordHash = "03AC674216F3E15C761EE1A5E255F067953623C8B388B4459E13F978D7C846F4",
                            Phone = "0712345678",
                            RoleId = 1
                        });
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

                    b.HasData(
                        new
                        {
                            PersonId = new Guid("a2df34df-b17f-45cf-b233-feaf61cd2dac"),
                            RoleId = 1
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Secretary"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Teacher"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Student"
                        });
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

                    b.HasData(
                        new
                        {
                            Id = new Guid("f8de3c36-3c8f-4b4c-b97a-0b037b96d11e"),
                            Name = "inmatriculat"
                        },
                        new
                        {
                            Id = new Guid("e1b02dd5-0fcc-47e3-9221-614d46266c45"),
                            Name = "exmatriculat"
                        });
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Subject");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8982d7c8-a56a-4872-a3b7-bdda5a893aae"),
                            Name = "Matematica 1"
                        },
                        new
                        {
                            Id = new Guid("865b7084-f6a3-4aa3-8dfd-6d78239b93a9"),
                            Name = "Matematica 2"
                        },
                        new
                        {
                            Id = new Guid("03d7d3a1-d882-4c8a-91d0-be82e27ba3de"),
                            Name = "Programarea Calculatoarelor"
                        },
                        new
                        {
                            Id = new Guid("4063cb8a-14ab-4f28-aa6b-cddc1a8918a5"),
                            Name = "Metode Numerice"
                        },
                        new
                        {
                            Id = new Guid("019234af-b18b-4e0b-afee-60d865a66e87"),
                            Name = "Paradigme de Programare"
                        },
                        new
                        {
                            Id = new Guid("8b2214fd-63ef-4af4-a085-754f83904b5b"),
                            Name = "Utilizarea sistemelor de operare"
                        },
                        new
                        {
                            Id = new Guid("515bf6d3-c4d0-44a1-87f7-b41ae288165a"),
                            Name = "Analiza Algoritmilor"
                        });
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
                    b.HasOne("MyPoli.Entities.Student", "IdStudentNavigation")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("IdStudent")
                        .HasConstraintName("Fk_StudentSubject_Student")
                        .IsRequired();

                    b.HasOne("MyPoli.Entities.Subject", "IdSubjectNavigation")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("IdSubject")
                        .HasConstraintName("Fk_StudentSubject_Subject")
                        .IsRequired();

                    b.HasOne("MyPoli.Entities.Grade", "IdS")
                        .WithOne("StudentSubject")
                        .HasForeignKey("MyPoli.Entities.StudentSubject", "IdStudent", "IdSubject")
                        .HasConstraintName("Fk_StudentSubject_Grade")
                        .IsRequired();

                    b.Navigation("IdS");

                    b.Navigation("IdStudentNavigation");

                    b.Navigation("IdSubjectNavigation");
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

            modelBuilder.Entity("MyPoli.Entities.Gender", b =>
                {
                    b.Navigation("People");
                });

            modelBuilder.Entity("MyPoli.Entities.Grade", b =>
                {
                    b.Navigation("StudentSubject");
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
                    b.Navigation("SecretarySpecializations");
                });

            modelBuilder.Entity("MyPoli.Entities.Status", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("MyPoli.Entities.Student", b =>
                {
                    b.Navigation("StudentSubjects");
                });

            modelBuilder.Entity("MyPoli.Entities.Subject", b =>
                {
                    b.Navigation("StudentSubjects");

                    b.Navigation("SubjectTeachers");
                });

            modelBuilder.Entity("MyPoli.Entities.Teacher", b =>
                {
                    b.Navigation("SubjectTeachers");

                    b.Navigation("TeacherGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
