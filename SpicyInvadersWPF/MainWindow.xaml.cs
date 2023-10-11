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
        enemy enemy = new enemy();
        config config = new config();
        bullet bullet = new bullet();
        player player = new player();
        score score = new score();
        

        // Déclaration des bool qui vont permettre de bouger
        //bool goLeft;                        // Bool qui permettra d'aller à gauche
        //bool goRight;                       // Bool qui permettra d'aller à droite
        //bool goUp;                          // Bool qui permettra d'aller vers le haut
        //bool goDown;                        // Bool qui permettra d'aller vers le bas

        List<Rectangle> itemsToRemove = new List<Rectangle>();
        // Déclaration des variables
        //int enemyCompteur = 0;              // Compteur qui contera combien il y a de vaisseaux par ligne
        //int enemyRow = 0;                   // Compteur qui dira à quel ligne les ennemis vont spawn
        //int BulletTimer = 0;                // Int qui permettra que les ennemies auront un cooldown pour tirer
        //int BulletTimerLimit = 90;          // Timer pour les balles ennemies
        //int Totalenemies = 0;               // Nombre total d'ennemis présents
        //int enemySpeed = 4;                 // Vitesse des vaisseaux ennemis
        //int enemySpeedY = 25;
        double Boost = 1;
        bool gameOver = false;              // Bool pour permettre de faire une boucle pour jouer
        //int PlayerSpeed = 20;               // Vitesse du joueur
        bool isGoingRight = true;           // Permet de savoir si l'ennemi va vers la droite ou la gauche
        bool isGoingDown = false;
        //int NumberBullets = 30;             // Nbr de balles qu'on peut tirer sans cooldowns
        //double Cooldown = config.CONST_INT_COOLDOWN_TIME;
        bool isRestarting = false;
        //int intScore = 0;                   // Compte le score
        //int ValeurMort = 40;                // Valeur maximale d'une mort

        DispatcherTimer gameTimer = new DispatcherTimer();      // pour faire le timer du jeu
        //ImageBrush playerSkin = new ImageBrush();               // pour le skin du joueur

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
            enemy.makeEnnemies(myCanvas);
            player.Display(Player);
        }

        /// <summary>
        /// Permet de faire la loop pour jouer au jeu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Enregistre la touche qui a été touchée</param>
        private void GameLoop(object sender, EventArgs e)
        {
            // Hitbox du joueur
            //Rect playerHitBox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width, Player.Height);

            // Labels de la page XAML
            //Score.Content = "Score : " + intScore;
            bulletLeft.Content = "Bullet Left : " + bullet.NumberBullets;

            player.Update(Player);

            bullet.playerBulletCooldown();

            bullet.playerBulletMovement(myCanvas);
            /*
            // Gain d'une balle après un cooldown
            Cooldown--;
            if (Cooldown == 0)
            {
                NumberBullets++;
                Cooldown = config.CONST_INT_COOLDOWN_TIME;
            }*/


            // Création des balles enemies
            /*BulletTimer = BulletTimer - 3;

            if (BulletTimer < 0)
            {
                bullet.EnnemyBulletMaker(Canvas.GetLeft(Player) + 20, 10, myCanvas);

                BulletTimer = BulletTimerLimit;
            }*/


            // Forach qui regroupe : Ennemis / Balles / Hitbox / Mort et suppression des objets
            foreach (Rectangle x in myCanvas.Children.OfType<Rectangle>())
            {
                /*
                // Création de la balle 
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    // Position et vitesse de la balle
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);

                    if (Canvas.GetTop(x) < 10)
                    {
                        itemsToRemove.Add(x);

                        if(score.MaxDeadValue >= 10)
                        {
                            score.MaxDeadValue--;
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
                                enemy.Totalenemies--;
                                score.ScoreValue += score.MaxDeadValue;
                            }
                        }
                    }
                }*/

                // Mouvement des vaisseaux ennemis
                if (x is Rectangle && (string)x.Tag == "enemy")
                {
                    if (isGoingRight)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + enemy.enemySpeed * Boost);
                    }
                    else if (!isGoingRight)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - enemy.enemySpeed * Boost);
                    }

                    if (isGoingDown)
                    {
                        foreach (Rectangle y in myCanvas.Children.OfType<Rectangle>())
                        {
                            if (y is Rectangle && (string)y.Tag == "enemy")
                            {
                                Canvas.SetTop(y, Canvas.GetTop(y) + enemy.enemySpeedY * Boost);
                            }

                        }
                        isGoingDown = false;
                    }

                    if (Canvas.GetLeft(x) > Application.Current.MainWindow.Width - 100)
                    {
                        isGoingDown = true;
                        isGoingRight = false;
                    }

                    if (Canvas.GetLeft(x) < 10)
                    {
                        isGoingDown = true;
                        isGoingRight = true;
                    }

                    if(Canvas.GetTop(x) > Application.Current.MainWindow.Height - 200)
                    {
                        showGameOverLose("The invaders invaded earth");
                    }

                    Rect enemyHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    /*if (playerHitBox.IntersectsWith(enemyHitBox))
                    {
                        showGameOverLose("You were killed by the invaders !!");
                    }*/
                }

                if (x is Rectangle && (string)x.Tag == "enemyBullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + 10);

                    if (Canvas.GetTop(x) > Height)
                    {
                        itemsToRemove.Add(x);
                    }

                    Rect enemyBulletHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    /*if (playerHitBox.IntersectsWith(enemyBulletHitBox))
                    {
                        showGameOverLose("You were Killed by the invader's bullet !!");
                    }*/
                }
            }

            // Faire disparaitre les objets "morts"
            foreach (Rectangle i in itemsToRemove)
            {
                myCanvas.Children.Remove(i);
            }

            // Vitesse des ennemies selon le nombre d'ennemis restants
            if (enemy.Totalenemies < (config.CONST_INT_ENNEMIES * config.CONST_INT_NBR_ENNMIES_DIFF) / 2)
            {
                Boost = 1.5;
            }
            else if (enemy.Totalenemies < ((config.CONST_INT_ENNEMIES * config.CONST_INT_NBR_ENNMIES_DIFF) / 4) * 3)
            {
                Boost = 2;
            }
            else if (enemy.Totalenemies < ((config.CONST_INT_ENNEMIES * config.CONST_INT_NBR_ENNMIES_DIFF) / 8) * 7)
            {
                Boost = 2;
            }

            if (enemy.Totalenemies < 1)
            {
                showGameOverLose("You win, you saved the world !!");
            }
        }

        private void KeyisDown(object sender, KeyEventArgs e)
        {
            player.MovementOn(sender, e, myCanvas);
        }

        private void KeyisUp(object sender, KeyEventArgs e)
        {
            player.MovementOff(sender, e);
            bullet.playerBulletMaker(e, myCanvas, Player);
        }
        
        /*private void makeEnnemies(int limit)
        {
            int left = 200;

            enemy.Totalenemies = limit;


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
                enemy.enemyCompteur++;
                if (enemy.enemyCompteur - 1 == config.CONST_INT_ENNEMIES)
                {
                    enemy.enemyCompteur = 1;
                    enemy.enemyRow++;
                }

                Canvas.SetTop(newEnemy, enemy.enemyRow * 60 + 30);
                left = 85 * enemy.enemyCompteur;
                Canvas.SetLeft(newEnemy, left);


                // Mettre l'image de l'invader
                switch (enemy.enemyRow)
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
        }*/
        

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