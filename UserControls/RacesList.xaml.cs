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
    /// Interaction logic for RacesList.xaml
    /// </summary>
    public partial class RacesList : UserControl
    {
        public PlayerViewModel Main => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        public RacesList()
        {
            InitializeComponent();
        }
        private void Race_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Race.SelectedIndex == 0)
            {
                Main.Player.Race = "Człowiek";
            }
            else if (Race.SelectedIndex == 1)
            {
                Main.Player.Race = "Krasnolud";
            }
            else if (Race.SelectedIndex == 2)
            {
                Main.Player.Race = "Elf";
            }
            else if (Race.SelectedIndex == 3)
            {
                Main.Player.Race = "Jaszczuroczłek";
            }
            else if (Race.SelectedIndex == 4)
            {
                Main.Player.Race = "Hobbit";
            }
            else if (Race.SelectedIndex == 5)
            {
                Main.Player.Race = "Gnom";
            }
            else if (Race.SelectedIndex == 6)
            {
                Main.Player.Race = "Wampir";
            }
            else if (Race.SelectedIndex == 7)
            {
                Main.Player.Race = "Wilkołak";
            }
        }

    }
}
