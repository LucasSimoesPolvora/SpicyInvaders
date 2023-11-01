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
    /// Logique d'interaction pour MettrePseudo.xaml
    /// </summary>
    public partial class MettrePseudo : Window
    {
        config config = new config();

        /// <summary>
        /// Utilise la fenêtre
        /// </summary>
        public MettrePseudo()
        {
            InitializeComponent();

            // Paramètres de l'écran
            Left = config.CONST_INT_LEFT_OF_THE_SCREEN;
            Top = config.CONST_INT_TOP_OF_THE_SCREEN;
            Width = config.WidthOfTheScreen;
            Height = config.HeightOfTheScreen;
        }

        /// <summary>
        /// Si on clique sur le bouton on enregistre le score sur la db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            database db = new database();
            string msg;
            string score = "0";

            // Montre si le score a été enregistré ou s'il y a eu une erreur
            msg = db.WriteScore(textePseudo.Text, score);
            MessageBox.Show(msg);

            // fermeture de la fenêtre et ouverture du menu
            Menu window = new Menu();
            window.Show();
            this.Close();
        }
    }
}
