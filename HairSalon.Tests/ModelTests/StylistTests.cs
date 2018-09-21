using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mel_yasuda_test;";
    }

    [TestMethod]
    public void GetAll_NumberOfStylists_0()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    public void Dispose()
    {
       Client.DeleteAll();
      Stylist.DeleteAll();
    }

    [TestMethod]
    public void Equals_DifferentObjects_True()
    {
      //Arrange, Act
      Stylist stylist1 = new Stylist("Mel", 1);
      Stylist stylist2 = new Stylist("Mel", 1);

      //Assert
      Assert.AreEqual(stylist1, stylist2);
    }

    [TestMethod]
    public void Save_SaveStylist_List()
    {
      //Arrange
      Stylist testStylist = new Stylist("Mel");
      testStylist.Save();

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_FindStylist_FoundStylist()
    {
      //Arrange
      Stylist stylist1 = new Stylist("Mel");
      stylist1.Save();

      //Act
      Stylist foundStylist = Stylist.Find(stylist1.GetId()); //why just 0 doens't work?

      //Assert
      Assert.AreEqual(stylist1, foundStylist);
    }

    [TestMethod]
    public void GetClients_FindClientsByStylistId_FoundClientsList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Mel");
      testStylist.Save();

      Client client1 = new Client("AJ", testStylist.GetId());
      client1.Save();
      Client client2 = new Client("Chan", testStylist.GetId());
      client2.Save();

      List<Client> testClientList = new List<Client> {client1, client2};
      List<Client> resultClientsList = testStylist.GetClients();

      CollectionAssert.AreEqual(testClientList, resultClientsList);
    }


  }
}
