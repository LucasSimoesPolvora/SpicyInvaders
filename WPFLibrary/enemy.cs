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
    public class enemy
    {
        public const int CONST_INT_ENNEMIES = 8;                   // Nbr d'ennemis par ligne
        public const int CONST_INT_NBR_ENNMIES_DIFF = 8;           // nbr d'ennemis différents

        public int enemyRow = 0;
        public int enemyCompteur = 0;
        public int enemySpeed = 4;                 // Vitesse des vaisseaux ennemis
        public int enemySpeedY = 25;
        public int limit = CONST_INT_ENNEMIES * CONST_INT_NBR_ENNMIES_DIFF;
        public int Totalenemies = CONST_INT_ENNEMIES * CONST_INT_NBR_ENNMIES_DIFF;
        public bool isGoingRight = true;
        public bool isGoingLeft = false;
        public bool isGoingDown = false;


        private ImageBrush enemySkin = new ImageBrush();


        /// <summary>
        /// Fait spawn les ennemis sur la fenêtre
        /// </summary>
        /// <param name="myCanvas">Rassemble tous les ennemis</param>
        public void display(Canvas myCanvas)
        {
            int left = 200;

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

                    if(enemyRow == CONST_INT_NBR_ENNMIES_DIFF - 1)
                    {
                        return;
                    }
                }

                Canvas.SetTop(newEnemy, enemyRow * 60 + 30);
                left = 85 * enemyCompteur;
                Canvas.SetLeft(newEnemy, left);

                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string imagePath = System.IO.Path.Combine(basePath, $"Images/invader{enemyRow + 1}.gif");

                enemySkin.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            }

        }

        /// <summary>
        ///  fait le mouvement des ennemis
        /// </summary>
        /// <param name="myCanvas"></param>
        public void movement(Canvas myCanvas)
        {
            foreach(Rectangle x in myCanvas.Children.OfType<Rectangle>())
            {
                if(x is Rectangle && (string)x.Tag == "enemy")
                {
                    if (isGoingRight)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + enemySpeed);
                    }
                    else if (!isGoingRight)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - enemySpeed);
                    }

                    if (isGoingDown)
                    {
                        foreach (Rectangle y in myCanvas.Children.OfType<Rectangle>())
                        {
                            if (y is Rectangle && (string)y.Tag == "enemy")
                            {
                                Canvas.SetTop(y, Canvas.GetTop(y) + enemySpeedY );
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

                    if (Canvas.GetTop(x) > Application.Current.MainWindow.Height - 200)
                    {
                        
                    }
                }
            }
            
        }

        /// <summary>
        /// Fait tous les updates des ennemis
        /// </summary>
        public void update(Canvas myCanvas)
        {
            movement(myCanvas);
        }
    }
}
