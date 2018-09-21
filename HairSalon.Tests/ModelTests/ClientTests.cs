using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mel_yasuda_test;";
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
      Client client1 = new Client("Mel", 1);
      Client client2 = new Client("Mel", 1);

      //Assert
      Assert.AreEqual(client1, client2);
    }

    [TestMethod]
    public void Save_SaveClientToDatabase_ClientList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Mel");
      testStylist.Save();
      Client testClient = new Client("Doe", testStylist.GetId());
      testClient.Save();

      //Act
      List<Client> result = testStylist.GetClients();
      List<Client> testList = new List<Client>{testClient};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
  }
}
