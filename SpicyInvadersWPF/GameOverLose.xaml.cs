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
    /// Interaction logic for GameOverLose.xaml
    /// </summary>
    public partial class GameOverLose : Window
    {
        public GameOverLose()
        {
            InitializeComponent();
        }

        private void btnReturnLose_Click(object sender, RoutedEventArgs e)
        {
            Menu window = new Menu();
            window.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
            
        }
    }
}
