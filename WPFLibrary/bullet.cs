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
    public class bullet
    {
        int BulletTimer = 0;                // Int qui permettra que les ennemies auront un cooldown pour tirer
        int BulletTimerLimit = 90;          // Timer pour les balles ennemies
        double Cooldown = config.CONST_INT_COOLDOWN_TIME;
        public int NumberBullets = 30;             // Nbr de balles qu'on peut tirer sans cooldowns
        List<Rectangle> itemsToRemove = new List<Rectangle>();
        score score = new score();
        enemy enemy = new enemy();

        /// <summary>
        /// Fait apparaître les balles ennemis sur la fenêtre
        /// </summary>
        /// <param name="x">Distance sur l'axe X</param>
        /// <param name="y">Distance sur l'axe Y</param>
        /// <param name="myCanvas">Permet d'avoir accès aux canvas crée sur le fichier xaml pour crée des balles qui peuvent tuer le joueur</param>
        public void ennemyBulletMaker(double x, double y, Canvas myCanvas)
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

        /// <summary>
        /// Fait un cooldown aux balles ennemies
        /// </summary>
        /// <param name="myCanvas"></param>
        /// <param name="Player"></param>
        public void enemyBulletCooldown(Canvas myCanvas, Rectangle Player)
        {
            BulletTimer = BulletTimer - 3;

            if (BulletTimer < 0)
            {
                ennemyBulletMaker(Canvas.GetLeft(Player) + 20, 10, myCanvas);

                BulletTimer = BulletTimerLimit;
            }
        }

        /// <summary>
        /// Fait bouger les balles ennemies
        /// </summary>
        public void enemyBulletMovement()
        {

        }

        /// <summary>
        ///  fait un cooldown aux balles ennemies
        /// </summary>
        public void playerBulletCooldown()
        {
            Cooldown--;
            if (Cooldown == 0)
            {
                NumberBullets++;
                Cooldown = config.CONST_INT_COOLDOWN_TIME;
            }
        }

        /// <summary>
        /// Crée les balles du joueur
        /// </summary>
        /// <param name="e">Touche que le joueur a cliqué</param>
        /// <param name="myCanvas">Permet d'avoir accès aux canvas du fichier xaml afin de créer des balles qui peuvent tuer les ennemis</param>
        /// <param name="Player">Rectangle qui reprèsente le joueur</param>
        public void playerBulletMaker(KeyEventArgs e, Canvas myCanvas, Rectangle Player)
        {
            if (e.Key == Key.Space)
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

        /// <summary>
        /// Fait bouger les balles du joueur dans l'axe Vertical
        /// </summary>
        /// <param name="myCanvas">Permet d'avoir accès aux Canvas du fichier xaml pour faire bouger les balles existantes</param>
        public void playerBulletMovement(Canvas myCanvas)
        {
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

                        if (score.MaxDeadValue >= 10)
                        {
                            score.MaxDeadValue--;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Permet de faire l'update de toutes les balles
        /// </summary>
        public void update(Canvas myCanvas)
        {
            playerBulletMovement(myCanvas);
            enemyBulletMovement();
        }
    }
}

