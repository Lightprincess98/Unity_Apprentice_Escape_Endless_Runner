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

    private ScoreManager scoreManager;
    private int id;

    [Space(10)]
    public Text uiText;
    public InputField uiInputField;


    // Start is called before the first frame update
    void Start()
    {
        id = 2;
        scoreManager = GameObject.FindGameObjectWithTag("Score Manager").GetComponent<ScoreManager>();
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
            Debug.Log("Connection Failed");
        }
    }

    public void InsertScore()
    {
        id = UnityEngine.Random.Range(id, 9000);
        string connetionString = null;
        connetionString = "server=" + ConnectionServerName + ";database=" + ConnectionDatabaseName + ";uid=" + ConnectionUsername + ";pwd=" + ConnectionPassword;
        MySqlConnection conn = new MySqlConnection(connetionString);
        try
        {
            Debug.Log("Starting Connection");
            conn.Open();
            Debug.Log("Inserting Into Database");
            string sql = "INSERT INTO score VALUES('" + id + "', '" + scoreManager.Score + "', '" + uiInputField.text + "')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Debug.Log("Score added");
            conn.Close();
            ShowScore();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
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
            string sql = "SELECT Score_Num FROM score";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int highScore = 0;
            while (rdr.Read())
            {
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    Debug.Log(rdr[i]);
                    int currentscore = Convert.ToInt32(rdr[i]);
                    if(currentscore > highScore)
                    {
                        highScore = currentscore;
                    }
                }
                uiText.text = "Highscore: " + highScore;
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
