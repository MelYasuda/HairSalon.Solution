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

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int stylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, stylistId, clientId);
        allClients.Add(newClient);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @clientId;";

      MySqlParameter clientId = new MySqlParameter();
      clientId.ParameterName = "@clientId";
      clientId.Value = this.GetId();
      cmd.Parameters.Add(clientId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int ClientId = 0;
      string ClientName = "";
      int StylistId = 0;
      while(rdr.Read())
      {
        ClientId = rdr.GetInt32(0);
        ClientName = rdr.GetString(1);
        StylistId = rdr.GetInt32(2);
      }
      Client foundClient = new Client(ClientName, StylistId, ClientId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundClient;
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

    public void Edit(string editName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = editName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _name = editName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }

}
