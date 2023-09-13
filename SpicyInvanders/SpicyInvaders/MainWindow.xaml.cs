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
        bool goLeft;                        // Bool qui permettra d'aller à gauche
        bool goRight;                       // Bool qui permettra d'aller à droite
        bool goUp;                          // Bool qui permettra d'aller vers le haut
        bool goDown;                        // Bool qui permettra d'aller vers le bas

        List<Rectangle> itemsToRemove = new List<Rectangle>();

        int enemyImages = 0;
        int BulletTimer = 0;
        int BulletTimerLimit = 90;
        int Totalenemies = 0;
        int enemySpeed = 6;
        bool gameOver = false;

        DispatcherTimer gameTimer = new DispatcherTimer();
        ImageBrush playerSkin = new ImageBrush();

        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(30);
            gameTimer.Start();

            //string[] filePaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "player.png", SearchOption.AllDirectories);

            
                playerSkin.ImageSource = new BitmapImage(new Uri("C:/Users/pd57mgs/Documents/GitHub/SpicyInvaders/SpicyInvanders/SpicyInvaders/Images/player.png"));
                Player.Fill = playerSkin;
            

            myCanvas.Focus();
        }
        private void GameLoop(object sender, EventArgs e)
        {
            if (goLeft == true && Canvas.GetLeft(Player) > 10)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) - 10);
            }

            else if (goRight == true && Canvas.GetLeft(Player) + 80 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(Player, Canvas.GetLeft(Player) + 10);
            }

            else if (goUp == true && Canvas.GetTop(Player) > 10)
            {
                Canvas.SetTop(Player, Canvas.GetTop(Player) - 10);
            }

            else if (goDown == true && Canvas.GetTop(Player) + 110 < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(Player, Canvas.GetTop(Player) + 10);
            }

            BulletTimer -= 3;

            if(BulletTimer < 0)
            {
                EnnemyBulletMaker(Canvas.GetLeft(Player) + 20, 10) ;

                BulletTimer = BulletTimerLimit;
            }
        }

        private void KeyisDown(object sender, KeyEventArgs e)
        {
            // permet d'aller à droite ou à gauche selon les touches
            if(e.Key == Key.Left || e.Key == Key.A)
            {
                goLeft = true;
            }
            if(e.Key == Key.Right || e.Key == Key.D)
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

            

            else if(e.Key == Key.Space)
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
                Canvas.SetLeft(newBullet,Canvas.GetLeft(Player) + Player.Width / 2);

                myCanvas.Children.Add(newBullet);
            }

        }

        private void EnnemyBulletMaker(double x, double y)
        {
            Rectangle enemyBullet = new Rectangle
            {
                Tag = "enemyBulet",
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

        }

        private void showGameOver(string msg)
        {

        }


    }
}
