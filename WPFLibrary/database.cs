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
        public string ConnectionToDB()
        {
            string str = "s";
            try
            {
                string connstring = "server=localhost; uid=root; pwd=root; database=db_space_invaders; port=6033;";
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = connstring;
                con.Open();
                string sql = "SELECT * FROM t_arme";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    str = reader["idArme"] + " " + reader["armNom"];
                }
            }
            catch (MySqlException ex)
            {
                str = ex.Message;
            }
            return str;
        }

        public void WriteScore()
        {

        }
    }
}
