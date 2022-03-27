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
    /// Interaction logic for KosturList.xaml
    /// </summary>
    public partial class KosturyList : UserControl
    {
        public PlayerViewModel Main => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        public KosturyList()
        {
            InitializeComponent();
        }
        public void ResetKostury()
        {
            Main.Player.KosturDMG = 0;
            Main.Player.KosturDEF = 0;
            Main.Player.KosturSpeed = 0;
        }
        private void Kostur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ResetKostury();
            if (Kostur.SelectedIndex == 0)
            {
                Main.Player.KosturDEF = 10000;
            }
            else if (Kostur.SelectedIndex == 1)
            {
                Main.Player.KosturDMG = 5000;
                Main.Player.KosturDEF = 5000;
            }
            else if (Kostur.SelectedIndex == 2)
            {
                Main.Player.KosturDEF = 5000;
                Main.Player.KosturSpeed = 5000;
            }
        }
    }
}
