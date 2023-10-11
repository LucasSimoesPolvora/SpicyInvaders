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
        double Boost = 1;                   // permet d'accélérer le jeu
      
        DispatcherTimer gameTimer = new DispatcherTimer();      // pour faire le timer du jeu

        /// <summary>
        /// Utilise la fenêtre
        /// </summary>
        public MainWindow()
        {
            // Initialise le programme
            InitializeComponent();

            // Fait le height de mainwindow
            Application.Current.MainWindow.Height = System.Windows.SystemParameters.PrimaryScreenHeight - 200;

            // Timer du jeu
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(30);

            // Démarre le timer
            gameTimer.Start();

            // Permet de tout mettre dans l'écran
            myCanvas.Focus();



            // Crée des ennemies avec un nbr limité
            enemy.display(myCanvas);

            // Affiche le joueur sur la fenêtre
            player.display(Player);
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
                        showGameOverLose("You were killed by the invaders !!");
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
                        showGameOverLose("You were Killed by the invader's bullet !!");
                    }
                }
            }

            // Faire disparaitre les objets "morts"
            foreach (Rectangle i in itemsToRemove)
            {
                myCanvas.Children.Remove(i);
            }

            // Vitesse des ennemies selon le nombre d'ennemis restants
            if (enemy.Totalenemies < (enemy.CONST_INT_ENNEMIES * enemy.CONST_INT_NBR_ENNMIES_DIFF) / 2)
            {
                Boost = 1.5;
            }
            else if (enemy.Totalenemies < ((enemy.CONST_INT_ENNEMIES * enemy.CONST_INT_NBR_ENNMIES_DIFF) / 4) * 3)
            {
                Boost = 2;
            }
            else if (enemy.Totalenemies < ((enemy.CONST_INT_ENNEMIES * enemy.CONST_INT_NBR_ENNMIES_DIFF) / 8) * 7)
            {
                Boost = 2;
            }

            if (enemy.Totalenemies < 1)
            {
                showGameOverLose("You win, you saved the world !!");
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


        /// <summary>
        /// a retirer
        /// </summary>
        /// <param name="msg"></param>
        private void showGameOverLose(string msg)
        {

            gameTimer.Stop();

            //ennemiesLeft.Content += " " + msg + " Press Enter to play again";
        }

        /// <summary>
        /// a retirer
        /// </summary>
        /// <param name="msg"></param>
        private void showGameOverWin(string msg)
        {
            gameTimer.Stop();

        }

    }
}