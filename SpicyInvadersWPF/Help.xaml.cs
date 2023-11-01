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
    /// Logique d'interaction pour Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        config config = new config();
        /// <summary>
        /// Page Help
        /// </summary>
        public Help()
        {
            InitializeComponent();

            // Paramètres de la page
            Left = config.CONST_INT_LEFT_OF_THE_SCREEN;
            Top = config.CONST_INT_TOP_OF_THE_SCREEN;
            Width = config.WidthOfTheScreen;
            Height = config.HeightOfTheScreen;
        }
        /// <summary>
        /// Lorsqu'on clique sur le bouton on revient au menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Affiche le menu et ferme la fenêtre HELP
            Menu window = new Menu();
            this.Close();
            window.Show();
        }
    }
}
