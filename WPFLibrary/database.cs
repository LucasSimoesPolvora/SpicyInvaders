using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Model
{
    public class database
    {
        public string[] ConnectionToDB(string sql, string table1, string table2)
        {
            string[] str = new string[10];
            List<string> list = new List<string>();
            try
            {
                string connstring = "server=localhost; uid=root; pwd=root; database=db_space_invaders; port=6033;";
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = connstring;
                con.Open();
                //sql = "SELECT * FROM t_arme";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    while (reader.Read())
                    {
                        str[i] = reader[table1] + " " + reader[table2];
                    }
                }
            }
            catch (MySqlException ex)
            {
                str[0] = ex.Message;
            }
            return str;
        }

        public void WriteScore()
        {

        }

        public string[] ShowHighscore()
        {
            string cmdSql = "SELECT jouPseudo, jouNombrePoints FROM t_joueur";
            string table1 = "jouPseudo";
            string table2 = "jouNombrePoints";
            string[] Answer = new string[10];
            Answer = ConnectionToDB(cmdSql, table1, table2);
            return Answer;
        }
    }
}
