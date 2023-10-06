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

        // Crée les ennemis
        public void Create(Canvas myCanvas)
        {
            for(int i = 0; i < limit; i++)
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
            }
        }

        public void Display()
        {

        }
        /// <summary>
        /// Permet de faire spawn les ennemis dans le programme
        /// </summary>
        /// <param name="limit">Marque la limite d'ennemis qu'il peut y avoir</param>
        /// <param name="myCanvas">Sert à afficher dans le programme avec des canvas</param>
        public void makeEnnemies(Canvas myCanvas)
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
                if (enemyCompteur - 1 == config.CONST_INT_ENNEMIES)
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

        public void Update()
        {

        }
    }
}
