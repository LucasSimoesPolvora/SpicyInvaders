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
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        config config = new config();

        int valueNiveau;
        int valueScoreTotal;

        public GameOver(int score, int ennemisRestants, int niveau)
        {
            InitializeComponent();

            Left = config.CONST_INT_LEFT_OF_THE_SCREEN;
            Top = config.CONST_INT_TOP_OF_THE_SCREEN;
            Width = config.WidthOfTheScreen;
            Height = config.HeightOfTheScreen;

            valueNiveau = (score - ennemisRestants) * (niveau / 10);
            valueScoreTotal = score - ennemisRestants + valueNiveau;


            scoreJoueur.Content = score;
            ennemisRestantsJoueur.Content = ennemisRestants;
            niveauJoueur.Content = valueNiveau;

            scoreTotalJoueur.Content = valueScoreTotal;

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            /*
            Menu mainWindow = new Menu();
            Visibility = Visibility.Hidden;
            mainWindow.Visibility = Visibility.Visible;*/
        }
    }
}
