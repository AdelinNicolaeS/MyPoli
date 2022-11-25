using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyPoli.DataAccess.EntityFramework.Configurations;
using MyPoli.Entities;
using MyPoli.Entities.Enums;
using MyPoli.Common;
#nullable disable

namespace MyPoli.DataAccess
{
    public partial class MyPoliContext : DbContext
    {
        public MyPoliContext()
        {
        }

        public MyPoliContext(DbContextOptions<MyPoliContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonRole> PersonRoles { get; set; }
        public virtual DbSet<Secretary> Secretaries { get; set; }
        public virtual DbSet<SecretarySpecialization> SecretarySpecializations { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentSubject> StudentSubjects { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectTeacher> SubjectTeachers { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<TeacherGroup> TeacherGroups { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Thesis> Theses { get; set; }
        public virtual DbSet<Circumstance> Circumstances { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<BadWord> BadWords { get; set; }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Data Source=ASTANCA;Initial Catalog=MyPoli;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-T2SS4A8;Initial Catalog=MyPoli;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            //base.OnModelCreating(modelBuilder);
            // modelBuilder.ApplyConfiguration(new GenderConfiguration());
            // modelBuilder.ApplyConfiguration(new GradeConfiguration());
            // modelBuilder.ApplyConfiguration(new GroupConfiguration());
            // modelBuilder.ApplyConfiguration(new NationalityConfiguration());
            // modelBuilder.ApplyConfiguration(new PersonConfiguration());
            // modelBuilder.ApplyConfiguration(new RoleConfiguration());
            // modelBuilder.ApplyConfiguration(new SecretaryConfiguration());
            // modelBuilder.ApplyConfiguration(new SecretarySpecializationConfiguration());
            // modelBuilder.ApplyConfiguration(new SpecializationConfiguration());
            // modelBuilder.ApplyConfiguration(new StatusConfiguration());
            // modelBuilder.ApplyConfiguration(new StudentConfiguration());
            // modelBuilder.ApplyConfiguration(new StudentSubjectConfiguration());
            // modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            // modelBuilder.ApplyConfiguration(new SubjectTeacherConfiguration());
            // modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            // modelBuilder.ApplyConfiguration(new TeacherGroupConfiguration());
            //// modelBuilder.ApplyConfiguration(new UserConfiguration());
            // modelBuilder.ApplyConfiguration(new PersonRoleConfiguration());


            var romanId = Guid.Parse("E1183758-2844-4EED-8415-FF03841BB0AB");
            var moldoveanId = Guid.Parse("182B49E4-4513-4BB0-A2B2-260913151DAC");
            var mataseId = Guid.Parse("55322079-BF4D-409E-99D0-C448A3F5F8ED");
            var nationalities = new List<Guid>()
            {
                romanId, moldoveanId, mataseId
            };
            //var englezId = Guid.Parse("CAF06E9B-8431-418C-8768-FFE53F091E26");
            //var germanId = Guid.Parse("47D76411-8FB1-43CA-9CB1-39AC00119FFE");
            //var bulgarId = Guid.Parse("8B63A25D-86AA-4AE9-A08E-0778953DFFD4");
            //var spaniolId = Guid.Parse("")

            var maleId = Guid.Parse("3A2F97ED-9088-4E22-A407-66954895C372");
            var femaleId = Guid.Parse("9792182E-6EB6-4A5F-9455-859C061A36A6");
            var otherGenderId = Guid.Parse("3F69E8B8-E161-4E33-B94C-361E82F57A6B");
            var genders = new List<Guid>()
            {
                maleId, femaleId, otherGenderId
            };

            var inmatriculatId = Guid.Parse("5C71F99E-67A0-4AE0-A428-2AC74E9C5393");
            var exmatriculatId = Guid.Parse("C8BA7A83-B8D5-4638-82D9-3A83B90E00FA");
            var statuses = new List<Guid>()
            {
                inmatriculatId, exmatriculatId
            };


            var secretaryId = Guid.Parse("A2DF34DF-B17F-45CF-B233-FEAF61CD2DAC");


            //await PopulateStudentsAsync(modelBuilder, nationalities, genders, statuses);
           
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.ToTable("Feedback");
            
                entity.HasOne(g => g.StudentSubject)
                    .WithOne(ss => ss.Feedback)
                    .HasForeignKey<Feedback>(d => new { d.IdStudent, d.IdSubject })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_StudentSubject");
              
            });
            modelBuilder.Entity<Circumstance>(entity => {
                entity.ToTable("Circumstance");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.StudentId)
                    .IsRequired();
                entity.Property(e => e.Description)
                    .IsRequired();
                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .IsRequired();
                entity.Property(e => e.EndDate)
                 .HasColumnType("date")
                 .IsRequired();
                entity.Property(e => e.Accepted)
                    .IsRequired();
                entity.HasOne(e => e.Student)
                   .WithMany(s => s.Circumstances)
                   .HasForeignKey(e => e.StudentId)
                   .HasConstraintName("FK_StudentCircumstance");
            });
            modelBuilder.Entity<Thesis>(entity => {
                entity.ToTable("Thesis");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.StudentId).IsRequired();
                entity.Property(e => e.TeacherId).IsRequired();
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(e => e.Student)
                    .WithOne(s => s.Thesis)
                    .HasForeignKey<Thesis>(e => e.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentThesis");

                entity.HasOne(e => e.Teacher)
                    .WithMany(t => t.Theses)
                    .HasForeignKey(e => e.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherThesis");
            });

            modelBuilder.Entity<Certificate>(entity => {
                entity.ToTable("Certificate");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.StudentId)
                    .IsRequired();
                entity.Property(e => e.Reason)
                    .IsRequired();
                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .IsRequired();
                entity.HasOne(e => e.Student)
                   .WithMany(s => s.Certificates)
                   .HasForeignKey(e => e.StudentId)
                   .HasConstraintName("FK_StudentCertificate");
            });

            modelBuilder.Entity<PersonRole>()
                        .HasKey(pr => new { pr.PersonId, pr.RoleId });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => new { e.IdStudent, e.IdSubject })
                    .HasName("Pk_Grade");

                entity.ToTable("Grade");

                entity.HasOne(g => g.StudentSubject)
                    .WithOne(ss => ss.Grade)
                    .HasForeignKey<Grade>(g => new { g.IdStudent, g.IdSubject })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grade_StudentSubject");

                entity.HasOne(g => g.SubjectTeacher)
                    .WithMany(st => st.Grades)
                    .HasForeignKey(g => new { g.IdTeacher, g.IdSubject })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grade_SubjectTeacher");

                entity.HasOne(g => g.TeacherGroup)
                   .WithMany(tg => tg.Grades)
                   .HasForeignKey(g => new { g.IdTeacher, g.IdGroup })
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Grade_TeacherGroup");

                entity.Property(e => e.GradeValue).HasColumnName("Grade");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(g => g.Specialization)
                    .WithMany(s => s.Groups)
                    .HasForeignKey(g => g.SpecializationId)
                    .HasConstraintName("FK_SpecializationGroup");
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.ToTable("Nationality");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).IsRequired();

                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Gender_Person");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.NationalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Nationality_Person");
            });

            modelBuilder.Entity<Secretary>(entity =>
            {
                entity.ToTable("Secretary");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Salary).HasColumnType("decimal(15, 2)");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Secretary)
                    .HasForeignKey<Secretary>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SecretaryPerson");
            });

            modelBuilder.Entity<SecretarySpecialization>(entity =>
            {
                entity.HasKey(e => new { e.IdSecretary, e.IdSpecialization })
                    .HasName("Pk_SecretarySpecialization");

                entity.ToTable("SecretarySpecialization");

                entity.HasOne(d => d.IdSecretaryNavigation)
                    .WithMany(p => p.SecretarySpecializations)
                    .HasForeignKey(d => d.IdSecretary)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_SecretarySpecialization_Secretary");

                entity.HasOne(d => d.IdSpecializationNavigation)
                    .WithMany(p => p.SecretarySpecializations)
                    .HasForeignKey(d => d.IdSpecialization)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_SecretarySpecialization_Specialization");
            });

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.ToTable("Specialization");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_StudentGroup");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentPerson");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("Fk_Status_Student");
            });

            modelBuilder.Entity<StudentSubject>(entity =>
            {
                entity.HasKey(e => new { e.IdStudent, e.IdSubject })
                    .HasName("Pk_StudentSubject");

                entity.ToTable("StudentSubject");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentSubjects)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_StudentSubject_Student");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.StudentSubjects)
                    .HasForeignKey(d => d.IdSubject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_StudentSubject_Subject");

                //entity.HasOne(d => d.Grade)
                //    .WithOne(p => p.StudentSubject)
                //    .HasForeignKey<StudentSubject>(d => new { d.IdStudent, d.IdSubject })
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("Fk_StudentSubject_Grade");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SubjectTeacher>(entity =>
            {
                entity.HasKey(e => new { e.TeacherId, e.SubjectId })
                    .HasName("Pk_SubjectTeacher");

                entity.ToTable("SubjectTeacher");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectTeachers)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubjectTeacher_Subject");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.SubjectTeachers)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubjectTeacher_Teacher");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Salary).HasColumnType("decimal(15, 2)");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Teacher)
                    .HasForeignKey<Teacher>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherPerson");
            });

            modelBuilder.Entity<TeacherGroup>(entity =>
            {
                entity.HasKey(e => new { e.IdTeacher, e.IdGroup })
                    .HasName("Pk_TeacherGroup");

                entity.ToTable("TeacherGroup");

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.TeacherGroups)
                    .HasForeignKey(d => d.IdGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_TeacherGroup_Group");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.TeacherGroups)
                    .HasForeignKey(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_TeacherGroup_Teacher");
            });

            modelBuilder.Entity<BadWord>(entity =>
            {
                entity.ToTable("BadWord");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        //private async Task PopulateStudentsAsync(ModelBuilder modelBuilder, List<Guid> nationalities, List<Guid> genders, List<Guid> statuses)
        //{
        //    var firstNameMales = new List<string>()
        //    {
        //        "Christopher", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua",
        //        "Kenneth", "Kevin", "Bryan", "George", "Edward", "Ronald", "Timothy", "Jason", "Raul", "Ryan"
        //    };
        //    var secondNameMales = new List<string>()
        //    {
        //        "James", "Robert", "John", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles"
        //    };

        //    var firstNameFemales = new List<string>()
        //    {
        //       "Dorothy", "Carol", "Amanda", "Melissa", "Deborah", "Stephanie", "Rebecca", "Sharon", "Laura", "Cyntia",
        //       "Nancy", "Lisa", "Betty", "Margaret", "Sandra", "Ashley", "Kimberly", "Emily", "Donna", "Michelle"
        //    };
        //    var secondNameFemales = new List<string>()
        //    {
        //         "Mary", "Patricia", "Jennifer", "Linda", "Elizabeth", "Barbara", "Susan", "Jessica", "Sarah", "Karen"      
        //    };

        //    var familyNames = new List<string>()
        //    {
        //        "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
        //        "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin"
        //    };

        //    var addresses = new List<string>()
        //    {
        //        "01 Orin Junction", "8517 5th Drive", "839 Fieldstone Parkway", "4 Pond Drive", "69817 Tomscot Court", "0 Kingsford Trail",
        //        "23 Forest Dale Street", "0 Burrows Crossing", "59995 Schurz Road", "735 Vernon Alley", "069 Menomonie Point", "894 New Castle Way", "2196 Comanche Lane"
        //    };

        //   foreach(var first in firstNameMales)
        //    {
        //        foreach(var second in secondNameMales)
        //        {
        //            foreach(var name in familyNames)
        //            {
        //                var random = new Random();
        //                var id = Guid.NewGuid();
        //                modelBuilder.Entity<Person>().HasData(new Person()
        //                {
        //                    Id = id,
        //                    FirstName = first + "-" + second,
        //                    LastName = name,
        //                    Birthday = GenerateBirthdayStudent(),
        //                    PasswordHash = Utils.MyHashFunction("1234"),
        //                    Address = addresses[random.Next(addresses.Count)],
        //                    Email = GenerateEmail(first + "-" + second, name),
        //                    Phone = GeneratePhone(),
        //                    NationalityId = GenerateNationality(nationalities),
        //                    GenderId = genders[0],
        //                    RoleId = 3
        //                });
        //                var start = GenerateStartDate(); 
        //                modelBuilder.Entity<Student>().HasData(new Student()
        //                {
        //                    Id = id,
        //                    GroupId = (await Groups.ToListAsync())[random.Next((await Groups.ToListAsync()).Count)].Id,
        //                    StartDate = start,
        //                    EndDate = start.AddYears(4),
        //                    StatusId = GenerateStatus(statuses)
        //                }) ;
        //            }
        //        }
        //    }
        //    foreach (var first in firstNameFemales)
        //    {
        //        foreach (var second in secondNameFemales)
        //        {
        //            foreach (var name in familyNames)
        //            {
        //                var random = new Random();
        //                var id = Guid.NewGuid();
        //                modelBuilder.Entity<Person>().HasData(new Person()
        //                {
        //                    Id = id,
        //                    FirstName = first + "-" + second,
        //                    LastName = name,
        //                    Birthday = GenerateBirthdayStudent(),
        //                    PasswordHash = Utils.MyHashFunction("1234"),
        //                    Address = addresses[random.Next(addresses.Count)],
        //                    Email = GenerateEmail(first + "-" + second, name),
        //                    Phone = GeneratePhone(),
        //                    NationalityId = GenerateNationality(nationalities),
        //                    GenderId = genders[1],
        //                    RoleId = 3
        //                });
        //                var start = GenerateStartDate();
        //                modelBuilder.Entity<Student>().HasData(new Student()
        //                {
        //                    Id = id,
        //                    GroupId = (await Groups.ToListAsync())[random.Next((await Groups.ToListAsync()).Count)].Id,
        //                    StartDate = start,
        //                    EndDate = start.AddYears(4),
        //                    StatusId = GenerateStatus(statuses)
        //                });
        //            }
        //        }
        //    }

        //}

        private Guid? GenerateStatus(List<Guid> statuses)
        {
            Random random = new Random();
            if(random.Next(0, 10) <= 7)
            {
                return statuses[0];
            }
            return statuses[1];
        }

        private DateTime GenerateStartDate()
        {
            var random = new Random();
            var year = random.Next(2016, 2021);
            var month = random.Next(1, 12);
            var day = month == 2 ? random.Next(1, 28) : random.Next(1, 30);
            return new DateTime(year, month, day);
        }

        private Guid GenerateGender(List<Guid> genders)
        {
            var random = new Random();
            return genders[random.Next(0, 2)];
        }

        private Guid GenerateNationality(List<Guid> nationalities)
        {
            var random = new Random();
            return nationalities[random.Next(0, 2)];
        }

    

        private string GeneratePhone()
        {
            var random = new Random();
            return "07" + random.Next(1000, 9999).ToString() + random.Next(1000, 9999).ToString();
        }
        private string GenerateEmail(string forename, string lastname)
        {
            var random = new Random();
            return forename + "." + lastname + random.Next(10, 99).ToString() + "@gmail.com";
        }

        private bool GenerateisDeleted()
        {
            var random = new Random();
            return !(random.Next(0, 10) <= 7);
        }

        private DateTime GenerateBirthdayStudent()
        {
            var random = new Random();
            var year = random.Next(1994, 2002);
            var month = random.Next(1, 12);
            var day = month == 2 ? random.Next(1, 28) : random.Next(1, 30);
            return new DateTime(year, month, day);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
    }
}
