using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialityTests : IDisposable
  {
    public SpecialityTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mel_yasuda_test;";
    }

    [TestMethod]
    public void GetAll_NumberOfSpecialities_0()
    {
      //Arrange, Act
      int result = Speciality.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    public void Dispose()
    {
       Client.DeleteAll();
      Stylist.DeleteAll();
      Speciality.DeleteAll();
    }

    [TestMethod]
    public void Equals_DifferentObjects_True()
    {
      //Arrange, Act
      Speciality speciality1 = new Speciality("Cut", 1);
      Speciality speciality2 = new Speciality("Cut", 1);

      //Assert
      Assert.AreEqual(speciality1, speciality2);
    }

    [TestMethod]
    public void Save_SaveSpeciality_List()
    {
      //Arrange
      Speciality testSpeciality = new Speciality("Cut");
      testSpeciality.Save();

      //Act
      List<Speciality> result = Speciality.GetAll();
      List<Speciality> testList = new List<Speciality>{testSpeciality};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_FindSpeciality_FoundSpeciality()
    {
      //Arrange
      Speciality speciality1 = new Speciality("Cut");
      speciality1.Save();

      //Act
      Speciality foundSpeciality = Speciality.Find(speciality1.GetId()); //why just 0 doens't work?

      //Assert
      Assert.AreEqual(speciality1, foundSpeciality);
    }



    [TestMethod]
    public void AddStylists_AddsStylistsToSpeciality_StylistList()
    {
        //Arrange
        Stylist testStylist = new Stylist("Mel");
        testStylist.Save();

        Speciality testSpeciality = new Speciality("Cut");
        testSpeciality.Save();

        //Act
        testSpeciality.AddStylists(testStylist);

        List<Stylist> result = testSpeciality.GetStylists();
        List<Stylist> testList = new List<Stylist>{testStylist};

        //Assert
        CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetStylists_ReturnsAllSpecialityStylists_StylistList()
    {
        //Arrange
        Stylist testStylist = new Stylist("Mel");
        testStylist.Save();

        Speciality testSpeciality = new Speciality("Cut");
        testSpeciality.Save();

        //Act
        testSpeciality.AddStylists(testStylist);
        List<Stylist> result = testSpeciality.GetStylists();
        List<Stylist> testList = new List<Stylist> {testStylist};

        //Assert
        CollectionAssert.AreEqual(testList, result);
    }
  }
}
