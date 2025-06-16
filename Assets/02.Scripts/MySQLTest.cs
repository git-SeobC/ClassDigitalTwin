using UnityEngine;
using MySql.Data.MySqlClient;

public class MySQLTest : MonoBehaviour
{
    void Start()
    {
        string server = "localhost";
        string database = "twinpro"; // 예: uni12twinpro
        string uid = "root";
        string password = "12341qaz"; // 실제 root 비밀번호 입력
        string port = "3306";

        //string connStr = $"server={server};user={uid};password={password};database={database};port={port};charset=utf8;";
        string connStr = "server=localhost;user=root;password=12341qaz;database=uni12twinpro;SslMode=None;";

        try
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                Debug.Log("MySQL 연결 성공!");

                string query = "SELECT * FROM testtable";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("ID");
                        string name = reader.GetString("Name");
                        byte state = reader.GetByte("State");
                        Debug.Log($"ID: {id}, Name: {name}, State: {state}");
                    }
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("MySQL 연결 실패: " + ex.Message);
        }
    }
}
