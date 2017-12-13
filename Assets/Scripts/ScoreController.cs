using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class ScoreController : MonoBehaviour {

    private string connectionString;
    public static ScoreController scoreController;
    private List<HighScore> highScore = new List<HighScore>();
    public GameObject scorePrefab;
    public Transform scoreParent;
    public GameObject scoreParentGO;
    public int topScores = 20;

    void Start ()
    {
        connectionString = "Data Source=" + Application.dataPath + "/HighScoreDB.db; Version=3;";
        GetScores();
    }

    public void SubmitScore(string name, int newScore)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("insert into HighScore(name, score) values (\"{0}\", \"{1}\")", name, newScore);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    public void GetScores()
    {
        highScore.Clear();
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "select * from HighScore";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        highScore.Add(new HighScore(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        highScore.Sort();
    }

    public void DeleteScore(int id)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("delete from HighScore where PlayerID = \"{0}\"", id);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    public void ShowScores()
    {
        GetScores();
        for (int i = 0; i < highScore.Count && i < topScores; i++)
        {
            GameObject tmpObj = Instantiate(scorePrefab);

            HighScore tmpScore = highScore[i];

            tmpObj.GetComponent<HighScoreScript>().SetScore(tmpScore.Name, tmpScore.Score.ToString(), "#" + (i + 1).ToString());

            tmpObj.transform.SetParent(scoreParent, false);

            tmpObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1); 
        }
    }

    public bool VerifyName(string name)
    {
        GetScores();
        for (int i = 0; i < highScore.Count; i++)
        {
            HighScore tmpScore = highScore[i];
            if (name.Equals(tmpScore.Name, StringComparison.Ordinal))
                return true;
        }
        return false;
    }
}
