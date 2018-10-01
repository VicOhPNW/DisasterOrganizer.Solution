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

    }
}
