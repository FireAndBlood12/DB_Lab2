using DB_LAB2.Database.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Database
{
    public class MarkDAO : DAO<Mark>
    {
        public MarkDAO(DBConnection db) : base(db)
        {
        }
        public override long Count()
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM public.marks";
            NpgsqlDataReader reader = command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count = Convert.ToInt32(reader.GetValue(0));
            }
            dbconnection.Close();
            return count;
        }

        private bool checkSubjectId(long id)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM public.subjects subj WHERE id = :Subject_id";
            command.Parameters.Add(new NpgsqlParameter("Subject_id", id));
            NpgsqlDataReader reader = command.ExecuteReader();
            Subject s = null;
            while (reader.Read())
            {
                s = new Subject(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString());
            }
            dbconnection.Close();
            return s != null;
        }
        private bool checkTeacherId(long id)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM public.teachers WHERE id = :teacher_id";
            command.Parameters.Add(new NpgsqlParameter("teacher_id", id));
            NpgsqlDataReader reader = command.ExecuteReader();
            Teacher tch = null;
            while (reader.Read())
            {
                tch = new Teacher(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                                  reader.GetValue(2).ToString(), Convert.ToInt32(reader.GetValue(3)),
                                  Convert.ToInt64(reader.GetValue(4)));
            }
            dbconnection.Close();
            return tch != null;
        }

        private bool checkStudentId(long id)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM public.students WHERE id = :student_id";
            command.Parameters.Add(new NpgsqlParameter("student_id", id));
            NpgsqlDataReader reader = command.ExecuteReader();
            Student st = null;
            while (reader.Read())
            {
                st = new Student(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                                  reader.GetValue(2).ToString(), Convert.ToDateTime(reader.GetValue(3)),
                                  Convert.ToInt64(reader.GetValue(4)));
            }
            dbconnection.Close();
            return st != null;
        }
        public override long Create(Mark entity)
        {
            if (!checkSubjectId(entity.SubjectId)) throw new Exception("There is no subject with this ID");
            if (!checkTeacherId(entity.TeacherId)) throw new Exception("There is no teacher with this ID");
            if (!checkStudentId(entity.StudentId)) throw new Exception("There is no student with this ID");
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO public.marks (mark, date, subject_id, student_id, teacher_id)" +
                " VALUES (:mark, :date, :subject_id, :student_id, :teacher_id) RETURNING  id";
            command.Parameters.Add(new NpgsqlParameter("mark", entity.Mark_));
            command.Parameters.Add(new NpgsqlParameter("date", entity.Date));
            command.Parameters.Add(new NpgsqlParameter("subject_id", entity.SubjectId));
            command.Parameters.Add(new NpgsqlParameter("student_id", entity.StudentId));
            command.Parameters.Add(new NpgsqlParameter("teacher_id", entity.TeacherId));
            long id = 0;
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = Convert.ToInt64(reader.GetValue(0));
                }
                dbconnection.Close();
                return id;
            }
            catch (PostgresException)
            {
                throw new Exception("Unable to create new Mark");
            }
        }

        public override void Delete(long id)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM public.marks WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            command.ExecuteNonQuery();
            dbconnection.Close();
        }

        public override Mark GetById(long id)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT mark.*,  sub.*, st.*, gr.*,  tch.* FROM public.marks as mark " +
                                    "join public.subjects as sub on sub.id = mark.subject_id " +
                                    "join public.teachers as tch on tch.id = mark.teacher_id " +
                                    "join public.students as st on st.id = mark.student_id " +
                                    "join public.groups as gr on gr.id = st.group_id " +
                                    "WHERE mark.id = :mark_id";
            command.Parameters.Add(new NpgsqlParameter("mark_id", id));
            NpgsqlDataReader reader = command.ExecuteReader();
            Mark mark = null;
            while (reader.Read())
            {
                mark = new Mark(Convert.ToInt64(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(1)), Convert.ToDateTime(reader.GetValue(2)),
                                  Convert.ToInt64(reader.GetValue(5)), Convert.ToInt64(reader.GetValue(3)), Convert.ToInt64(reader.GetValue(4)),
                                  reader.GetValue(7).ToString(), reader.GetValue(9).ToString(), reader.GetValue(10).ToString(),
                                  Convert.ToDateTime(reader.GetValue(11)),Convert.ToInt64(reader.GetValue(12)), reader.GetValue(14).ToString(),
                                  Convert.ToDateTime(reader.GetValue(15)), reader.GetValue(17).ToString(), reader.GetValue(18).ToString(),
                                  Convert.ToInt32(reader.GetValue(19)), Convert.ToInt64(reader.GetValue(20)));
            }
            dbconnection.Close();
            return mark;
        }

        public override List<Mark> GetList(int page)
        {
            List<Mark> mark_list = new List<Mark>();
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT mark.*,  sub.*, st.*, gr.*,  tch.* FROM public.marks as mark " +
                                    "join public.subjects as sub on sub.id = mark.subject_id " +
                                    "join public.teachers as tch on tch.id = mark.teacher_id " +
                                    "join public.students as st on st.id = mark.student_id " +
                                    "join public.groups as gr on gr.id = st.group_id " +
                                     "ORDER BY mark.id ASC LIMIT 10 OFFSET :offset";
            command.Parameters.Add(new NpgsqlParameter("offset", (page - 1) * 10));
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Mark mark = new Mark(Convert.ToInt64(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(1)), Convert.ToDateTime(reader.GetValue(2)),
                                  Convert.ToInt64(reader.GetValue(5)), Convert.ToInt64(reader.GetValue(3)), Convert.ToInt64(reader.GetValue(4)),
                                  reader.GetValue(7).ToString(), reader.GetValue(9).ToString(), reader.GetValue(10).ToString(),
                                  Convert.ToDateTime(reader.GetValue(11)), Convert.ToInt64(reader.GetValue(12)), reader.GetValue(14).ToString(),
                                  Convert.ToDateTime(reader.GetValue(15)), reader.GetValue(17).ToString(), reader.GetValue(18).ToString(),
                                  Convert.ToInt32(reader.GetValue(19)), Convert.ToInt64(reader.GetValue(20)));
                mark_list.Add(mark);
            }
            dbconnection.Close();
            return mark_list;
        }

        public override void Update(Mark entity)
        {
            if (!checkSubjectId(entity.SubjectId)) throw new Exception("There is no subject with this ID");
            if (!checkTeacherId(entity.TeacherId)) throw new Exception("There is no teacher with this ID");
            if (!checkStudentId(entity.StudentId)) throw new Exception("There is no student with this ID");
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE public.marks SET mark = :mark, date = :date, subject_id = :subject_id, " +
                "student_id = :student_id, teacher_id = :teacher_id WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("mark", entity.Mark_));
            command.Parameters.Add(new NpgsqlParameter("date", entity.Date));
            command.Parameters.Add(new NpgsqlParameter("subject_id", entity.SubjectId));
            command.Parameters.Add(new NpgsqlParameter("student_id", entity.StudentId));
            command.Parameters.Add(new NpgsqlParameter("teacher_id", entity.TeacherId));
            command.Parameters.Add(new NpgsqlParameter("id", entity.Id));
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
            }
            catch (PostgresException)
            {
                throw ;
            }
            dbconnection.Close();
        }
    }
}
