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
    /// Логика взаимодействия для ShipCreation.xaml
    /// </summary>
    public partial class ShipCreation : Window
    {
        public ShipCreation()
        {
            InitializeComponent();
        }

        private bool isRotation = true;
        private int[] ships = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1, 0 };
        private int shipIterator = 0;
        private void grid_Main_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    ShipButton button = new ShipButton();
                    button.x = j;
                    button.y = i;
                    button.isShipped = false;
                    button.MouseEnter += button_MouseEnter;
                    button.MouseLeave += button_MouseLeave;
                    button.Click += button_Click;
                    grid_Main.Children.Add(button);
                }
            }

        }

        private void grid_Main_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.R)
            {
                isRotation = !isRotation;
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            int xOrY;
            if (isRotation)
            {
                xOrY = ((ShipButton)sender).x;
            }
            else
            {
                xOrY = ((ShipButton)sender).y;
            }
            for (int i = xOrY; (xOrY + ships[shipIterator] < 9) ? (i < xOrY + ships[shipIterator]) : (i > xOrY - ships[shipIterator]);)
            {
                foreach (ShipButton shipButton in grid_Main.Children)
                {
                    if (isRotation ? shipButton.x == i && shipButton.y == ((ShipButton)sender).y : shipButton.y == i && shipButton.x == ((ShipButton)sender).x)
                    {
                        shipButton.Background = Brushes.Red;
                        shipButton.isShipped = true;
                        break;
                    }
                }
                if (xOrY + ships[shipIterator] < 9)
                    i++;
                else
                    i--;
            }
            shipIterator++;
            if (ships[shipIterator] == 0)
            {
                string shipMatrix = "[";
                foreach (ShipButton shipButton in grid_Main.Children)
                {
                    Logic.shipButtons.Add(new ShipButton() {x=shipButton.x,y=shipButton.y,isShipped=shipButton.isShipped});
                    shipMatrix += "{\"x\":"+shipButton.x+",\"y\":"+shipButton.y+",\"isShipped\":"+(shipButton.isShipped ? "true":"false")+"},";
                }
                shipMatrix= shipMatrix.Substring(0,shipMatrix.Length-1);
                shipMatrix += "]";
                Logic.SetMatrix(shipMatrix);
                WaitOppToCreate waitOppToCreate = new WaitOppToCreate();
                waitOppToCreate.Show();
                this.Close();
            }
        }
        private void button_MouseLeave(object sender, MouseEventArgs e)
        {
            foreach (ShipButton button in grid_Main.Children)
            {
                if (!button.isShipped)
                {
                    button.Background = null;
                }
            }
        }

        private void button_MouseEnter(object sender, MouseEventArgs e)
        {
            int xOrY;
            if (isRotation)
            {
                xOrY = ((ShipButton)sender).x;
            }
            else
            {
                xOrY = ((ShipButton)sender).y;
            }
            for (int i = xOrY; (xOrY + ships[shipIterator] < 9) ? (i < xOrY + ships[shipIterator]) : (i > xOrY - ships[shipIterator]);)
            {
                foreach (ShipButton shipButton in grid_Main.Children)
                {
                    if (isRotation ? shipButton.x == i && shipButton.y == ((ShipButton)sender).y : shipButton.y == i && shipButton.x == ((ShipButton)sender).x)
                    {
                        if (!shipButton.isShipped)
                        {
                            shipButton.Background = Brushes.Yellow;
                        }
                        break;
                    }
                }
                if (xOrY + ships[shipIterator] < 9)
                    i++;
                else
                    i--;
            }
        }
    }
}
