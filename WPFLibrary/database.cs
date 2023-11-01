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
        public string[] ConnectionToDB(string sql, string table1)
        {
            string[] str = new string[10];
            int i = 0;
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

                while (reader.Read())
                {
                    str[i] = reader[table1] + "";
                    i++;
                }

            }
            catch (MySqlException ex)
            {
                str[0] = ex.Message;
            }
            return str;
        }

        public string WriteScore(string player, string score)
        {
            string connstring = "server=localhost; uid=root; pwd=root; database=db_space_invaders;";
            MySqlConnection con = new MySqlConnection(connstring);

            try
            {
                con.Open();

                // Créez la commande SQL d'insertion pour ajouter un nouveau score au joueur.
                string sql = "INSERT INTO db_space_invaders.t_joueur (jouPseudo, jouNombrePoints) VALUES (@jouPseudo, @jouNombrePoints)";
                MySqlCommand cmd = new MySqlCommand(sql, con);

                // Ajoutez les paramètres pour le nom du joueur et le score.
                cmd.Parameters.AddWithValue("@jouPseudo", player);
                cmd.Parameters.AddWithValue("@jouNombrePoints", score);



                // Exécutez la commande d'insertion.
                cmd.ExecuteNonQuery();

                // Affichez un message de succès en utilisant MessageBox.Show.
                return "Score enregistré !";


            }
            catch (MySqlException ex)
            {
                // Erreur base de donnée
                return "Erreur de base de données : " + ex.Message;
            }
        }

        public string[] ShowHighscoreNames()
        {
            string cmdSql = "SELECT jouPseudo FROM t_joueur order by jouNombrePoints DESC limit 10";
            string table1 = "jouPseudo";
            string[] Answer = new string[10];
            Answer = ConnectionToDB(cmdSql, table1);
            return Answer;
        }

        public string[] showHighscoreScore()
        {
            string cmdSql = "SELECT jouNombrePoints FROM t_joueur order by jouNombrePoints DESC limit 10";
            string table1 = "jouNombrePoints";
            string[] Answer = new string[10];
            Answer = ConnectionToDB(cmdSql, table1);
            return Answer;
        }

        public string tryConnection()
        {
            string connstring = "server=localhost; uid=root; pwd=root; database=db_space_invaders;";
            MySqlConnection con = new MySqlConnection(connstring);

            try
            {
                con.Open();

                // Créez la commande SQL d'insertion pour ajouter un nouveau score au joueur.
                string sql = "SELECT jouNombrePoints FROM t_joueur";
                MySqlCommand cmd = new MySqlCommand(sql, con);

                // Exécutez la commande d'insertion.
                cmd.ExecuteNonQuery();

                // Affichez un message de succès en utilisant MessageBox.Show.
                return "1";
            }
            catch(MySqlException ex)
            {
                return "Erreur de base de données : " + ex.Message;
            }
            return "a";
        }
    }
}
