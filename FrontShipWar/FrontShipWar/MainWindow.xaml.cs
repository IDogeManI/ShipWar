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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrontShipWar
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Logic.CreatePlayer();
        }

        private void bt_CreateLobby_Click(object sender, RoutedEventArgs e)
        {
            Logic.CreateLobby();
            CreatedLobby createdLobby = new CreatedLobby();
            createdLobby.Show();
            this.Close();
        }

        private void bt_ConnectToLobby_Click(object sender, RoutedEventArgs e)
        {
            if (Logic.ConnectToLobby(tb_LobbyId.Text) == "true")
            {
                ShipCreation shipCreation = new ShipCreation();
                shipCreation.Show();
                this.Close();
            }
        }
    }
}
