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
        config config = new config();
        database db = new database();

        public Highscore()
        {
            InitializeComponent();

            Left = config.CONST_INT_LEFT_OF_THE_SCREEN;
            Top = config.CONST_INT_TOP_OF_THE_SCREEN;
            Width = config.WidthOfTheScreen;
            Height = config.HeightOfTheScreen;

            string msg = db.tryConnection();

            if(msg == "1")
            {
                showHighscore();
            }
            else
            {
                MessageBox.Show(msg);
                Menu window = new Menu();
                this.Close();
                window.Show();
            }
        }

        private void showHighscore()
        {
            string[] tab_highscoreName = new string[10];
            string[] tab_highscoreScore = new string[10];
            tab_highscoreName = db.ShowHighscoreNames();
            tab_highscoreScore = db.showHighscoreScore();

            Placement1.Content = "1. " + tab_highscoreName[0];

            Placement2.Content = "2. " + tab_highscoreName[1];

            Placement3.Content = "3. " + tab_highscoreName[2];

            Placement4.Content = "4. " + tab_highscoreName[3];

            Placement5.Content = "5. " + tab_highscoreName[4];

            Placement6.Content = "6. " + tab_highscoreName[5];

            Placement7.Content = "7. " + tab_highscoreName[6];

            Placement8.Content = "8. " + tab_highscoreName[7];

            Placement9.Content = "9. " + tab_highscoreName[8];

            Placement10.Content = "10. " + tab_highscoreName[9];

            Label1.Content = tab_highscoreScore[0];
                                                  
            Label2.Content = tab_highscoreScore[1];
                                                  
            Label3.Content = tab_highscoreScore[2];
                                                  
            Label4.Content = tab_highscoreScore[3];
                                                  
            Label5.Content = tab_highscoreScore[4];
                                                  
            Label6.Content = tab_highscoreScore[5];
                                                  
            Label7.Content = tab_highscoreScore[6];
                                                  
            Label8.Content = tab_highscoreScore[7];
                                                  
            Label9.Content = tab_highscoreScore[8];
                                                  
            Label10.Content = tab_highscoreScore[9];
        }

        private void KeyisDown(object sender, KeyEventArgs e)
        {
        }
        private void KeyisUp(object sender, KeyEventArgs e)
        {
        }
    }
}
