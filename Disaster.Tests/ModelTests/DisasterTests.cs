using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Disaster.Controllers;
using Disaster.Models;

namespace Disaster.Tests
{
  [TestClass]
  public class DisasterTests : IDisposable
  {
    public DisasterTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=disaster_organizer_test;";
    }

  [TestMethod]
   public void GetAll_DatabaseEmptyAtFirst_0()
   {
     //Arrange, Act
     int result = Disaster.GetAll().Count;

     //Assert
     Assert.AreEqual(0, result);
   }

   [TestMethod]
   public void Equals_TrueForSameName_Disaster()
   {
     //Arrange, Act
     Disaster firstDisaster = new Disaster("Earthquake", "San Francisco, CA",
     "Alex",  "12PM");
     Disaster secondDisaster = new Disaster("Earthquake", "San Francisco, CA",
     "Alex", "12PM");

     //Assert
     Assert.AreEqual(firstDisaster, secondDisaster);
   }

   [TestMethod] //Save method for disaster list
   public void Save_DisasterSavesToDatabase_DisasterList()
   {
     //Arrange
     Disaster testDisaster = new Disaster("Earthquake", "San Francisco, CA",
     "Alex", "12PM");
     testDisaster.Save();

     //Act
     List<Disaster> result = Disaster.GetAll();
     List<Disaster> testList = new List<Disaster>{testDisaster};

     //Assert
     CollectionAssert.AreEqual(testList, result);
   }

   [TestMethod]
   public void Save_DatabaseAssignsIdToDisaster_Id()
   {
     //Arrange
     Disaster testDisaster = new Disaster("Earthquake", "San Francisco, CA",
     "Alex", "12PM");
     testDisaster.Save();

     //Act
     Disaster savedDisaster = Disaster.GetAll()[0];

     int result = savedDisaster.GetId();
     int testId = testDisaster.GetId();

     //Assert
     Assert.AreEqual(testId, result);
   }

   [TestMethod]
   public void Find_FindsDisasterInDatabase_Disaster()
   {
     //Arrange
     Disaster testDisaster = new Disaster("Earthquake", "San Francisco, CA",
     "Alex", "12PM");
     testDisaster.Save();

     //Act
     Disaster foundDisaster = Disaster.Find(testDisaster.GetId());

     //Assert
     Assert.AreEqual(testDisaster, foundDisaster);
   }

   [TestMethod]//this retrieves all of the existing volunteers belonging to the disasters
   public void GetVolunteers__AllVolunteerswithDisasters_VolunteerList()
   {
     //Arrange
     Disasters testDisaster = new Disaster("Earthquake", "San Francisco, CA",
     "Alex", "12PM");
     testDisaster.Save();

     //Act
     Volunteer firstVolunteer = new Volunteer("Nancy", "2020-01-02", "San Francisco, CA",
     "Alex", "12PM", 13, testDisaster.GetId());
     Volunteer secondVolunteer = new Volunteer("Nancy", "2020-01-02", "San Francisco, CA",
     "Alex", "12PM", 13, testDisaster.GetId());

     List<Volunteer> testVolunteerList = new List<Volunteer> {firstVolunteer, secondVolunteer};
     List<Volunteer>  resultVolunteerList = testDisaster.GetVolunteers();

     //Assert
     Collections.Assert.AreEqual(testVolunteerList, resultVolunteerList);
   }
   public void Dispose()
   {
     Volunteer.DeleteAll();
     Disaster.DeleteAll();
   }
  }
}
