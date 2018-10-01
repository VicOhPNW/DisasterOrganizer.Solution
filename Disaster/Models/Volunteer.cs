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

    }
}
