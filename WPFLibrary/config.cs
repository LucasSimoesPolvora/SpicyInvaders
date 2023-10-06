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
        public const int CONST_INT_ENNEMIES = 8;                   // Nbr d'ennemis par ligne
        public const int CONST_INT_NBR_ENNMIES_DIFF = 8;           // nbr d'ennemis différents
        public const int CONST_INT_COOLDOWN_TIME = 20;             // Cooldown pour éviter les spams des balles
    }
}
