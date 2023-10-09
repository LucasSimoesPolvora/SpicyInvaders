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

namespace Model
{
    public class player
    {
        bool goLeft;
        bool goRight;
        bool goUp;
        bool goDown;
        public int PlayerSpeed = 20;               // Vitesse du joueur
        static ImageBrush playerSkin = new ImageBrush();               // pour le skin du joueur
        Rectangle PlayerCaracter = new Rectangle
        {
            Tag = "Player",
            Height = 65,
            Width = 65,
            Fill = playerSkin
        };
        public void Display(Rectangle Playercaracter) 
        {
            ImageBrush playerSkin = new ImageBrush();               // pour le skin du joueur
            
            // Vaisseau du joueur
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = System.IO.Path.Combine(basePath, "Images/player.png");

            playerSkin.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            Playercaracter.Fill = playerSkin;
        }

        public void MovementAction()
        {
            // Mouvement du joueur
            if (goLeft == true && Canvas.GetLeft(PlayerCaracter) > 10)
            {
                Canvas.SetLeft(PlayerCaracter, Canvas.GetLeft(PlayerCaracter) - PlayerSpeed);
            }

            else if (goRight == true && Canvas.GetLeft(PlayerCaracter) + 80 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(PlayerCaracter, Canvas.GetLeft(PlayerCaracter) + PlayerSpeed);
            }

            else if (goUp == true && Canvas.GetTop(PlayerCaracter) > 10)
            {
                Canvas.SetTop(PlayerCaracter, Canvas.GetTop(PlayerCaracter) - PlayerSpeed);
            }

            else if (goDown == true && Canvas.GetTop(PlayerCaracter) + 110 < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(PlayerCaracter, Canvas.GetTop(PlayerCaracter) + PlayerSpeed);
            }
        }

        public void MovementOn(object sender, KeyEventArgs e, Canvas myCanvas)
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

        public void MovementOff(object sender, KeyEventArgs e, Canvas myCanvas)
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


            /*else if (e.Key == Key.Space)
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

                    Canvas.SetTop(newBullet, Canvas.GetTop(PlayerCaracter) - newBullet.Height);
                    Canvas.SetLeft(newBullet, Canvas.GetLeft(PlayerCaracter) + PlayerCaracter.Width / 2);

                    myCanvas.Children.Add(newBullet);
                    bullet.NumberBullets--;
                }
            }*/
        }
        public void Colider()
        {
            // Hitbox du joueur
            Rect playerHitBox = new Rect(Canvas.GetLeft(PlayerCaracter), Canvas.GetTop(PlayerCaracter), PlayerCaracter.Width, PlayerCaracter.Height);
        }
        public void Update()
        {

        }
    }
}
