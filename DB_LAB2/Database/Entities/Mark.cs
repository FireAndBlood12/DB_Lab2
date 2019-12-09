using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Database.Entities
{
    public class Mark
    {
        private long id;
        private int mark;
        private DateTime date;
        private long subjectId;
        private long studentId;
        private long teacherId;

        private Subject subject;
        private Student student;
        private Teacher teacher;

        public Mark()
        {
        }

        public Mark(long id, int mark, DateTime date, long subjectId, long studentId, long teacherId)
        {
            this.id = id;
            this.mark = mark;
            this.date = date;
            this.subjectId = subjectId;
            this.studentId = studentId;
            this.teacherId = teacherId;
        }
        public Mark(long id, int mark, DateTime date, long subjectId, long studentId, long teacherId,
                        string sub_title,
                        string stud_firstName, string stud_lastName, DateTime stud_birthday,
                        long groupId, string group_code, DateTime group_entranceYear,
                        string teach_firstName, string teach_lastName, int teach_experience, 
                        long mainSubjectId)
        {
            this.id = id;
            this.mark = mark;
            this.date = date;
            this.subjectId = subjectId;
            this.studentId = studentId;
            this.teacherId = teacherId;
            this.subject = new Subject(subjectId, sub_title);
            this.student = new Student(studentId, stud_firstName, stud_lastName, stud_birthday,
                                        groupId, group_code, group_entranceYear);
            this.teacher = new Teacher(teacherId, teach_firstName, teach_lastName, teach_experience,
                                        mainSubjectId);
        }

        public long Id { get => id; set => id = value; }
        public int Mark_ { get => mark; set => mark = value; }
        public DateTime Date { get => date; set => date = value; }
        public long SubjectId { get => subjectId; set => subjectId = value; }
        public long StudentId { get => studentId; set => studentId = value; }
        public long TeacherId { get => teacherId; set => teacherId = value; }
        public Subject Subject { get => subject; set => subject = value; }
        public Student Student { get => student; set => student = value; }
        public Teacher Teacher { get => teacher; set => teacher = value; }
    }
}
