using AmoSim2.Player;
using CommonServiceLocator;
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
using System.Windows.Interactivity;

namespace AmoSim2.UserControls
{
    /// <summary>
    /// Interaction logic for BlessList.xaml
    /// </summary>
    public partial class BlessList : UserControl
    {
        public PlayerViewModel Main => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        public BlessList()
        {
            InitializeComponent();
        }

        public void ResetBlessings()
        {
            Main.Player.StrengthBless = 0;
            Main.Player.ToughnessBless = 0;
            Main.Player.AgilityBless = 0;
            Main.Player.SpeedBless = 0;
            Main.Player.InteligenceBless = 0;
            Main.Player.WillPowerBless = 0;
            Main.Player.TrafienieBless = 0;
            Main.Player.UnikiBless = 0;
        }

        private void Bless_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetBlessings();

            switch (Bless.SelectedIndex)
            {
                case 0:
                    Main.Player.StrengthBless = 6 * Main.Player.Level * Main.Player.Modificator;
                    break;

                case 1:
                    Main.Player.ToughnessBless = 6 * Main.Player.Level * Main.Player.Modificator;
                    break;

                case 2:
                    Main.Player.AgilityBless = 6 * Main.Player.Level * Main.Player.Modificator;
                    break;

                case 3:
                    Main.Player.SpeedBless = 6 * Main.Player.Level * Main.Player.Modificator;
                    break;

                case 4:
                    Main.Player.InteligenceBless = 6 * Main.Player.Level * Main.Player.Modificator;
                    break;

                case 5:
                    Main.Player.WillPowerBless = 6 * Main.Player.Level * Main.Player.Modificator;
                    break;

                case 6:
                    Main.Player.TrafienieBless = 6 * Main.Player.Level * Main.Player.Modificator;
                    break;

                case 7:
                    Main.Player.UnikiBless = 6 * Main.Player.Level * Main.Player.Modificator;
                    break;

                default:
                    ResetBlessings();
                    break;
            }
        }
    }
}