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
    /// Logique d'interaction pour Highscore.xaml
    /// </summary>
    public partial class Highscore : Window
    {
        public Highscore()
        {
            InitializeComponent();

            showHighscore();
        }

        private void showHighscore()
        {
            database db = new database();

            string[] tab_highscore = new string[10];
            tab_highscore = db.ShowHighscore();

            for (int i = 0; i < tab_highscore.Length / 2; i++)
            {
                Placement.Content = tab_highscore[i] + "            " + tab_highscore[i + 5];
            }
        }

        private void KeyisDown(object sender, KeyEventArgs e)
        {
        }
        private void KeyisUp(object sender, KeyEventArgs e)
        {
        }
    }
}
