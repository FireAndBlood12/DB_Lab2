using DB_LAB2.Database.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Database
{
    public class GroupDAO : DAO<Group>
    {
        public GroupDAO(DBConnection db) : base(db)
        {
        }

        public override long Count()
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM public.groups";
            NpgsqlDataReader reader = command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count = Convert.ToInt32(reader.GetValue(0));
            }
            dbconnection.Close();
            return count;
        }

        public override void Create(Group entity)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO public.groups (code, entrance_year) VALUES (:code, :entrance_year)";
            command.Parameters.Add(new NpgsqlParameter("code", entity.Code));
            command.Parameters.Add(new NpgsqlParameter("entrance_year", entity.EntranceYear));
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
            }
            catch (PostgresException)
            {
                throw new Exception("Unable to create new Group");
            }
            dbconnection.Close();
        }

        public override void Delete(long id)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM public.groups WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            command.ExecuteNonQuery();
            dbconnection.Close();
        }

        public override Group GetById(long id)
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
            return g;
        }

        public override List<Group> GetList(int page)
        {
            List<Group> g_list = new List<Group>();
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM public.groups ORDER BY id ASC LIMIT :limit OFFSET :offset";
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
                Group g = new Group(Convert.ToInt64(reader.GetValue(0)),
                                  reader.GetValue(1).ToString(),
                                  Convert.ToDateTime(reader.GetValue(2)));
                g_list.Add(g);
            }
            dbconnection.Close();
            return g_list;
        }

        public override void Update(Group entity)
        {
            NpgsqlConnection connection = dbconnection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE public.groups SET code = :code, entrance_year = :entrance_year WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", entity.Id));
            command.Parameters.Add(new NpgsqlParameter("code", entity.Code));
            command.Parameters.Add(new NpgsqlParameter("entrance_year", entity.EntranceYear));
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
            }
            catch (PostgresException)
            {
                throw new Exception("Unable to edit group");
            }
            dbconnection.Close();
        }
    }
}
