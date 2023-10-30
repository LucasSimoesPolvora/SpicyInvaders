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

namespace SpicyInvadersWPF
{
    /// <summary>
    /// Interaction logic for GameOverWin.xaml
    /// </summary>
    public partial class GameOverWin : Window
    {
        public GameOverWin()
        {
            InitializeComponent();
        }

        private void btnContinueWin_Click(object sender, RoutedEventArgs e)
        {
            Menu window = new Menu();
            this.Visibility = Visibility.Hidden;
            window.Visibility = Visibility.Visible;
        }
    }
}
