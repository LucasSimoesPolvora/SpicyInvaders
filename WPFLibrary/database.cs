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
        /// <summary>
        /// Faires toutes les requêtes SELECT du programme
        /// </summary>
        /// <param name="sql">Commande SQL</param>
        /// <param name="table1">Table choisie</param>
        /// <returns></returns>
        public string[] ConnectionToDBReading(string sql, string table1)
        {
            // String qui va recevoir toutes les 
            string[] str = new string[10];
            string[] exception = new string[1];
            int i = 0;

            try
            {
                // Infos de la base de donnée
                string connstring = "server=localhost; uid=root; pwd=root; database=db_space_invaders; port=6033;";
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = connstring;

                // Ouverture de la connexion
                con.Open();

                // faire la commande
                MySqlCommand cmd = new MySqlCommand(sql, con);

                // Lire la commande
                MySqlDataReader reader = cmd.ExecuteReader();

                // Noter les résultats dans le tableau
                while (reader.Read())
                {
                    str[i] = reader[table1] + "";
                    i++;
                }

            }
            // Réception d'une erreur
            catch (MySqlException ex)
            {
                exception[0] = ex.Message;
                return exception;
            }
            return str;
        }

        /// <summary>
        /// Essayer de faire une connection à la base de donnée pour voir si ca marche
        /// </summary>
        /// <returns>réponse si ca marche</returns>
        public string tryConnection()
        {
            // Commande SQL
            string sql = "SELECT * FROM t_joueur";
            try
            {
                // Connexion à la db
                string connstring = "server=localhost; uid=root; pwd=root; database=db_space_invaders; port=6033;";
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = connstring;
                // Ouverture de la connexion
                con.Open();

                // Faire la requête
                MySqlCommand cmd = new MySqlCommand(sql, con);

                // Lire la requête
                MySqlDataReader reader = cmd.ExecuteReader();

                // Réussi
                while (reader.Read())
                {
                    return "connection successful";
                }
            }
            // S'il y a une erreur
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

                // Ajoute les valeurs
                cmd.Parameters.AddWithValue("@jouPseudo", player);
                cmd.Parameters.AddWithValue("@jouNombrePoints", score);

                // Exécute la commande
                cmd.ExecuteNonQuery();

                // Affiche un popup pour dire que la requête a fonctionné
                return "Score ajouté avec succès !";
            }
            // S'il y a une erreur
            catch (MySqlException ex)
            {
                // Affiche un popup si il y a une erreur
                return "Erreur de base de données : " + ex.Message;
            }
        }
        /// <summary>
        /// Sert à montrer les noms du highscore
        /// </summary>
        /// <returns></returns>
        public string[] ShowHighscoreNames()
        {
            // Requête SQL
            string cmdSql = "SELECT jouPseudo FROM t_joueur order by jouNombrePoints DESC limit 10";
            string table1 = "jouPseudo";

            // tableau qui va recevoir les réponses
            string[] Answer = new string[10];
            Answer = ConnectionToDBReading(cmdSql, table1);

            // Retourne la réponse
            return Answer;
        }

        /// <summary>
        /// Sert à montrer les scores du highscore
        /// </summary>
        /// <returns></returns>
        public string[] showHighscoreScore()
        {
            // Requête SQL
            string cmdSql = "SELECT jouNombrePoints FROM t_joueur order by jouNombrePoints DESC limit 10";
            string table1 = "jouNombrePoints";

            // tableau qui va recevoir les réponses
            string[] Answer = new string[10];
            Answer = ConnectionToDBReading(cmdSql, table1);

            // Retourne la réponse
            return Answer;
        }
    }
}
