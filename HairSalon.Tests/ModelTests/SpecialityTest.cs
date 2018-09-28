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
    public void AddSpecialities_AddsSpecialitiesToStylist_StylistList()
    {
        //Arrange
        Stylist testStylist = new Stylist("Mel");
        testStylist.Save();

        Speciality testSpeciality = new Speciality("Cut");
        testSpeciality.Save();

        //Act
        testStylist.AddSpecialities(testSpeciality);

        List<Speciality> result = testStylist.GetSpecialities();
        List<Speciality> testList = new List<Speciality>{testSpeciality};

        //Assert
        CollectionAssert.AreEqual(testList, result);
    }

    // [TestMethod]
    // public void GetSpecialities_ReturnsAllStylistSpecialities_SpecialityList()
    // {
    //     //Arrange
    //     Stylist testStylist = new Stylist("Mel");
    //     testStylist.Save();
    //
    //     Speciality testSpeciality = new Speciality("Cut");
    //     testSpeciality.Save();
    //
    //     //Act
    //     testStylist.AddSpecialities(testSpeciality);
    //     List<Speciality> result = testStylist.GetSpecialities();
    //     List<Speciality> testList = new List<Speciality> {testSpeciality};
    //
    //     //Assert
    //     CollectionAssert.AreEqual(testList, result);
    // }
    //
    // [TestMethod]
    // public void Delete_DeletesStylistAssociationsFromDatabase_StylistList()
    // {
    //     //Arrange
    //     Speciality testSpeciality = new Speciality("Cut");
    //     testSpeciality.Save();
    //
    //     Stylist testStylist = new Stylist("Mel");
    //     testStylist.Save();
    //
    //     //Act
    //     testStylist.AddSpecialities(testSpeciality);
    //     testStylist.Delete();
    //
    //     List<Stylist> resultStylist = testSpeciality.GetStylists();
    //     List<Stylist> testStylist2 = new List<Stylist> {};
    //
    //     //Assert
    //     CollectionAssert.AreEqual(testStylist2, resultStylist);
    // }
  }
}
