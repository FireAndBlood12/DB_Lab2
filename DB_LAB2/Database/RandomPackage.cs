using DB_LAB2.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Database
{
    public class RandomPackage
    {
        static char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();

        private DAO<Subject> subjectDAO;
        private DAO<Group> groupDAO;
        private DAO<Teacher> teacherDAO;
        private DAO<Student> studentDAO;
        private DAO<Mark> markDAO;
        public RandomPackage()
        {
            teacherDAO = new TeacherDAO(DBConnection.getInstance());
            subjectDAO = new SubjectDAO(DBConnection.getInstance());
            studentDAO = new StudentDAO(DBConnection.getInstance());
            markDAO = new MarkDAO(DBConnection.getInstance());
            groupDAO = new GroupDAO(DBConnection.getInstance());
        }

        public void GenerateEntenties(int count)
        {
            for (int i = 0; i < count; i++)
            {
                long sub_id = subjectDAO.Create(new Subject(2, randomString(7) + " " + randomString(7)));
                long gr_id = groupDAO.Create(new Group(2, randomString(4 + i % 3), getRandomPastDate()));
                long st_id = studentDAO.Create(new Student(2, randomString(6), randomString(8), getRandomPastDate(), gr_id));
                long tch_id = teacherDAO.Create(new Teacher(2, randomString(7), randomString(9), randomNumber(0, 60), sub_id));
                long mark_id = markDAO.Create(new Mark(2, randomNumber(0, 100), getRandomPastDate(), sub_id, st_id, tch_id));
            }
        }

        private static string randomString(int length)
        {
            Random rand = new Random();
            string word = "";
            for (int j = 1; j <= length; j++)
            {
                int letter_num = rand.Next(0, letters.Length - 1);
                word += letters[letter_num];
            }
            return word;
        }
        private static int randomNumber(int min, int max)
        {
            Random rand = new Random();
            return min + rand.Next(0, max - min);
        }

        private static DateTime getRandomFutureDate()
        {
            Random gen = new Random();
            int range = 2 * 365;
            DateTime randomDate = DateTime.Today.AddDays(gen.Next(range));
            return randomDate;
        }

        private static DateTime getRandomPastDate()
        {
            Random gen = new Random();
            int range = 1 * 365;
            DateTime randomDate = DateTime.Today.AddDays(-gen.Next(range));
            return randomDate;
        }
    }
}
