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
    /// Логика взаимодействия для WaitOppToCreate.xaml
    /// </summary>
    public partial class WaitOppToCreate : Window
    {
        public WaitOppToCreate()
        {
            InitializeComponent();
            WaitOpp();
        }
        public async void WaitOpp()
        {
            while (true)
            {
                await Task.Delay(100);
                string a = await Logic.GetLobbyState(Logic.lobbyId);
                if (a == "2")
                {
                    GameWindow gameWindow = new GameWindow();
                    gameWindow.Show();
                    this.Close();
                    break;
                }
            }
        }
    }
}
