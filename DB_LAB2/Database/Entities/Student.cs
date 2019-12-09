using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Database.Entities
{
    public class Student
    {
        private long id;

        private String firstName;


        private String lastName;


        private DateTime birthday;
        private long groupId;

        private Group group;

        public Student()
        {
        }

        public Student(long id, string firstName, string lastName, DateTime birthday, long groupId)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthday = birthday;
            this.groupId = groupId;
        }

        public Student(long id, string firstName, string lastName, DateTime birthday,
                        long groupId, string group_code, DateTime group_entranceYear)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthday = birthday;
            this.groupId = groupId;
            this.group = new Group(groupId, group_code, group_entranceYear);
        }

        public long Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public long GroupId { get => groupId; set => groupId = value; }
        public Group Group_ { get => group; set => group = value; }
    }
}
