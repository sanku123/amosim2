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
    /// Interaction logic for EquipList.xaml
    /// </summary>
    public partial class EquipList : UserControl
    {
        public PlayerViewModel Main => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        public EquipList()
        {
            InitializeComponent();
        }
        private void Broń_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (new[] { Main.Player.Strength, Main.Player.Agility }.Max() == Main.Player.Strength)
            {
                if (BrońLB1.SelectedIndex == 0)
                {
                    Main.Player.WeaponDMG = 2800;
                    Main.Player.WeaponSpeed = 300;
                }
                else if (BrońLB1.SelectedIndex == 1)
                {
                    Main.Player.WeaponDMG = 100 + (500 * EnchantedItemBonus());
                    Main.Player.WeaponSpeed = 2100;
                }     
            }
            else if (new[] { Main.Player.Strength, Main.Player.Agility }.Max() == Main.Player.Agility)
            {
                if (BrońLB1.SelectedIndex == 0)
                {
                    Main.Player.WeaponDMG = 4200 + (1000 * EnchantedItemBonus());
                    Main.Player.WeaponSpeed = 0;
                }
                else if (BrońLB1.SelectedIndex == 1)
                {
                    Main.Player.WeaponDMG = 2200 + (1000 * EnchantedItemBonus());
                    Main.Player.WeaponSpeed = 2800;
                }
            }
        }
        private void Ekwipunek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (new[] { Main.Player.Strength, Main.Player.Agility }.Max() == Main.Player.Strength)
            {
                if (EkwipunekLB2.SelectedIndex == 0)
                {
                    Main.Player.EQdefence = 2800;
                    Main.Player.EvasionArmor = 0;
                    Main.Player.EvasionLegs = 0;
                }
                else if (EkwipunekLB2.SelectedIndex == 1)
                {
                    Main.Player.EQdefence = 2800;
                    Main.Player.EvasionArmor = 0;
                    Main.Player.EvasionLegs = 0;
                }
            }
            else if (new[] { Main.Player.Strength, Main.Player.Agility }.Max() == Main.Player.Agility)
            {
                if (BrońLB1.SelectedIndex == 0)
                {
                    Main.Player.WeaponDMG = 5200;
                    Main.Player.WeaponSpeed = 0;
                }
                else if (BrońLB1.SelectedIndex == 1)
                {
                    Main.Player.WeaponDMG = 3100;
                    Main.Player.WeaponSpeed = 2800;
                }
            }
        }

        public double EnchantedItemBonus()
        {
            if (!Main.Player.Barbarian) return 1;
            return 0;

        }
    }
}
