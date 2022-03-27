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

namespace AmoSim2.UserControls
{
    /// <summary>
    /// Interaction logic for TalizmanList.xaml
    /// </summary>
    public partial class TalizmanList : UserControl
    {
        public PlayerViewModel Main => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        public TalizmanList()
        {
            InitializeComponent();
        }

        public void ResetTalisman()
        {
            Main.Player.StrengthTalizman = 1;
            Main.Player.ToughnessTalizman = 1;
            Main.Player.AgilityTalizman = 1;
            Main.Player.SpeedTalizman = 1;
        }

        private void Talizmany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetTalisman();
            ListBox _talizman = (ListBox)sender;

            if (_talizman.SelectedIndex == 0 && Main.Player.StrengthTalizman == 1)
            {
                Main.Player.StrengthTalizman = 1.5;
            }
            else if (_talizman.SelectedIndex == 1 && Main.Player.ToughnessTalizman == 1)
            {
                Main.Player.ToughnessTalizman = 1.5;
            }
            else if (_talizman.SelectedIndex == 2 && Main.Player.StrengthTalizman == 1)
            {
                Main.Player.AgilityTalizman = 1.5;
            }
            else if (_talizman.SelectedIndex == 3 && Main.Player.AgilityTalizman == 1)
            {
                Main.Player.AgilityTalizman = 1.5;
            }
            else if (_talizman.SelectedIndex == 4 && Main.Player.SpeedTalizman == 1)
            {
                Main.Player.SpeedTalizman = 1.5;
            }
        }
    }
}
