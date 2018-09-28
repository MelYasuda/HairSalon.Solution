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

    public static Speciality Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialities WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int specialityId = 0;
      string specialityDescription = "";

      rdr.Read();
      specialityId = rdr.GetInt32(0);
      specialityDescription = rdr.GetString(1);

      Speciality newSpeciality = new Speciality(specialityDescription, specialityId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newSpeciality;
    }

    public void AddStylists(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialities (stylist_id, speciality_id) VALUES (@StylistId, @SpecialityId);";

      MySqlParameter stylist_id = new MySqlParameter();
      stylist_id.ParameterName = "@StylistId";
      stylist_id.Value = newStylist.GetId();
      cmd.Parameters.Add(stylist_id);

      MySqlParameter speciality_id = new MySqlParameter();
      speciality_id.ParameterName = "@SpecialityId";
      speciality_id.Value = _id;
      cmd.Parameters.Add(speciality_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Stylist> GetStylists()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM specialities
      JOIN stylists_specialities ON (specialities.id = stylists_specialities.speciality_id)
      JOIN stylists ON (stylists_specialities.stylist_id = stylists.id)
      WHERE specialities.id = @SpecialityId;";

      MySqlParameter specialityIdParameter = new MySqlParameter();
      specialityIdParameter.ParameterName = "@SpecialityId";
      specialityIdParameter.Value = _id;
      cmd.Parameters.Add(specialityIdParameter);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Stylist> stylists = new List<Stylist>{};

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylistName, stylistId);
        stylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return stylists;
    }
  }
}
