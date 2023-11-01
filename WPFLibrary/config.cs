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
    public class config
    {
        public const int CONST_INT_COOLDOWN_TIME = 20;             // Cooldown pour éviter les spams des balles
        public const int CONST_INT_LEFT_OF_THE_SCREEN = 0;          // Position de la fenêtre dans l'écran depuis la gauche
        public const int CONST_INT_TOP_OF_THE_SCREEN = 0;           // Position de la fenêtre dans l'écran depuis le haut
        public double WidthOfTheScreen = System.Windows.SystemParameters.PrimaryScreenWidth;        // Largeur de l'application
        public double HeightOfTheScreen = System.Windows.SystemParameters.PrimaryScreenHeight;      // Hauteur de l'application
        public int ennemySpeed = 40;                                // Vitesse de l'ennemi
        public const int CONST_INT_ENNEMIES = 8;                   // Nbr d'ennemis par ligne
        public const int CONST_INT_NBR_ENNMIES_DIFF = 8;           // nbr d'ennemis différents
        public int timeCooldownEnemyBullet = 3;                     // Cooldown des balles ennemies

    }
}
