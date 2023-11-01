using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Timers;
using Model;

namespace SpicyInvadersWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Connections des classes
        enemy enemy = new enemy();
        config config = new config();
        bullet bullet = new bullet();
        player player = new player();
        score score = new score();

        // Déclaration des lists
        List<Rectangle> itemsToRemove = new List<Rectangle>();                  // liste qui ferra disparaître les objets à supprimer

        // Déclaration des variabales
        public int ennemisRestants;
        public double scoreTot;

        DispatcherTimer gameTimer = new DispatcherTimer();      // pour faire le timer du jeu

        /// <summary>
        /// Utilise la fenêtre
        /// </summary>
        public MainWindow()
        {
            // Initialise le programme
            InitializeComponent();

            // Paramètres de l'écran
            Left = config.CONST_INT_LEFT_OF_THE_SCREEN;
            Top = config.CONST_INT_TOP_OF_THE_SCREEN;
            Width = config.WidthOfTheScreen;
            Height = config.HeightOfTheScreen;

            // Timer du jeu
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(30);

            // Démarre le timer
            gameTimer.Start();

            // Permet de faire un focus sur les canvas
            myCanvas.Focus();

            // Permet de faire les ennemis
            enemy.display(myCanvas);

            // Permet de faire le joueur
            player.display(Player);

            ennemisRestants = enemy.Totalenemies;

        }

        /// <summary>
        /// Permet de faire la loop pour jouer au jeu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Enregistre la touche qui a été touchée</param>
        private void GameLoop(object sender, EventArgs e)
        {

            //Hitbox du joueur
            Rect playerHitBox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);

            // Labels de la page XAML

            Score.Content = score.writeScore();
            bulletLeft.Content = "Bullet Left : " + bullet.NumberBullets;

            player.update(Player);
            enemy.update(myCanvas);

            bullet.update(myCanvas, Player);

            // Foreach qui regroupe : Ennemis / Balles / Hitbox / Mort et suppression des objets
            foreach (Rectangle x in myCanvas.Children.OfType<Rectangle>())
            {
                // Permet de choisir les rectangles avec un tag "bullet" 
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    // Si ca touche un ennemi l'ennemi disparait
                    Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    // Si la balle touche un ennemi l'ennemi disparait
                    foreach (Rectangle y in myCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                            if (bullet.IntersectsWith(enemyHit))
                            {
                                itemsToRemove.Add(x);
                                itemsToRemove.Add(y);
                                enemy.Totalenemies--;
                                ennemisRestants--;
                                if (ennemisRestants % 10 == 0) enemy.moreBoost();
                                score.update();
                            }
                        }
                    }
                }

                // Permet de choisir les rectangles avec un tag "enemy"
                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    // Hitbox de l'ennemi
                    Rect enemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    // Si l'hitbox de l'ennemi touche l0hitbox du joueur le joueur meurt
                    if (playerHitBox.IntersectsWith(enemyHitBox))
                    {
                        GameOverLose windowShow = new GameOverLose(ennemisRestants, score.ScoreValue);
                        this.Close();
                        windowShow.Show();
                        gameTimer.Stop();
                    }
                }

                // Permet de choisir tous les rectangles avec un tag "enemyBullet"
                if (x is Rectangle && (string)x.Tag == "enemyBullet")
                {
                    // Hitbox de la balle ennemie
                    Rect enemyBulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    // Si l'hitbox de la balle ennemie touche le joueur le joueur meurt
                    if (playerHitBox.IntersectsWith(enemyBulletHitBox))
                    {
                        GameOverLose windowShow = new GameOverLose(ennemisRestants, score.ScoreValue);
                        this.Close();
                        windowShow.Show();
                        gameTimer.Stop();
                    }
                }
            }

            // Faire disparaitre les objets "morts"
            foreach (Rectangle i in itemsToRemove)
            {
                myCanvas.Children.Remove(i);
            }

            // S'il n'y a plus d'ennemis le joueur gagne
            if (enemy.Totalenemies < 1)
            {
                GameOverWin windowShow = new GameOverWin(ennemisRestants, score.ScoreValue);
                this.Visibility = Visibility.Hidden;
                windowShow.Show();
                gameTimer.Stop();
            }

            // joueur perd car les ennemis sont en bas de l'écran
            if (enemy.gameOver)
            {
                GameOverLose windowShow = new GameOverLose(ennemisRestants, score.ScoreValue);
                this.Close();
                windowShow.Show();
                gameTimer.Stop();
            }
        }

        /// <summary>
        /// Est utilisé quand le joueur clique sur un bouton
        /// </summary>
        /// <param name="sender">ne fait rien (a été mis par défaut)</param>
        /// <param name="e">Stocke la touche cliquée</param>
        private void KeyisDown(object sender, KeyEventArgs e)
        {
            player.movementOn(sender, e, myCanvas);
        }

        /// <summary>
        /// Est utilisée quand un joueur relâche un bouton
        /// </summary>
        /// <param name="sender">ne fait rien (a été mis par défaut)</param>
        /// <param name="e">Stocke la touche relâchée</param>
        private void KeyisUp(object sender, KeyEventArgs e)
        {
            player.movementOff(sender, e);
            bullet.playerBulletMaker(e, myCanvas, Player);
        }
    }
}