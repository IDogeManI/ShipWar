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
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
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
                if (a == "4")
                {
                    End end = new End();
                    end.Show();
                    this.Close();
                    break;
                }
                if ((a == "2"&&Logic.isPlayerHost)||(a=="3"&&!Logic.isPlayerHost))
                {
                    string lastOppShoot = Logic.GetLastOppShoot();
                    foreach (ShipButton shipButton in grid_My.Children)
                    {
                        if (lastOppShoot[5].ToString() == "-")
                            break;
                        if(shipButton.x.ToString() == lastOppShoot[5].ToString() && shipButton.y.ToString() == lastOppShoot[11].ToString())
                        {
                            shipButton.IsEnabled= false;
                            break;
                        }
                    }
                    foreach (ShipButton shipButton in grid_Other.Children)
                    {
                         shipButton.IsEnabled = true;
                    }
                }
                else
                {
                    foreach (ShipButton shipButton in grid_Other.Children)
                    {
                        shipButton.IsEnabled = false;
                    }
                }
            }
        }

        private void grid_My_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (ShipButton shipButton in Logic.shipButtons)
            {
                if (shipButton.isShipped)
                {
                    shipButton.Background = Brushes.Red;
                }
                grid_My.Children.Add(shipButton);
            }
        }

        private void grid_Other_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    ShipButton button = new ShipButton();
                    button.x = j;
                    button.y = i;
                    button.isShipped = false;
                    button.Click += button_Other_Click;
                    grid_Other.Children.Add(button);
                }
            }
        }

        private void button_Other_Click(object sender, RoutedEventArgs e)
        {
            if(Logic.Shoot("{\"x\":" + ((ShipButton)sender).x + ",\"y\":" + ((ShipButton)sender).y + "}") == "true")
            {
                ((ShipButton)sender).Background= Brushes.Red;
            }
            else
            {
                ((ShipButton)sender).Background = Brushes.Yellow;
            }
            ((ShipButton)sender).Click -= button_Other_Click;
        }
    }
}
