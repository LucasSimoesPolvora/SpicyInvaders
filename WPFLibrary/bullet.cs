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

        public void enemyBulletCooldown(Canvas myCanvas, Rectangle Player)
        {
            BulletTimer = BulletTimer - 3;

            if (BulletTimer < 0)
            {
                ennemyBulletMaker(Canvas.GetLeft(Player) + 20, 10, myCanvas);

                BulletTimer = BulletTimerLimit;
            }
        }

        public void enemyBulletMovement()
        {

        }

        public void playerBulletCooldown()
        {
            Cooldown--;
            if (Cooldown == 0)
            {
                NumberBullets++;
                Cooldown = config.CONST_INT_COOLDOWN_TIME;
            }
        }

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
                                score.ScoreValue += score.MaxDeadValue;

                                score.MaxDeadValue--;

                            }
                        }
                    }
                }
            }
        }
    }
}

