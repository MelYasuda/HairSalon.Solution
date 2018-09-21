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
           public void GetAll_StylistsEmptyAtFirst_0()
           {
             //Arrange, Act
             int result = Stylist.GetAll().Count;

             //Assert
             Assert.AreEqual(0, result);
           }

           public void Dispose()
           {
            //  Item.DeleteAll();
             Stylist.DeleteAll();
           }

           [TestMethod]
           public void Equals_ReturnsTrueForSameName_Stylist()
           {
             //Arrange, Act
             Stylist firstStylist = new Stylist("Mel", 1);
             Stylist secondStylist = new Stylist("Mel", 1);

             //Assert
             Assert.AreEqual(firstStylist, secondStylist);
           }
  }
}
