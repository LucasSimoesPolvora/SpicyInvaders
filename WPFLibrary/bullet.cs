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


        public void EnnemyBulletMaker(double x, double y, Canvas myCanvas)
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

        /*public void EnemyBulletCooldown(Canvas myCanvas)
        {
            BulletTimer = BulletTimer - 3;

            if (BulletTimer < 0)
            {
                bullet.EnnemyBulletMaker(Canvas.GetLeft(Player) + 20, 10, myCanvas);

                BulletTimer = BulletTimerLimit;
            }
        }*/

        public void PlayerBulletCooldown(Canvas myCanvas)
        {
            Cooldown--;
            if (Cooldown == 0)
            {
                NumberBullets++;
                Cooldown = config.CONST_INT_COOLDOWN_TIME;
            }
        }
    }
}
