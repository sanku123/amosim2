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
    /// Interaction logic for SpellsList.xaml
    /// </summary>
    public partial class SpellsList : UserControl
    {
        public PlayerViewModel Main => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        public SpellsList()
        {
            InitializeComponent();
        }

        private void Czary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetSpells();

            if (Czary.SelectedIndex == 0)
            {
                Main.Player.CzarDEF = 1.5;
            }
            else if (Czary.SelectedIndex == 1)
            {
                Main.Player.CzarDEF = 1.1;
                Main.Player.CzarSpeed = 1.4;
            }
            else if (Czary.SelectedIndex == 2)
            {
                Main.Player.CzarDMG = 1.5;
            }
            else if (Czary.SelectedIndex == 2)
            {
                Main.Player.CzarDMG = 1.4;
                Main.Player.CzarSpeed2 = 1.1;
            }
        }
        public void ResetSpells()
        {
            Main.Player.CzarDMG = 1;
            Main.Player.CzarSpeed2 = 1;
            Main.Player.CzarDEF = 1;
            Main.Player.CzarSpeed = 1;
        }
    }
}
