using MyPoli.Common;
using MyPoli.Entities;


namespace MyPoli.DataAccess
{
    public class UnitOfWork
    {
        private readonly MyPoliContext Context;

        public UnitOfWork(MyPoliContext context)
        {
            Context = context;
        }

        private IRepository<Gender> genders;
        public IRepository<Gender> Genders => genders ?? (genders = new BaseRepository<Gender>(Context));

        private IRepository<Grade> grades;
        public IRepository<Grade> Grades => grades ?? (grades = new BaseRepository<Grade>(Context));

        private IRepository<Group> groups;
        public IRepository<Group> Groups => groups ?? (groups = new BaseRepository<Group>(Context));

        private IRepository<Nationality> nationalities;
        public IRepository<Nationality> Nationalities => nationalities ?? (nationalities = new BaseRepository<Nationality>(Context));

        private IRepository<Person> persons;
        public IRepository<Person> People => persons ?? (persons = new BaseRepository<Person>(Context));

        private IRepository<Role> roles;
        public IRepository<Role> Roles => roles ?? (roles = new BaseRepository<Role>(Context));

        private IRepository<Secretary> secretaries;
        public IRepository<Secretary> Secretaries => secretaries ?? (secretaries = new BaseRepository<Secretary>(Context));

        private IRepository<SecretarySpecialization> secretarySpecializations;
        public IRepository<SecretarySpecialization> SecretarySpecializations => secretarySpecializations ?? (secretarySpecializations = new BaseRepository<SecretarySpecialization>(Context));

        private IRepository<Specialization> specializations;
        public IRepository<Specialization> Specializations => specializations ?? (specializations = new BaseRepository<Specialization>(Context));

        private IRepository<Status> statuses;
        public IRepository<Status> Statuses => statuses ?? (statuses = new BaseRepository<Status>(Context));

        private IRepository<Student> students;
        public IRepository<Student> Students => students ?? (students = new BaseRepository<Student>(Context));

        private IRepository<StudentSubject> studentSubjects;
        public IRepository<StudentSubject> StudentSubjects => studentSubjects ?? (studentSubjects = new BaseRepository<StudentSubject>(Context));

        private IRepository<Subject> subjects;
        public IRepository<Subject> Subjects => subjects ?? (subjects = new BaseRepository<Subject>(Context));

        private IRepository<SubjectTeacher> subjectTeachers;
        public IRepository<SubjectTeacher> SubjectTeachers => subjectTeachers ?? (subjectTeachers = new BaseRepository<SubjectTeacher>(Context));

        private IRepository<Teacher> teachers;
        public IRepository<Teacher> Teachers => teachers ?? (teachers = new BaseRepository<Teacher>(Context));

        private IRepository<TeacherGroup> teacherGroups;
        public IRepository<TeacherGroup> TeacherGroups => teacherGroups ?? (teacherGroups = new BaseRepository<TeacherGroup>(Context));

        private IRepository<User> users;
        public IRepository<User> Users => users ?? (users = new BaseRepository<User>(Context));

        private IRepository<PersonRole> personroles;
        public IRepository<PersonRole> Personroles => personroles ?? (personroles = new BaseRepository<PersonRole>(Context));

        private IRepository<Certificate> certificates;
        public IRepository<Certificate> Certificates => certificates ?? (certificates = new BaseRepository<Certificate>(Context));

        private IRepository<Thesis> theses;
        public IRepository<Thesis> Theses => theses ?? (theses = new BaseRepository<Thesis>(Context));

        private IRepository<Circumstance> circumstances;
        public IRepository<Circumstance> Circumstances => circumstances ?? (circumstances = new BaseRepository<Circumstance>(Context));

        private IRepository<Feedback> feedbacks;
        public IRepository<Feedback> Feedbacks => feedbacks ?? (feedbacks = new BaseRepository<Feedback>(Context));
        // pentru fiecare entitate? si pentru tabelele de legatura??????????????????????????????
        // doar ce e nevoie, doar ce folosesc

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
