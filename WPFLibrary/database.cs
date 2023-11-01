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
        public string[] ConnectionToDBReading(string sql, string table1)
        {
            string[] str = new string[10];
            string[] exception = new string[1];
            int i = 0;

            try
            {
                string connstring = "server=localhost; uid=root; pwd=root; database=db_space_invaders; port=6033;";
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = connstring;
                con.Open();
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
                exception[0] = ex.Message;
                return exception;
            }
            return str;
        }

        public string tryConnection()
        {
            string sql = "SELECT * FROM t_joueur";
            try
            {
                string connstring = "server=localhost; uid=root; pwd=root; database=db_space_invaders; port=6033;";
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = connstring;
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    return "connection successful";
                }
            }
            catch (MySqlException ex)
            {
                return ex + "";
            }
            return "connection successful";
        }

        /// <summary>
        /// Sert à sauvegarder le nom et le score du joueur
        /// </summary>
        /// <param name="player">Nom du joueur</param>
        /// <param name="score">Score du joueur</param>
        /// <returns></returns>
        public string WriteScore(string player, string score)
        {
            // Identification du serveur
            string connstring = "server=localhost; uid=root; pwd=root; database=db_space_invaders;";
            MySqlConnection con = new MySqlConnection(connstring);

            // try catch pour éviter les erreurs
            try
            {
                con.Open();

                // Commande sql
                string sql = "INSERT INTO db_space_invaders.t_joueur (jouPseudo, jouNombrePoints) VALUES (@jouPseudo, @jouNombrePoints)";
                MySqlCommand cmd = new MySqlCommand(sql, con);

                // Ajoutez les paramètres pour le nom du joueur et le score.
                cmd.Parameters.AddWithValue("@jouPseudo", player);
                cmd.Parameters.AddWithValue("@jouNombrePoints", score);

                // Exécutez la commande d'insertion.
                cmd.ExecuteNonQuery();

                // Affichez un message de succès en utilisant MessageBox.Show.
                return "Score ajouté avec succès !";
            }
            catch (MySqlException ex)
            {
                // Gérez les erreurs de connexion à la base de données en affichant une boîte de dialogue.
                return "Erreur de base de données : " + ex.Message;
            }
        }
        /// <summary>
        /// Sert à montrer les noms du highscore
        /// </summary>
        /// <returns></returns>
        public string[] ShowHighscoreNames()
        {
            string cmdSql = "SELECT jouPseudo FROM t_joueur order by jouNombrePoints DESC limit 10";
            string table1 = "jouPseudo";
            string[] Answer = new string[10];
            Answer = ConnectionToDBReading(cmdSql, table1);
            return Answer;
        }

        public string[] showHighscoreScore()
        {
            string cmdSql = "SELECT jouNombrePoints FROM t_joueur order by jouNombrePoints DESC limit 10";
            string table1 = "jouNombrePoints";
            string[] Answer = new string[10];
            Answer = ConnectionToDBReading(cmdSql, table1);
            return Answer;
        }
    }
}
