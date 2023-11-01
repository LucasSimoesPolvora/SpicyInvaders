using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;

namespace SpicyInvadersWPF
{
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        config config = new config();
        database db = new database();

        /// <summary>
        /// Utilise la fenêtre
        /// </summary>
        public Menu()
        {
            InitializeComponent();

            // Configuration de la fenêtre
            Left = config.CONST_INT_LEFT_OF_THE_SCREEN;
            Top = config.CONST_INT_TOP_OF_THE_SCREEN;
            Width = config.WidthOfTheScreen;
            Height = config.HeightOfTheScreen;
        }

        /// <summary>
        /// Lance le jeu si on clicke le bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void play_Click(object sender, RoutedEventArgs e)
        {
            // fermeture du menu et ouverture de la fenêtre avec le jeu
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        /// <summary>
        /// Ouverture du highscore si on clicke sur le bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Highscore_Click(object sender, RoutedEventArgs e)
        {
            // Si la connexion est réussie on ouvre la page du highscore
            string answer = db.tryConnection();
            if(answer == "connection successful")
            {
                Highscore window = new Highscore();
                this.Close();
                window.Show();
            }
            else
            {
                MessageBox.Show("Erreur de base de donnée :" + answer);
            }
        }

        /// <summary>
        /// Ouverture de la fenêtre Help si on clique dessus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            // Fermeture du menu est ouverture de la fenêtre Help
            Help window = new Help();
            this.Close();
            window.Show();
        }

        /// <summary>
        /// Fermeture de l'application si on clique dessus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            // Fermeture de l'application
            System.Windows.Application.Current.Shutdown();
        }
    }
}
