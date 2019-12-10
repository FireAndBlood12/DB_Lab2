using DB_LAB2.Database.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Database
{
    public class StudentDAO : DAO<Student>
    {
        public StudentDAO(DBConnection db) : base(db)
        {
        }

        public override long Count()
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM public.students";
            NpgsqlDataReader reader = command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count = Convert.ToInt32(reader.GetValue(0));
            }
            dbconnection.Close();
            return count;
        }
        private bool checkGroupId(long id)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM public.groups gr WHERE gr.id = :group_id";
            command.Parameters.Add(new NpgsqlParameter("group_id", id));
            NpgsqlDataReader reader = command.ExecuteReader();
            Group g = null;
            while (reader.Read())
            {
                g = new Group(Convert.ToInt64(reader.GetValue(0)),
                                  reader.GetValue(1).ToString(),
                                  Convert.ToDateTime(reader.GetValue(2)));
            }
            dbconnection.Close();
            return g != null;
        }
        public override long Create(Student entity)
        {
            if (!checkGroupId(entity.GroupId)) throw new Exception("There is no group with this ID");
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO public.students (firstname, lastname, birthday, group_id) VALUES (:firstname, :lastname, :birthday, :group_id)  RETURNING  id";
            command.Parameters.Add(new NpgsqlParameter("firstname", entity.FirstName));
            command.Parameters.Add(new NpgsqlParameter("lastname", entity.LastName));
            command.Parameters.Add(new NpgsqlParameter("birthday", entity.Birthday));
            command.Parameters.Add(new NpgsqlParameter("group_id", entity.GroupId));
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
                throw new Exception("Unable to create new Student");
            }
        }

        public override void Delete(long id)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM public.students WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            command.ExecuteNonQuery();
            dbconnection.Close();
        }

        public override Student GetById(long id)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT st.*, gr.* FROM public.groups as gr "
                                    + "join public.students as st on st.group_id = gr.id and st.id = :student_id";
            command.Parameters.Add(new NpgsqlParameter("student_id", id));
            NpgsqlDataReader reader = command.ExecuteReader();
            Student st = null;
            while (reader.Read())
            {
                st = new Student(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                                  reader.GetValue(2).ToString(), Convert.ToDateTime(reader.GetValue(3)),
                                  Convert.ToInt64(reader.GetValue(4)), reader.GetValue(6).ToString(), Convert.ToDateTime(reader.GetValue(7)));
            }
            dbconnection.Close();
            return st;
        }

        public override List<Student> GetList(int page)
        {
            List<Student> st_list = new List<Student>();
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT st.*, gr.* FROM public.groups as gr "
                                    + "join public.students as st on st.group_id = gr.id ORDER BY st.id ASC LIMIT :limit OFFSET :offset";
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
                Student st = new Student(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                                  reader.GetValue(2).ToString(), Convert.ToDateTime(reader.GetValue(3)),
                                  Convert.ToInt64(reader.GetValue(4)), reader.GetValue(6).ToString(), Convert.ToDateTime(reader.GetValue(7)));
                st_list.Add(st);
            }
            dbconnection.Close();
            return st_list;
        }

        public override void Update(Student entity)
        {
            if (!checkGroupId(entity.GroupId)) throw new Exception("There is no group with this ID");
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE public.students SET firstname = :firstname, lastname = :lastname, birthday = :birthday, " +
                "group_id = :group_id WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("firstname", entity.FirstName));
            command.Parameters.Add(new NpgsqlParameter("lastname", entity.LastName));
            command.Parameters.Add(new NpgsqlParameter("birthday", entity.Birthday));
            command.Parameters.Add(new NpgsqlParameter("group_id", entity.GroupId));
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
