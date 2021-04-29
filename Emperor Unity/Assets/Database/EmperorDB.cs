using System.Collections;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;

public class EmperorDB : MonoBehaviour {

    private string cs;

    void Start() {
        cs = "URI=file:" + Application.persistentDataPath + "/emperor.s3db";
        print(cs);
        CreateTable();
        // addEntry("Jacob", 200, "00:01:32", 23);
        // addEntry("Kaylin", 540, "00:02:31", 56);
        // addEntry("Austin", 600, "00:02:31", 87);
        List<User> ls = generateLeaderboard();
        foreach(User u in ls){
            Debug.Log(u.ToString());
        }
        print(ls.ToString());
    }

    private void CreateTable() {
        using (var con = new SqliteConnection(cs)) {
            con.Open();
            using(var cmd = con.CreateCommand()) {
                // con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText =   "CREATE TABLE IF NOT EXISTS 'leaderboard' ( " +
                                    "   'id' INTEGER PRIMARY KEY AUTOINCREMENT, " + 
                                    "   'name' TEXT NOT NULL, " +
                                    "   'score' INT NOT NULL, " +
                                    "   'time' TEXT NOT NULL, " +
                                    "   'moves' INT NOT NULL " +
                                    ");";
                var result = cmd.ExecuteNonQuery();
                Debug.Log("Create Table: " + result);
            }
        }
    }

    //adds an entry to the table
    public void addEntry(string username, int userscore, string usertime, int usermoves){
        using (var con = new SqliteConnection(cs)) {
            con.Open();
            using (var cmd = con.CreateCommand()) {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText =   "INSERT INTO leaderboard (name, score, time, moves) " +
                                    "VALUES (@Name, @Score, @Time, @Moves);";
                
                cmd.Parameters.Add(new SqliteParameter {
                    ParameterName = "Name",
                    Value = username
                });

                cmd.Parameters.Add(new SqliteParameter {
                    ParameterName = "Score",
                    Value = userscore
                });

                cmd.Parameters.Add(new SqliteParameter {
                    ParameterName = "Time",
                    Value = usertime
                });

                cmd.Parameters.Add(new SqliteParameter {
                    ParameterName = "Moves",
                    Value = usermoves
                });

                Debug.Log(new User() {UserName = username, UserScore = userscore, UserTime = usertime, UserMoves = usermoves}.ToString());
                var result = cmd.ExecuteNonQuery();
                Debug.Log("Insert Entry: " + result);
            }
        }
    }

    //pulls data from table to create leaderboard 
    public List<User> generateLeaderboard(){ //change return type to what will work best
        List<User> lb = new List<User>();
        using (var con = new SqliteConnection(cs)) {
            con.Open();
            using (var cmd = con.CreateCommand()) {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM leaderboard ORDER BY score DESC LIMIT 5";

                Debug.Log("**Leaderboard Top**");
                var reader = cmd.ExecuteReader();
                while(reader.Read()) {
                    var id = reader.GetInt32(0);
                    var name = reader.GetString(1);
                    var score = reader.GetInt32(2);
                    var time = reader.GetString(3);
                    var moves = reader.GetInt32(4);
                    lb.Add(new User() {UserName = name, UserScore = score, UserTime = time, UserMoves = moves});
                    Debug.Log(new User() {UserName = name, UserScore = score, UserTime = time, UserMoves = moves}.ToString());
                }
                Debug.Log("**Leaderboard Bottom**");
            }
        }
        return lb;

    }
}