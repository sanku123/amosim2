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
    /// Interaction logic for ClassList.xaml
    /// </summary>
    public partial class ClassList : UserControl
    {
        public PlayerViewModel Main => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        public ClassList()
        {
            InitializeComponent();
        }

        private void Class_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Class.SelectedIndex == 0)
            {
                Main.Player.Class = "Wojownik";
            }
            else if (Class.SelectedIndex == 1)
            {
                Main.Player.Class = "Barbarzyńca";
                Main.Player.Barbarian = true;
            }
            else if (Class.SelectedIndex == 2)
            {
                Main.Player.Class = "Mag";
            }
            else if (Class.SelectedIndex == 3)
            {
                Main.Player.Class = "Łowca";
            }
            else if (Class.SelectedIndex == 4)
            {
                Main.Player.Class = "Złodziej";
            }
            else if (Class.SelectedIndex == 5)
            {
                Main.Player.Class = "Paladyn";
            }
            else if (Class.SelectedIndex == 6)
            {
                Main.Player.Class = "Czarnoksieżnik";
            }
        }
    }
}