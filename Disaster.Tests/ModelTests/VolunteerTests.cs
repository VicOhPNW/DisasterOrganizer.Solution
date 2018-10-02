using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Disaster.Controllers;
using Disaster.Models;

namespace Disaster.Tests
{
  [TestClass]
  public class VolunteerTests : IDisposable
  {
    public VolunteerTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=disaster_organizer_test;";
    }
    public void Dispose()
    {
      Volunteer.DeleteAll();
      Disaster.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
       //Arrange, Act
       int result = Volunteer.GetAll().Count;

       //Assert
       Assert.AreEqual(0, result);
     }

     [TestMethod]
     public void Equals_TrueForSameName_Volunteer()
     {
       //Arrange, Act
       Volunteer firstVolunteer = new Volunteer("Ann", 1); //Parameters
       Volunteer secondVolunteer = new Volunteer("Ann", 1);//Parameters

       //Assert
       Assert.AreEqual(firstVolunteer, secondVolunteer);
     }

     [TestMethod]
     public void Save_VolunteerSavesToDatabase_VolunteerList()
     {
       //Arrange
       Volunteer testVolunteer = new Volunteer("Ann", 1);//Parameters
       testVolunteer.Save();

       //Act
       List<Volunteer> result = Volunteer.GetAll();
       List<Volunteer> testList = new List<Volunteer>{testVolunteer};

       //Assert
       CollectionAssert.AreEqual(testList, result);
     }

     [TestMethod]
     public void Save_DatabaseAssignsIdToVolunteer_Id()
     {
       //Arrange
       Volunteer testVolunteer = new Volunteer("Ann", 1);//Parameters
       testVolunteer.Save();

       //Act
       Volunteer savedVolunteer = Volunteer.GetAll()[0];

       int result = savedVolunteer.GetId();
       int testId = testVolunteer.GetId();

       //Assert
       Assert.AreEqual(testId, result);
     }

     [TestMethod]
     public void Find_FindsVolunteerInDatabase_Volunteer()
     {
       //Arrange
       Volunteer testVolunteer = new Volunteer("Ann", 1);//Parameters
       testVolunteer.Save();

       //Act
       Volunteer foundVolunteer = Volunteer.Find(testVolunteer.GetId());

       //Assert
       Assert.AreEqual(testVolunteer, foundVolunteer);
     }
  }
}
