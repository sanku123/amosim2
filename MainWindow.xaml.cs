using AmoSim2.Player;
using AmoSim2.ViewModel;
using CommonServiceLocator;
using Microsoft.Win32;
using System;
using System.Windows;

namespace AmoSim2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlayerViewModel PlayerViewModel => ServiceLocator.Current.GetInstance<PlayerViewModel>();
        

        public MainWindow()
        {
            InitializeComponent();
        }
     
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var player = PlayerViewModel.Player;
            var enemy = PlayerViewModel.Enemy;

            PlayerInicjatywaBaseLbl.Content = player.PlayerInicjatywaBase.ToString();
            PlayerInicjatywaLbl.Content = player.PlayerInicjatywa.ToString();
            EnemyInicjatywaBaseLbl.Content = enemy.EnemyInicjatywaBase.ToString();
            EnemyInicjatywaLbl.Content = enemy.EnemyInicjatywa.ToString();
            PlayerTrafienieLbl.Content = player.PlayerHitChance.ToString();
            EnemyTrafienieLbl.Content = enemy.EnemyHitChance.ToString();
        }
    }
}