using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    [Header("Connection Details")]
    public string ConnectionServerName;
    public string ConnectionDatabaseName;
    public string ConnectionUsername;
    public string ConnectionPassword;
    [Space(10)]
    [Header("Insert Information")]
    public string insertTableName;
    public string InsertID;
    public string InsertValue;
    public string InsertName;
    [Space(10)]
    [Header("Select Information")]
    public string selectTableName;
    public string selectColumnName;
    [Space(10)]
    public Text uiText;

    // Start is called before the first frame update
    void Start()
    {
        string connetionString = null;
        connetionString = "server=" + ConnectionServerName + ";database=" + ConnectionDatabaseName + ";uid=" + ConnectionUsername + ";pwd=" + ConnectionPassword;
        MySqlConnection conn = new MySqlConnection(connetionString);
        try
        {
            Debug.Log("Starting Connection");
            conn.Open();
            Debug.Log("Connection Opened");
            conn.Close();
        }
        catch (Exception ex)
        {
            Debug.Log("Connection failed to open");
        }
    }

    public void InsertScore()
    {
        string connetionString = null;
        connetionString = "server=" + ConnectionServerName + ";database=" + ConnectionDatabaseName + ";uid=" + ConnectionUsername + ";pwd=" + ConnectionPassword;
        MySqlConnection conn = new MySqlConnection(connetionString);
        try
        {
            Debug.Log("Starting Connection");
            conn.Open();
            Debug.Log("Inserting Into Database");
            string sql = "INSERT INTO " + insertTableName + " VALUES(" + InsertID + ", " + InsertValue + ", " + InsertName + ")";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Debug.Log("Score added");
            conn.Close();
            ShowScore();
        }
        catch (Exception ex)
        {
            Debug.Log("Debug not working");
        }
    }

    private void ShowScore()
    {
        string connetionString = null;
        connetionString = "server=" + ConnectionServerName + ";database=" + ConnectionDatabaseName + ";uid=" + ConnectionUsername + ";pwd=" + ConnectionPassword;
        MySqlConnection conn = new MySqlConnection(connetionString);
        try
        {
            Debug.Log("Starting Connection");
            conn.Open();
            Debug.Log("Selecting Into Database");
            string sql = "SELECT " + selectColumnName + " FROM " + selectTableName;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Debug.Log(rdr[0]);
                uiText.text = "Highscore: " + rdr[0].ToString();
            }

            rdr.Close();
            conn.Close();
        }
        catch (Exception ex)
        {
            Debug.Log("Score could not be gotten");
        }
    }
}
