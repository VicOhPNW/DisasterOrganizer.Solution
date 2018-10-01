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

        }
        public void Delete()
        {

        }
        public void Edit(string newName)
        {

        }
        public void Save()
        {

        }
        public static List<Volunteer> GetAll()
        {
            List<Volunteer> allVolunteers = new List<Volunteer> { };
            return allVolunteers;
        }
        public static Volunteer Find(int id)
        {
            Volunteer newVolunteer = new Volunteer(volunteerName, volunteerId);
            return newVolunteer;
        }
        public List<Disaster> GetDisaster()
        {
            List<Disaster> VolunteerDisaster = new List<Disaster>{};
            return VolunteerDisaster;
        }
        public void AddDisaster(Disaster newDisaster)
        {

        }

    }
}
