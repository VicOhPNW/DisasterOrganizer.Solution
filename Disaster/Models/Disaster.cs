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
        public static List<Disaster> GetAll()
        {
            List<Disaster> allDisasters = new List<Disaster> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
           
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
            Disaster newDisaster = new Disaster (disasterName, disasterId);
            
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
            List<Volunteer> DisasterVolunteer = new List<Volunteer> {};
            
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

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
