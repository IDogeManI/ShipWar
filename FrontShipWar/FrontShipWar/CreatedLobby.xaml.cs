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

namespace FrontShipWar
{
    /// <summary>
    /// Логика взаимодействия для CreatedLobby.xaml
    /// </summary>
    public partial class CreatedLobby : Window
    {
        public CreatedLobby()
        {
            InitializeComponent();
            lb_lobbyID.Content = Logic.lobbyId.ToString();
            WaitToConnect();
        }
        public async void WaitToConnect()
        {
            while (true)
            {
                await Task.Delay(100);
                string a = await Logic.GetLobbyState(Logic.lobbyId);
                if (a == "1")
                {
                    ShipCreation shipCreation = new ShipCreation();
                    shipCreation.Show();
                    this.Close();
                    break;
                }
            }
        }
    }
}
