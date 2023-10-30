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
        public bool goLeft;
        public bool goRight;
        public bool goUp;
        public bool goDown;
        public int PlayerSpeed = 20;               // Vitesse du joueur
        static ImageBrush playerSkin = new ImageBrush();               // pour le skin du joueur
        Rectangle PlayerCaracter = new Rectangle
        {
            Tag = "Player",
            Height = 65,
            Width = 65,
            Fill = playerSkin
        };

        /// <summary>
        /// Affiche le joueur
        /// </summary>
        /// <param name="Playercaracter"></param>
        public void display(Rectangle Playercaracter)
        {
            ImageBrush playerSkin = new ImageBrush();               // pour le skin du joueur

            // Vaisseau du joueur
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = System.IO.Path.Combine(basePath, "Images/player.png");

            playerSkin.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            Playercaracter.Fill = playerSkin;
        }

        /// <summary>
        /// est utilisé quand la touche est appuyée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="myCanvas"></param>
        public void movementOn(object sender, KeyEventArgs e, Canvas myCanvas)
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

        /// <summary>
        /// Est utilisée lorsque la touche est retirée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void movementOff(object sender, KeyEventArgs e)
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
        }

        /// <summary>
        /// Action du joueur
        /// </summary>
        /// <param name="Player"></param>
        public void movementAction(Rectangle Player)
        {
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
        }
     
        /// <summary>
        /// Fait un update du joueur
        /// </summary>
        /// <param name="Player"></param>
        public void update(Rectangle Player)
        {
            movementAction(Player);
        }
    }
}
