using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Speciality
  {
    private int _id;
    private string _description;

    public Speciality(string description, int id = 0)
    {
      _description = description;
      _id = id;
    }

    public string GetDescription()
    {
      return _description;
    }

    public int GetId()
    {
      return _id;
    }

    public static List<Speciality> GetAll()
    {
      List<Speciality> allSpecialities = new List<Speciality> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialities;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int specialityId = rdr.GetInt32(0);
        string specialityDescription = rdr.GetString(1);
        Speciality newSpeciality = new Speciality(specialityDescription, specialityId);
        allSpecialities.Add(newSpeciality);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allSpecialities;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialities;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialities (description) VALUES (@description);";

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@description";
      description.Value = this._description;
      cmd.Parameters.Add(description);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


  }
}
