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

        public MettrePseudo()
        {
            InitializeComponent();
            Left = config.CONST_INT_LEFT_OF_THE_SCREEN;
            Top = config.CONST_INT_TOP_OF_THE_SCREEN;
            Width = config.WidthOfTheScreen;
            Height = config.HeightOfTheScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            database db = new database();
            string msg;
            string score = "0";

            msg = db.WriteScore(textePseudo.Text, score);
            MessageBox.Show(msg);
        }
    }
}
