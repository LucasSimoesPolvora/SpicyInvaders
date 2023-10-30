using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Model;

namespace SpicyInvadersWPF
{
    /// <summary>
    /// Interaction logic for GameOverWin.xaml
    /// </summary>
    public partial class GameOverWin : Window
    {

        config config = new config();
        public GameOverWin()
        {
            InitializeComponent();


            Left = config.CONST_INT_LEFT_OF_THE_SCREEN;
            Top = config.CONST_INT_TOP_OF_THE_SCREEN;
            Width = config.WidthOfTheScreen;
            Height = config.HeightOfTheScreen;
        }

        private void btnContinueWin_Click(object sender, RoutedEventArgs e)
        {
            Menu window = new Menu();
            this.Visibility = Visibility.Hidden;
            window.Visibility = Visibility.Visible;
        }
    }
}
