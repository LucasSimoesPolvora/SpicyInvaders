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
        config config = new config();                                                                       // Appelle la classe config
        score score = new score();                                                                          // Appelle la classe score

        public int enemyRow = 0;                                                                            // int qui va compter a quel ligne les ennemis spawn
        public int enemyCompteur = 0;                                                                       // Compteur qui sers a display les ennemis
        public int enemySpeed = 8;                                                                          // Vitesse des vaisseaux ennemis
        public int enemySpeedY = 35;                                                                        // Vitesse de la vitesse varticale des vaisseaux ennemis
        public int limit = config.CONST_INT_ENNEMIES * config.CONST_INT_NBR_ENNMIES_DIFF;                   // Limite du nbr de vaisseaux qui peuvent spawn
        public int Totalenemies = config.CONST_INT_ENNEMIES * config.CONST_INT_NBR_ENNMIES_DIFF;            // Mëme chose que le limit mais celui ci va être diminuer a chaque fois qu'on tue les ennemis
        public int ennemiesKilled = 0;                                                                      // Compte le nbr d'ennemis tué
        public double boost = 1;                                                                            // boost pour les ennemis quand ils sont peu
        public bool isGoingRight = true;                                                                    // bool qui dira si l'ennemi va vers la droite
        public bool isGoingLeft = false;                                                                    // Bool qui dira si l'ennemi va vers la gauche
        public bool isGoingDown = false;                                                                    // Bool qui dira si l'ennemi descend
        public bool gameOver = false;                                                                       // Si les ennemis touchent le bas de l'écran le joueur perd

        private ImageBrush enemySkin = new ImageBrush();                                                    // Image de l'ennemi


        /// <summary>
        /// Fait spawn les ennemis sur la fenêtre
        /// </summary>
        /// <param name="myCanvas">Rassemble tous les ennemis</param>
        public void display(Canvas myCanvas)
        {
            // A partir d'où les ennemis vont apparaître
            int left = 200;

            // Reprend la valeur
            Totalenemies = limit;

            // Fait apparaître les ennemis avec une quantité limitée
            for (int i = 0; i < limit; i++)
            {
                // Image de l'ennemi
                ImageBrush enemySkin = new ImageBrush();

                // Propriétés de l'ennemi
                Rectangle newEnemy = new Rectangle
                {
                    Tag = "enemy",
                    Height = 55,
                    Width = 55,
                    Fill = enemySkin
                };

                // Ajout des ennemis aux canvas afin qu'ils apparaissent
                myCanvas.Children.Add(newEnemy);

                // Faire le retour à la ligne
                enemyCompteur++;
                if (enemyCompteur - 1 == config.CONST_INT_ENNEMIES)
                {
                    enemyCompteur = 1;
                    enemyRow++;

                    if(enemyRow == config.CONST_INT_NBR_ENNMIES_DIFF)
                    {
                        return;
                    }
                }

                // Endroit où ils vont apparaître
                Canvas.SetTop(newEnemy, enemyRow * 70 + 60);
                left = 80 * enemyCompteur;
                Canvas.SetLeft(newEnemy, left);

                // Mettre l'image des ennemis
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
            // Foreach et if qui sélectionne tous les ennemis
            foreach(Rectangle x in myCanvas.Children.OfType<Rectangle>())
            {
                if(x is Rectangle && (string)x.Tag == "enemy")
                {
                    // If pour la direction et mouvement dans les conditions
                    if (isGoingRight)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) + enemySpeed * boost);
                    }

                    else if (!isGoingRight)
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - enemySpeed * boost);
                    }

                    if (isGoingDown)
                    {
                        foreach (Rectangle y in myCanvas.Children.OfType<Rectangle>())
                        {
                            if (y is Rectangle && (string)y.Tag == "enemy")
                            {
                                Canvas.SetTop(y, Canvas.GetTop(y) + enemySpeedY * boost);
                            }

                        }
                        isGoingDown = false;
                    }

                    if (Canvas.GetLeft(x) > config.WidthOfTheScreen - 70)
                    {
                        isGoingDown = true;
                        isGoingRight = false;
                    }

                    if (Canvas.GetLeft(x) < 10)
                    {
                        isGoingDown = true;
                        isGoingRight = true;
                    }

                    if (Canvas.GetTop(x) > config.HeightOfTheScreen - 200)
                    {
                        gameOver = true;
                    }
                }
            }
            
        }

        /// <summary>
        /// Fait avancer les ennemis plus rapidement
        /// </summary>
        public void moreBoost()
        {
            boost += 0.1;
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