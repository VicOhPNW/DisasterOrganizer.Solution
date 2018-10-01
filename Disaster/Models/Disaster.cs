using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Disaster;

namespace Disaster.Models
{
    public class Disaster
    {
        private int _id;
        private string _name;
        private string _location;
        private int _volunteers;
        private DateTime _time;
        public Disaster(string name, string location, int volunteers, DateTime time, int id=0)
        {
            _name = name;
            _location = location;
            _volunteers = volunteers;
            _time = time;
            _id = id;
        }
        public string GetName()
        {
            return _name;
        }
        public string GetLocation()
        {
            return _location;
        }
        public int GetVolunteers()
        {
            return _volunteers;
        }
        public DateTime GetTime()
        {
            return _time;
        }
        public int GetId()
        {
            return _id;
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"DELETE FROM disasters;";

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

            cmd.CommandText = @"DELETE FROM disasters WHERE id = @disasterId; DELETE FROM disasters_volunteers WHERE id = @id;";

            MySqlParameter disasterId = new MySqlParameter();
            disasterId.ParameterName = "@disasterId";
            disasterId.Value = _id;
            cmd.Parameters.Add(disasterId);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void Edit(string newName, string newLocation, int newVolunteers, DateTime newTime)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"UPDATE disasters SET (name = @newName, location = @newLocation, voluteers = @newVolunteers, time = @newTime) WHERE id = @searchId;";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);

            MySqlParameter location = new MySqlParameter();
            location.ParameterName = "@newLocation";
            location.Value = newLocation;
            cmd.Parameters.Add(location);

            MySqlParameter volunteers = new MySqlParameter();
            volunteers.ParameterName = "@newVolunteers";
            volunteers.Value = newVolunteers;
            cmd.Parameters.Add(volunteers);

            MySqlParameter time = new MySqlParameter();
            time.ParameterName = "newTime";
            time.Value = newTime;
            cmd.Parameters.Add(time);

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            cmd.ExecuteNonQuery();

            _name = newName;
            _location = newLocation;
            _volunteers = newVolunteers;
            _time = newTime;

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

            cmd.CommandText = @"INSERT INTO disasters (name, location, volunteers, time) VALUES (@name, @location, @volunteers, @time);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name"; 
            name.Value = this.GetName();
            cmd.Parameters.Add(name);

            MySqlParameter location = new MySqlParameter();
            location.ParameterName = "@location";
            location.Value = this.GetLocation();
            cmd.Parameters.Add(location);

            MySqlParameter volunteers = new MySqlParameter();
            volunteers.ParameterName = "@volunteers";
            volunteers.Value = this.GetVolunteers();
            cmd.Parameters.Add(volunteers);

            MySqlParameter time = new MySqlParameter();
            time.ParameterName = "@time";
            time.Value = this.GetTime();
            cmd.Parameters.Add(time);
            
            cmd.ExecuteNonQuery();

            _id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Disaster> GetAll()
        {
            List<Disaster> allDisasters = new List<Disaster> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM disasters;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int disasterId = rdr.GetInt32(0);
                string disasterName = rdr.GetString(1);
                string disasterLocation = rdr.GetString(2);
                int disasterVolunteer = rdr.GetInt32(3);
                DateTime disasterTime = rdr.GetDateTime(4);
                Disaster newDisaster = new Disaster(disasterName, disasterLocation, disasterVolunteer, disasterTime, disasterId);
                allDisasters.Add(newDisaster);
            }
           
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
           
            return allDisasters;
        }
        public static Disaster Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT * FROM disasters WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            int disasterId = 0;
            string disasterName = "";
            string disasterLocation = "";
            int disasterVolunteer = 0;
            DateTime disasterTime = new DateTime(2012, 01, 02);

            while(rdr.Read())
            {
                disasterId = rdr.GetInt32(0);
                disasterName = rdr.GetString(1);
                disasterLocation = rdr.GetString(2);
                disasterVolunteer = rdr.GetInt32(3);
                disasterTime = rdr.GetDateTime(4);
            }
            Disaster newDisaster = new Disaster (disasterName, disasterLocation, disasterVolunteer, disasterTime, disasterId);
           
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
         
            return newDisaster;
        }
        public List<Volunteer> GetVolunteer()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"SELECT volunteers.* FROM disasters
            JOIN disasters_volunteers ON (disasters.id = disasters_volunteers.disasters_id)
            JOIN volunteers ON(disasters_volunteers.volunteer_id = volunteers.id)
            WHERE disasters.id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Volunteer> DisasterVolunteer = new List<Volunteer> { };


            while(rdr.Read())
            {
                int volunteerId = rdr.GetInt32(0);
                string volunteerName = rdr.GetString(1);
                Volunteer newVolunteer = new Volunteer(volunteerName, volunteerId);
                DisasterVolunteer.Add(newVolunteer);
            }
            
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            
            return DisasterVolunteer;
        }
        public void AddVolunteer(Volunteer newVolunteer)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"INSERT INTO disasters_volunteers (disaster_id, volunteer_id) VALUES (@disasterId, @volunteerId);";

            MySqlParameter disasterId = new MySqlParameter();
            disasterId.ParameterName = "@disasterId";
            disasterId.Value = _id;
            cmd.Parameters.Add(disasterId);

            MySqlParameter volunteerId = new MySqlParameter();
            volunteerId.ParameterName = "@volunteerId";
            volunteerId.Value = newVolunteer.GetId();
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
