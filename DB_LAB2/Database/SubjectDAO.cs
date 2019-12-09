using DB_LAB2.Database.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Database
{
    public class SubjectDAO : DAO<Subject>
    {
        public SubjectDAO(DBConnection db) : base(db)
        {
        }
        public override long Count()
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM public.subjects";
            NpgsqlDataReader reader = command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count = Convert.ToInt32(reader.GetValue(0));
            }
            dbconnection.Close();
            return count;
        }
        public override void Create(Subject entity)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO public.subjects (title) VALUES (:title)";
            command.Parameters.Add(new NpgsqlParameter("title", entity.Title));
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
            }
            catch (PostgresException)
            {
                throw new Exception("Unable to create new Subject");
            }
            dbconnection.Close();
        }

        public override void Delete(long id)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM public.subjects WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            command.ExecuteNonQuery();
            dbconnection.Close();
        }

        public override Subject GetById(long id)
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
            return s;
        }

        public override List<Subject> GetList(int page)
        {
            List<Subject> s_list = new List<Subject>();
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM public.subjects ORDER BY id ASC LIMIT :limit OFFSET :offset";
            if(page != 0)
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
                Subject s = new Subject(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString());
                s_list.Add(s);
            }
            dbconnection.Close();
            return s_list;
        }

        public override void Update(Subject entity)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE public.subjects SET title = :title WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", entity.Id));
            command.Parameters.Add(new NpgsqlParameter("title", entity.Title));
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
            }
            catch (PostgresException)
            {
                throw new Exception("Unable to edit subject");
            }
            dbconnection.Close();
        }
    }
}
