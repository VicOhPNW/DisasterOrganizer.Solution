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
        public string GetName()
        {
            return _name;
        }
        public int GetId()
        {
            return _id;
        }
        public override bool Equals(System.Object otherVolunteer)
        {
            if(!(otherVolunteer is Volunteer))
            {
                return false;
            }
            else
            {
                Volunteer newVolunteer = (Volunteer)otherVolunteer;
                bool idEquality = this.GetId() == newVolunteer.GetId();
                bool nameEquality = this.GetName() == newVolunteer.GetName();
                return (idEquality && nameEquality);
            }
        }
        public override int GetHashCode()
        {
            string allHash = this.GetName();
            return allHash.GetHashCode();
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

            cmd.ExecuteNonQuery();

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

            cmd.CommandText = @"UPDATE volunteers SET name = @newName WHERE id = @volunteerId;";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);

            MySqlParameter volunteerId = new MySqlParameter();
            volunteerId.ParameterName = "@volunteerId";
            volunteerId.Value = _id;
            cmd.Parameters.Add(volunteerId);

            cmd.ExecuteNonQuery();
            
            _name = newName;

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

            cmd.CommandText = @"INSERT INTO volunteers (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this.GetName();
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();

            _id = (int)cmd.LastInsertedId;

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

            cmd.CommandText = @"SELECT * FROM volunteers;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int volunteerId = rdr.GetInt32(0);
                string volunteerName = rdr.GetString(1);
                Volunteer newVolunteer = new Volunteer(volunteerName, volunteerId);
                allVolunteers.Add(newVolunteer);
            }

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

            cmd.CommandText = @"SELECT * FROM volunteers WHERE id = @volunteerId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@volunteerId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            int volunteerId = 0;
            string volunteerName = "";

            while(rdr.Read())
            {
                volunteerId = rdr.GetInt32(0);
                volunteerName = rdr.GetString(1);
            }

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

            cmd.CommandText = @"SELECT disasters.* FROM volunteers
            JOIN disasters_volunteers ON (volunteers.id = disasters_volunteers.volunteer_id)
            JOIN disasters ON (disasters_volunteers.disaster_id = disasters.id)
            WHERE volunteers.id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int disasterId = rdr.GetInt32(0);
                string disasterName = rdr.GetString(1);
                string disasterLocation = rdr.GetString(2);
                int disasterVolunteer = rdr.GetInt32(3);
                DateTime disasterTime = rdr.GetDateTime(4);
                Disaster newDisaster = new Disaster(disasterName, disasterLocation, disasterVolunteer, disasterTime, disasterId);
                VolunteerDisaster.Add(newDisaster);
            }

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

            cmd.CommandText = @"INSERT INTO disasters_volunteers (disaster_id, volunteer_id) VALUES (@disasterId, @volunteerId);";

            MySqlParameter disasterId = new MySqlParameter();
            disasterId.ParameterName = "@disasterId";
            disasterId.Value = newDisaster.GetId();
            cmd.Parameters.Add(disasterId);

            MySqlParameter volunteerId = new MySqlParameter();
            volunteerId.ParameterName = "@volunteerId";
            volunteerId.Value = _id;
            cmd.Parameters.Add(volunteerId);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
