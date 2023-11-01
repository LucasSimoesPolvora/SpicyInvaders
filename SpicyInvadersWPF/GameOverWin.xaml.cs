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
    /// Interaction logic for GameOverWin.xaml
    /// </summary>
    public partial class GameOverWin : Window
    {
        config config = new config();
        score score = new score();
        enemy enemy = new enemy();

        double scoreTot;            //Variable pour connaître le score final
        public GameOverWin(int ennemisRestants, int finalScore)
        {
            InitializeComponent();

            // Config de l'écran
            Left = config.CONST_INT_LEFT_OF_THE_SCREEN;
            Top = config.CONST_INT_TOP_OF_THE_SCREEN;
            Width = config.WidthOfTheScreen;
            Height = config.HeightOfTheScreen;

            // Mettre en place tous les canvas
            Canvas.SetLeft(title, config.WidthOfTheScreen / 2 - 270);
            Canvas.SetTop(title, 20);

            Canvas.SetLeft(Score, config.WidthOfTheScreen / 4);
            Canvas.SetTop(Score, 200);

            Canvas.SetLeft(ennemiRestant, config.WidthOfTheScreen / 4 - 290);
            Canvas.SetTop(ennemiRestant, 350);

            Canvas.SetLeft(trait, config.WidthOfTheScreen / 4 - 200);
            Canvas.SetTop(trait, 600);

            Canvas.SetLeft(scoreTotal, config.WidthOfTheScreen / 4 - 100);
            Canvas.SetTop(scoreTotal, 700);

            Canvas.SetLeft(scoreJoueur, config.WidthOfTheScreen / 2);
            Canvas.SetTop(scoreJoueur, 200);

            Canvas.SetLeft(ennemisRestantsJoueur, config.WidthOfTheScreen / 2);
            Canvas.SetTop(ennemisRestantsJoueur, 350);

            Canvas.SetLeft(scoreTotalJoueur, config.WidthOfTheScreen / 2);
            Canvas.SetTop(scoreTotalJoueur, 700);

            // Calculs pour le score
            scoreTot = (finalScore - ennemisRestants * 10);

            scoreJoueur.Content = finalScore;
            ennemisRestantsJoueur.Content = ennemisRestants * 10;
            scoreTotalJoueur.Content = scoreTot;
            
            
        }

        /// <summary>
        /// Action lorsqu'on clicke sur le bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinueWin_Click(object sender, RoutedEventArgs e)
        {
            // Va au jeu
            MainWindow window = new MainWindow();
            this.Close();
            window.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Action lorsqu'on clicke sur le bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturnLose_Click(object sender, RoutedEventArgs e)
        {
            // Va sur la page pour mettre son pseudo
            MettrePseudo window = new MettrePseudo();
            this.Close();
            window.Show();
        }
    }
}
