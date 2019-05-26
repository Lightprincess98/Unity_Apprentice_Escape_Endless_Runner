using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    [Header("Connection Details")]
    public string ConnectionServerName;
    public string ConnectionDatabaseName;
    public string ConnectionUsername;
    public string ConnectionPassword;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
