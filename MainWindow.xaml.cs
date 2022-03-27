using AmoSim2.Battle;
using AmoSim2.Player;
using AmoSim2.ViewModel;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace AmoSim2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public PlayerViewModel Main => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void MyWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Session.SavePlayerData(Main.Player);
            Session.SaveBattleData(BattleResultsSource.Results);
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Session.LoadPlayerData(Main.Player);
            //Session.LoadBattleData(BattleResultsSource.Results);
        }


    }
}
