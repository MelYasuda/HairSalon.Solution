using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private string _name;
    private int _stylistId;

    public Client(string clientName, int stylistId, int id = 0)
    {
      _name = clientName;
      _stylistId = stylistId;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }

    public void Save()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@name, @stylistId);";

        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@name";
        name.Value = _name;
        cmd.Parameters.Add(name);

        MySqlParameter stylistId = new MySqlParameter();
        stylistId.ParameterName = "@stylistId";
        stylistId.Value = _stylistId;
        cmd.Parameters.Add(stylistId);

        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }


    public List<Client> GetClients()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tasks WHERE stylist_id = @stylist_id;";

      MySqlParameter newSlylistId = new MySqlParameter();
      newSlylistId.ParameterName = "@stylist_id";
      newSlylistId.Value = this.GetId();
      cmd.Parameters.Add(newSlylistId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int id = 0;
      string name = "";
      int stylist_id = 0;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
        stylist_id = rdr.GetInt32(3);
        Client newClient = new Client(name, stylist_id, id);
        allClients.Add(newClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }



    public override bool Equals(System.Object otherClient)
    {
        if (!(otherClient is Client))
        {
            return false;
        }
        else
        {
            Client newClient = (Client) otherClient;
            bool areIdsEqual = (this.GetId() == newClient.GetId());
            bool areDescriptionsEqual = (this.GetName() == newClient.GetName());
            return (areIdsEqual && areDescriptionsEqual);
        }
    }

    public override int GetHashCode()
    {
        return this.GetName().GetHashCode();
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }



  }

}
