using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Disaster;

namespace Disaster.Models
{
    public class Volunteer
    {
        private int _id;
        private string _name;
        public Volunteer(string name, int id=0)
        {
            _name = name;
            _id = id;
        }
        private string GetName()
        {
            return _name;
        }
        private int GetId()
        {
            return _id;
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"DELETE FROM volunteers;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"DELETE FROM volunteers WHERE id = @volunteerId, DELETE FROM disasters_volunteers WHERE id = @volunteerId;";

            MySqlParameter volunteerId = new MySqlParameter();
            volunteerId.ParameterName = "@volunteerId";
            volunteerId.Value = _id;
            cmd.Parameters.Add(volunteerId);

            cmd.ExecuteNonQuery;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void Edit(string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Volunteer> GetAll()
        {
            List<Volunteer> allVolunteers = new List<Volunteer> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allVolunteers;
        }
        public static Volunteer Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            Volunteer newVolunteer = new Volunteer(volunteerName, volunteerId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newVolunteer;
        }
        public List<Disaster> GetDisaster()
        {
            List<Disaster> VolunteerDisaster = new List<Disaster>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return VolunteerDisaster;
        }
        public void AddDisaster(Disaster newDisaster)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
