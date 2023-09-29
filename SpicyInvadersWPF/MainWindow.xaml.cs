﻿using System;
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

namespace SpicyInvaders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Déclaration des constantes
        const int CONST_INT_ENNEMIES = 8;                  // Nbr d'ennemis par ligne
        const int CONST_INT_NBR_ENNMIES_DIFF = 8;           // nbr d'ennemis différents
        const int CONST_INT_COOLDOWN_TIME = 20;             // Cooldown pour éviter les spams des balles

        // Déclaration des bool qui vont permettre de bouger
        bool goLeft;                        // Bool qui permettra d'aller à gauche
        bool goRight;                       // Bool qui permettra d'aller à droite
        bool goUp;                          // Bool qui permettra d'aller vers le haut
        bool goDown;                        // Bool qui permettra d'aller vers le bas

        List<Rectangle> itemsToRemove = new List<Rectangle>();
        // Déclaration des variables
        int enemyCompteur = 0;              // Compteur qui contera combien il y a de vaisseaux par ligne
        int enemyRow = 0;                   // Compteur qui dira à quel ligne les ennemis vont spawn
        int BulletTimer = 0;                // Int qui permettra que les ennemies auront un cooldown pour tirer
        int BulletTimerLimit = 90;          // Timer pour les balles ennemies
        int Totalenemies = 0;               // Nombre total d'ennemis présents
        int enemySpeed = 4;                 // Vitesse des vaisseaux ennemis
        int enemySpeedY = 25;
        double Boost = 1;
        bool gameOver = false;              // Bool pour permettre de faire une boucle pour jouer
        int PlayerSpeed = 20;               // Vitesse du joueur
        bool isGoingRight = true;           // Permet de savoir si l'ennemi va vers la droite ou la gauche
        bool isGoingDown = false;
        int NumberBullets = 30;             // Nbr de balles qu'on peut tirer sans cooldowns
        double Cooldown = CONST_INT_COOLDOWN_TIME;
        bool isRestarting = false;
        int intScore = 0;                      // Compte le score
        int ValeurMort = 40;                // Valeur maximale d'une mort

        DispatcherTimer gameTimer = new DispatcherTimer();      // pour faire le timer du jeu
        ImageBrush playerSkin = new ImageBrush();               // pour le skin du joueur

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


            // Vaisseau du joueur
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = System.IO.Path.Combine(basePath, "Images/player.png");

            playerSkin.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            Player.Fill = playerSkin;

            // Permet de tout mettre dans l'écran
            myCanvas.Focus();

            // Crée des ennemies avec un nbr limité
            makeEnnemies(CONST_INT_ENNEMIES * CONST_INT_NBR_ENNMIES_DIFF);
        }

        /// <summary>
        /// Permet de faire la loop pour jouer au jeu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Enregistre la touche qui a été touchée</param>
        private void GameLoop(object sender, EventArgs e)
        {
            // Hitbox du joueur
            Rect playerHitBox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);

            // Labels de la page XAML
            Score.Content = "Score : " + intScore;
            bulletLeft.Content = "Bullet Left : " + NumberBullets;

            // Mouvement du joueur
            if (goLeft == true && Canvas.GetLeft(Player) > 10)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) - PlayerSpeed);
            }

            else if (goRight == true && Canvas.GetLeft(Player) + 80 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) + PlayerSpeed);
            }

            else if (goUp == true && Canvas.GetTop(Player) > 10)
            {
                Canvas.SetTop(Player, Canvas.GetTop(Player) - PlayerSpeed);
            }

            else if (goDown == true && Canvas.GetTop(Player) + 110 < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(Player, Canvas.GetTop(Player) + PlayerSpeed);
            }

            // Gain d'une balle après un cooldown
            Cooldown--;
            if (Cooldown == 0)
            {
                NumberBullets++;
                Cooldown = CONST_INT_COOLDOWN_TIME;
            }


            // Création des balles enemies
            BulletTimer = BulletTimer - 3;

            if (BulletTimer < 0)
            {
                EnnemyBulletMaker(Canvas.GetLeft(Player) + 20, 10);

                BulletTimer = BulletTimerLimit;
            }


            // Forach qui regroupe : Ennemis / Balles / Hitbox / Mort et suppression des objets
            foreach (Rectangle x in myCanvas.Children.OfType<Rectangle>())
            {
                // Création de la balle 
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    // Position et vitesse de la balle
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    if (Canvas.GetTop(x) < 10)
                    {
                        itemsToRemove.Add(x);

                        if(ValeurMort >= 10)
                        {
                            ValeurMort--;
                        }
                        
                        
                    }

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
                                Totalenemies--;
                                intScore += ValeurMort;
                            }
                        }
                    }
                }

                // Mouvement des vaisseaux ennemis
                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    if (isGoingRight)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + enemySpeed * Boost);
                    }
                    else if (!isGoingRight)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - enemySpeed * Boost);
                    }

                    if (isGoingDown)
                    {
                        foreach (Rectangle y in myCanvas.Children.OfType<Rectangle>())
                        {
                            if (y is Rectangle && (string)y.Tag == "enemy")
                            {
                                Canvas.SetTop(y, Canvas.GetTop(y) + enemySpeedY * Boost);
                            }

                        }
                        isGoingDown = false;
                    }

                    if (Canvas.GetLeft(x) == Application.Current.MainWindow.Width - 200)
                    {
                        isGoingDown = true;
                        isGoingRight = false;
                    }

                    if (Canvas.GetLeft(x) == 200)
                    {
                        isGoingDown = true;
                        isGoingRight = true;
                    }

                    if(Canvas.GetTop(x) == Height - 100)
                    {
                        showGameOverLose("The invaders invaded earth");
                    }

                    Rect enemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyHitBox))
                    {
                        showGameOverLose("You were killed by the invaders !!");
                    }
                }

                if (x is Rectangle && (string)x.Tag == "enemyBullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + 10);

                    if (Canvas.GetTop(x) > Height)
                    {
                        itemsToRemove.Add(x);
                    }

                    Rect enemyBulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

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
            if (Totalenemies < (CONST_INT_ENNEMIES * CONST_INT_NBR_ENNMIES_DIFF) / 2)
            {
                Boost = 1.5;
            }
            else if (Totalenemies < ((CONST_INT_ENNEMIES * CONST_INT_NBR_ENNMIES_DIFF) / 4) * 3)
            {
                Boost = 2;
            }
            else if (Totalenemies < ((CONST_INT_ENNEMIES * CONST_INT_NBR_ENNMIES_DIFF) / 8) * 7)
            {
                Boost = 2;
            }

            if (Totalenemies < 1)
            {
                showGameOverLose("You win, you saved the world !!");
            }

        }

        private void KeyisDown(object sender, KeyEventArgs e)
        {
            // permet d'aller à droite ou à gauche selon les touches
            if (e.Key == Key.Left || e.Key == Key.A)
            {
                goLeft = true;
            }
            if (e.Key == Key.Right || e.Key == Key.D)
            {
                goRight = true;
            }
            if (e.Key == Key.Up || e.Key == Key.W)
            {
                goUp = true;
            }
            if (e.Key == Key.Down || e.Key == Key.S)
            {
                goDown = true;
            }
        }

        private void KeyisUp(object sender, KeyEventArgs e)
        {
            // permet d'arrêter le vaisseau d'aller à droite ou à gauche quand on lève la touche
            if (e.Key == Key.Left || e.Key == Key.A)
            {
                goLeft = false;
            }
            else if (e.Key == Key.Right || e.Key == Key.D)
            {
                goRight = false;
            }
            else if (e.Key == Key.Up || e.Key == Key.W)
            {
                goUp = false;
            }
            else if (e.Key == Key.Down || e.Key == Key.S)
            {
                goDown = false;
            }


            else if (e.Key == Key.Space)
            {
                if (NumberBullets == 0)
                {

                }
                else
                {
                    Rectangle newBullet = new Rectangle
                    {
                        Tag = "bullet",
                        Height = 20,
                        Width = 5,
                        Fill = Brushes.White,
                        Stroke = Brushes.Red
                    };

                    Canvas.SetTop(newBullet, Canvas.GetTop(Player) - newBullet.Height);
                    Canvas.SetLeft(newBullet, Canvas.GetLeft(Player) + Player.Width / 2);

                    myCanvas.Children.Add(newBullet);
                    NumberBullets--;
                }
            }
            else if (e.Key == Key.Escape)
            {

            }

        }

        private void EnnemyBulletMaker(double x, double y)
        {

            Rectangle enemyBullet = new Rectangle
            {
                Tag = "enemyBullet",
                Height = 40,
                Width = 15,
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black,
                StrokeThickness = 5,
            };

            Canvas.SetTop(enemyBullet, y);
            Canvas.SetLeft(enemyBullet, x);

            myCanvas.Children.Add(enemyBullet);
        }

        private void makeEnnemies(int limit)
        {
            int left = 100;
            
            Totalenemies = limit;


            for (int i = 0; i < limit; i++)
            {
                ImageBrush enemySkin = new ImageBrush();

                Rectangle newEnemy = new Rectangle
                {
                    Tag = "enemy",
                    Height = 45,
                    Width = 45,
                    Fill = enemySkin
                };

                

                myCanvas.Children.Add(newEnemy);

                // Faire le retour à la ligne
                enemyCompteur++;
                if (enemyCompteur - 1 == CONST_INT_ENNEMIES)
                {
                    enemyCompteur = 1;
                    enemyRow++;
                }

                Canvas.SetTop(newEnemy, enemyRow * 60 + 30);
                left = 85 * enemyCompteur;
                Canvas.SetLeft(newEnemy, left);


                // Mettre l'image de l'invader
                switch (enemyRow)
                {
                    case 0:
                        string basePath = AppDomain.CurrentDomain.BaseDirectory;
                        string imagePath = System.IO.Path.Combine(basePath, "Images/invader1.gif");

                        enemySkin.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                        break;

                    case 1:
                        string basePath1 = AppDomain.CurrentDomain.BaseDirectory;
                        string imagePath1 = System.IO.Path.Combine(basePath1, "Images/invader2.gif");

                        enemySkin.ImageSource = new BitmapImage(new Uri(imagePath1, UriKind.RelativeOrAbsolute));
                        break;

                    case 2:
                        string basePath2 = AppDomain.CurrentDomain.BaseDirectory;
                        string imagePath2 = System.IO.Path.Combine(basePath2, "Images/invader3.gif");

                        enemySkin.ImageSource = new BitmapImage(new Uri(imagePath2, UriKind.RelativeOrAbsolute));
                        break;

                    case 3:
                        string basePath3 = AppDomain.CurrentDomain.BaseDirectory;
                        string imagePath3 = System.IO.Path.Combine(basePath3, "Images/invader4.gif");

                        enemySkin.ImageSource = new BitmapImage(new Uri(imagePath3, UriKind.RelativeOrAbsolute));
                        break;

                    case 4:
                        string basePath4 = AppDomain.CurrentDomain.BaseDirectory;
                        string imagePath4 = System.IO.Path.Combine(basePath4, "Images/invader5.gif");

                        enemySkin.ImageSource = new BitmapImage(new Uri(imagePath4, UriKind.RelativeOrAbsolute));
                        break;

                    case 5:
                        string basePath5 = AppDomain.CurrentDomain.BaseDirectory;
                        string imagePath5 = System.IO.Path.Combine(basePath5, "Images/invader6.gif");

                        enemySkin.ImageSource = new BitmapImage(new Uri(imagePath5, UriKind.RelativeOrAbsolute));
                        break;

                    case 6:
                        string basePath6 = AppDomain.CurrentDomain.BaseDirectory;
                        string imagePath6 = System.IO.Path.Combine(basePath6, "Images/invader7.gif");

                        enemySkin.ImageSource = new BitmapImage(new Uri(imagePath6, UriKind.RelativeOrAbsolute));
                        break;

                    case 7:
                        string basePath7 = AppDomain.CurrentDomain.BaseDirectory;
                        string imagePath7 = System.IO.Path.Combine(basePath7, "Images/invader8.gif");

                        enemySkin.ImageSource = new BitmapImage(new Uri(imagePath7, UriKind.RelativeOrAbsolute));
                        break;
                }

            }
        }

        private void showGameOverLose(string msg)
        {
            gameOver = true;
            gameTimer.Stop();

            //ennemiesLeft.Content += " " + msg + " Press Enter to play again";
        }

        private void showGameOverWin(string msg)
        {
            gameOver = true;
            gameTimer.Stop();

        }

    }
}