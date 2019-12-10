using DB_LAB2.Database.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Database
{
    public class TeacherDAO : DAO<Teacher>
    {
        public TeacherDAO(DBConnection db) : base(db)
        {
        }

        public override long Count()
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM public.teachers";
            NpgsqlDataReader reader = command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count = Convert.ToInt32(reader.GetValue(0));
            }
            dbconnection.Close();
            return count;
        }
        private bool checkMainSubjectId(long id)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM public.subjects subj WHERE subj.id = :Subject_id";
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
        public override long Create(Teacher entity)
        {
            if (!checkMainSubjectId(entity.MainSubjectId)) throw new Exception("There is no subject with this ID");
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO public.teachers (firstname, lastname, experience, main_subject_id)" +
                " VALUES (:firstname, :lastname, :experience, :main_subject_id) RETURNING id";
            command.Parameters.Add(new NpgsqlParameter("firstname", entity.FirstName));
            command.Parameters.Add(new NpgsqlParameter("lastname", entity.LastName));
            command.Parameters.Add(new NpgsqlParameter("experience", entity.Experience));
            command.Parameters.Add(new NpgsqlParameter("main_subject_id", entity.MainSubjectId));
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
                throw new Exception("Unable to create new Teacher");
            }
        }

        public override void Delete(long id)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM public.teachers WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            command.ExecuteNonQuery();
            dbconnection.Close();
        }

        public override Teacher GetById(long id)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT tch.*, sub.* FROM public.subjects as sub " +
                                    "join public.teachers as tch on tch.main_subject_id = sub.id and tch.id = :teacher_id";
            command.Parameters.Add(new NpgsqlParameter("teacher_id", id));
            NpgsqlDataReader reader = command.ExecuteReader();
            Teacher tch = null;
            while (reader.Read())
            {
                tch = new Teacher(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                                  reader.GetValue(2).ToString(), Convert.ToInt32(reader.GetValue(3)),
                                  Convert.ToInt64(reader.GetValue(4)), reader.GetValue(6).ToString());
            }
            dbconnection.Close();
            return tch;
        }

        public override List<Teacher> GetList(int page)
        {
            List<Teacher> tch_list = new List<Teacher>();
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT tch.*, sub.* FROM public.subjects as sub "
                                    + "join public.teachers as tch on tch.main_subject_id = sub.id ORDER BY tch.id ASC LIMIT :limit OFFSET :offset";
            if (page != 0)
            {
                command.Parameters.Add(new NpgsqlParameter("offset", (page - 1) * 10));
                command.Parameters.Add(new NpgsqlParameter("limit", 10));
            }
            else
            {
                command.Parameters.Add(new NpgsqlParameter("offset", page)); ;
                command.Parameters.Add(new NpgsqlParameter("limit", 20));
            }
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Teacher tch = new Teacher(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                                  reader.GetValue(2).ToString(), Convert.ToInt32(reader.GetValue(3)),
                                  Convert.ToInt64(reader.GetValue(4)), reader.GetValue(6).ToString());
                tch_list.Add(tch);
            }
            dbconnection.Close();
            return tch_list;
        }

        public List<Teacher> Search(int minExp, int MaxExp, string subjectTitle)
        {
            List<Teacher> tch_list = new List<Teacher>();
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            string search = subjectTitle != null && subjectTitle.Length > 0 ? subjectTitle.ToUpper() : "";
            command.CommandText = "SELECT tch.*, sub.* FROM public.subjects as sub " +
                                    "join public.teachers as tch on tch.main_subject_id = sub.id " +
                                    "where tch.experience > :min and tch.experience < :max and Upper(sub.title) Like :title ORDER BY tch.id ASC";
            command.Parameters.Add(new NpgsqlParameter("min", minExp));
            command.Parameters.Add(new NpgsqlParameter("max", MaxExp));
            command.Parameters.Add(new NpgsqlParameter("title", "%" + search + "%"));
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Teacher tch = new Teacher(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                                  reader.GetValue(2).ToString(), Convert.ToInt32(reader.GetValue(3)),
                                  Convert.ToInt64(reader.GetValue(4)), reader.GetValue(6).ToString());
                tch_list.Add(tch);
            }
            dbconnection.Close();
            return tch_list;
        }

        public override void Update(Teacher entity)
        {
            if (!checkMainSubjectId(entity.MainSubjectId)) throw new Exception("There is no subject with this ID");
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE public.teachers SET firstname = :firstname, lastname = :lastname, experience = :experience, " +
                "main_subject_id = :main_subject_id WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("firstname", entity.FirstName));
            command.Parameters.Add(new NpgsqlParameter("lastname", entity.LastName));
            command.Parameters.Add(new NpgsqlParameter("experience", entity.Experience));
            command.Parameters.Add(new NpgsqlParameter("main_subject_id", entity.MainSubjectId));
            command.Parameters.Add(new NpgsqlParameter("id", entity.Id));
            Console.WriteLine(entity);
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
            }
            catch (PostgresException)
            {
                throw new Exception("Unable to edit teacher");
            }
            dbconnection.Close();
        }
    }
}
