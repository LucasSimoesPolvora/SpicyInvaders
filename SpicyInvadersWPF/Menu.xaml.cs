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
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        config config = new config();
        database db = new database();
        public Menu()
        {
            InitializeComponent();

            Left = config.CONST_INT_LEFT_OF_THE_SCREEN;
            Top = config.CONST_INT_TOP_OF_THE_SCREEN;
            Width = config.WidthOfTheScreen;
            Height = config.HeightOfTheScreen;
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void Highscore_Click(object sender, RoutedEventArgs e)
        {
            string answer = db.tryConnection();
            if(answer == "connection successful")
            {
                Highscore window = new Highscore();
                this.Close();
                window.Show();
            }
            else
            {
                MessageBox.Show("Erreur de base de donnée :" + answer);
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            Help window = new Help();
            this.Close();
            window.Show();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
