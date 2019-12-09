using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Database.Entities
{
    public class Teacher
    {
        private long id;

        private String firstName;

        private String lastName;

        private int experience;

        private long mainSubjectId;

        private Subject mainSubject;

        public Teacher()
        {
        }

        public Teacher(long id, string firstName, string lastName, int experience, long mainSubjectId)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.experience = experience;
            this.mainSubjectId = mainSubjectId;
        }

        public Teacher(long id, string firstName, string lastName, int experience, long mainSubjectId, string subjectTitle)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.experience = experience;
            this.mainSubjectId = mainSubjectId;
            this.mainSubject = new Subject(mainSubjectId, subjectTitle);
        }

        public long Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int Experience { get => experience; set => experience = value; }
        public long MainSubjectId { get => mainSubjectId; set => mainSubjectId = value; }
        public Subject MainSubject { get => mainSubject; set => mainSubject = value; }
    }
}
