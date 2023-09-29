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
        int enemySpeed = 6;                 // Vitesse des vaisseaux ennemis
        bool gameOver = false;              // Bool pour permettre de faire une boucle pour jouer
        int PlayerSpeed = 20;               // Vitesse du joueur
        bool ennemyRight = false;           // Permet de savoir si l'ennemi va vers la droite ou la gauche
        //int EnemyAltitude = 80;             // Montre l'altitude du vaisseau le plus bas

        DispatcherTimer gameTimer = new DispatcherTimer();
        ImageBrush playerSkin = new ImageBrush();

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
            playerSkin.ImageSource = new BitmapImage(new Uri("C:/Users/pd57mgs/Documents/GitHub/SpicyInvaders/SpicyInvadersWPF/Images/player.png"));
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
            Rect playerHitBox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);

            ennemiesLeft.Content = "Enemies Left : " + Totalenemies;


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


            BulletTimer -= 3;

            if (BulletTimer < 0)
            {
                EnnemyBulletMaker(Canvas.GetLeft(Player) + 20, 10);

                BulletTimer = BulletTimerLimit;
            }

            foreach (Rectangle x in myCanvas.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    if (Canvas.GetTop(x) < 10)
                    {
                        itemsToRemove.Add(x);
                    }

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
                            }
                        }

                        if (y is Rectangle && (string)y.Tag == "enemyBullet")
                        {
                            Rect bulletHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);

                            if (bullet.IntersectsWith(bulletHit))
                            {
                                itemsToRemove.Add(x);
                                itemsToRemove.Add(y);
                            }
                        }
                    }
                }

                // Mouvement des vaisseaux ennemis
                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) + enemySpeed);

                    if (Canvas.GetLeft(x) > Width - 100)
                    {
                        Canvas.SetLeft(x, -60);

                        Canvas.SetTop(x, Canvas.GetTop(x) + (x.Height + 10));


                    }

                    Rect enemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox.IntersectsWith(enemyHitBox))
                    {
                        showGameOver("You were killed by the invaders !!");
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
                        showGameOver("You were Killed by the invader's bullet !!");
                    }


                }
            }


            foreach (Rectangle i in itemsToRemove)
            {
                myCanvas.Children.Remove(i);
            }

            if (Totalenemies < 10)
            {
                enemySpeed = 12;
            }

            if (Totalenemies < 1)
            {
                showGameOver("You win, you saved the world !!");
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

                Canvas.SetTop(newEnemy, enemyRow * 60 + 30);
                left = 70 * enemyCompteur;
                Canvas.SetLeft(newEnemy, left);

                myCanvas.Children.Add(newEnemy);


                if (enemyCompteur == CONST_INT_ENNEMIES)
                {
                    enemyCompteur = 1;
                    enemyRow++;
                }

                enemyCompteur++;

                switch (enemyRow)
                {
                    case 0:
                        enemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pd57mgs/Documents/GitHub/SpicyInvaders/SpicyInvadersWPF/Images/invader1.gif"));
                        break;

                    case 1:
                        enemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pd57mgs/Documents/GitHub/SpicyInvaders/SpicyInvadersWPF/Images/invader2.gif"));
                        break;

                    case 2:
                        enemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pd57mgs/Documents/GitHub/SpicyInvaders/SpicyInvadersWPF/Images/invader3.gif"));
                        break;

                    case 3:
                        enemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pd57mgs/Documents/GitHub/SpicyInvaders/SpicyInvadersWPF/Images/invader4.gif"));
                        break;

                    case 4:
                        enemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pd57mgs/Documents/GitHub/SpicyInvaders/SpicyInvadersWPF/Images/invader5.gif"));
                        break;

                    case 5:
                        enemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pd57mgs/Documents/GitHub/SpicyInvaders/SpicyInvadersWPF/Images/invader6.gif"));
                        break;

                    case 6:
                        enemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pd57mgs/Documents/GitHub/SpicyInvaders/SpicyInvadersWPF/Images/invader7.gif"));
                        break;

                    case 7:
                        enemySkin.ImageSource = new BitmapImage(new Uri("C:/Users/pd57mgs/Documents/GitHub/SpicyInvaders/SpicyInvadersWPF/Images/invader8.gif"));
                        break;
                }

            }
        }

        private void showGameOver(string msg)
        {
            gameOver = true;
            gameTimer.Stop();

            ennemiesLeft.Content += " " + msg + " Press Enter to play again";

        }


    }
}

