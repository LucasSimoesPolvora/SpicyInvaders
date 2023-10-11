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
            Placement1.Content = "1. " + tab_highscore[0];

            Placement2.Content = "2. " + tab_highscore[1];

            Placement3.Content = "3. " + tab_highscore[2];

            Placement4.Content = "4. " + tab_highscore[3];

            Placement5.Content = "5. " + tab_highscore[4];

            Placement6.Content = "6. " + tab_highscore[5];

            Placement7.Content = "7. " + tab_highscore[6];

            Placement8.Content = "8. " + tab_highscore[7];

            Placement9.Content = "9. " + tab_highscore[8];

            Placement10.Content = "10. " + tab_highscore[9];


        }

        private void KeyisDown(object sender, KeyEventArgs e)
        {
        }
        private void KeyisUp(object sender, KeyEventArgs e)
        {
        }
    }
}
