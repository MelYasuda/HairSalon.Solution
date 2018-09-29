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

    [TestMethod]
    public void GetAll_NumberOfSlients_0()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Delete_DeleteSpecificClientFromDatabase_ClientList()
    {
      //Arrange
      Client testClient = new Client("Mel", 1);
      testClient.Save();
      testClient.Delete();

      //Act
      List<Client> clients = Client.GetAll();
      int result = clients.Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Find_FindClient_FoundClient()
    {
      //Arrange
      Client testClient = new Client("Mel", 1);
      testClient.Save();

      //Act
      Client foundClient = Client.Find(testClient.GetId());

      //Assert
      Assert.AreEqual(testClient, foundClient);
    }

    [TestMethod]
    public void Edit_EditSpecificClient_String()
    {
      //Arrange
      Client testClient = new Client("Mel", 1);
      testClient.Save();
      testClient.Edit("Mel2");

      Client foundClient = Client.Find(testClient.GetId());
      String clientNewName = foundClient.GetName();

      Assert.AreEqual("Mel2", clientNewName);
    }
  }
}
