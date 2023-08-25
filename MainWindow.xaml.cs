using AmoSim2.Player;
using CommonServiceLocator;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace AmoSim2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlayerViewModel PlayerViewModel => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        public EnemyViewModel EnemyViewModel => ServiceLocator.Current.GetInstance<EnemyViewModel>();

        private readonly Random rnd = new Random();

        public double WinCount { get; set; }

        public double LostCount { get; set; }

        public double DrawCount { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MyWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PlayerViewModel.Player.Race = "";
            PlayerViewModel.Player.Class = "";
             PlayerViewModel.Player.SelectedKostur = "";
            PlayerViewModel.Player.SelectedTalisman = "";
            PlayerViewModel.Player.SelectedRobe = "";
            PlayerViewModel.Player.SelectedBlessing = "";
            PlayerViewModel.Player.SelectedCzar1 = "";
            PlayerViewModel.Player.SelectedCzar2 = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinCount = 0;
            LostCount = 0;
            DrawCount = 0;

            int iteracje = 20000;

            for (int i = 0; i < iteracje; i++)
            {
                BeginFastSimulation();
            }

            Win_Tb.Text = (WinCount / iteracje * 100).ToString() + "%";
            Lost_Tb.Text = (LostCount / iteracje * 100).ToString() + "%";
            Draw_Tb.Text = (DrawCount / iteracje * 100).ToString() + "%";
        }

        private void BeginFastSimulation()
        {
            EnemyViewModel.Enemy.MP -= 1;

            PlayerViewModel.Player.MP -= 1;

            BattleLog.Text = String.Empty;

            int HP1 = (int)PlayerViewModel.Player.HP;
            int HP2 = (int)EnemyViewModel.Enemy.HP;

            for (int i = 1; i <= 24; i++)
            {
                if (PlayerViewModel.Player.BattleSpeed > EnemyViewModel.Enemy.BattleSpeed)
                {
                    HP2 = PerformPlayerAttackMulti(HP1, HP2);

                    if (HP2 < 1)
                    {
                        WinCount += 1;
                        break;
                    }

                    HP1 = PerformEnemyAttackMulti(HP1);

                    if (HP1 < 1)
                    {
                        LostCount += 1;
                        break;
                    }
                    if (i == 24)
                    {
                        DrawCount += 1;
                        break;
                    }
                }
                else
                {
                    HP1 = PerformEnemyAttackMulti(HP1);

                    if (HP1 < 1)
                    {
                        LostCount += 1;
                        break;
                    }

                    HP2 = PerformPlayerAttackMulti(HP1, HP2);

                    if (HP2 < 1)
                    {
                        WinCount += 1;
                        break;
                    }
                    if (i == 24)
                    {
                        DrawCount += 1;
                        break;
                    }
                }
            }
        }

        private int PerformEnemyAttackMulti(int HP1)
        {
            int eLevel = (int)Math.Max(1, EnemyViewModel.Enemy.Level);
            double enemyHitChance = Math.Max(2, EnemyViewModel.Enemy.EnemyHitChance);

            for (int y = 0; y < EnemyViewModel.Enemy.EnemyInicjatywa && (HP1 > 0); y++)
            {
                if (enemyHitChance >= rnd.Next(1, 101))
                {
                    if (PlayerViewModel.Player.BlockChance >= rnd.Next(1, 101))
                    {
                        continue;
                    }
                    else
                    {
                        if (EnemyViewModel.Enemy.Class == "Łowca" && PlayerViewModel.Player.Race != "Jaszczuroczłek")
                        {
                            int damage = (int)Math.Max(EnemyViewModel.Enemy.BonusŁowcy, (EnemyViewModel.Enemy.Attack + rnd.Next(1, (int)(5 * eLevel))) * EnemyViewModel.Enemy.Critical() * EnemyViewModel.Enemy.ThiefDamagePenalty + EnemyViewModel.Enemy.BonusŁowcy - PlayerViewModel.Player.Defence);
                            HP1 -= damage;
                        }
                        else
                        {
                            int damage = (int)Math.Max(0, (EnemyViewModel.Enemy.Attack + rnd.Next(1, (int)(5 * eLevel))) * EnemyViewModel.Enemy.Critical() * EnemyViewModel.Enemy.ThiefDamagePenalty - PlayerViewModel.Player.Defence);
                            HP1 -= damage;
                        }
                    }
                }
            }

            return HP1;
        }

        private int PerformPlayerAttackMulti(int HP1, int HP2)
        {
            int pLevel = (int)Math.Max(1, PlayerViewModel.Player.Level);
            double playerHitChance = Math.Max(2, PlayerViewModel.Player.PlayerHitChance);

            for (int x = 0; x < PlayerViewModel.Player.PlayerInicjatywa && (HP1 > 0); x++)
            {
                if (playerHitChance >= rnd.Next(1, 101))
                {
                    if (EnemyViewModel.Enemy.BlockChance >= rnd.Next(1, 101))
                    {
                        continue;
                    }
                    else
                    {
                        if (PlayerViewModel.Player.Class == "Łowca" && EnemyViewModel.Enemy.Race != "Jaszczuroczłek")
                        {
                            int damage = (int)Math.Max(PlayerViewModel.Player.BonusŁowcy, (PlayerViewModel.Player.Attack + rnd.Next(1, (int)(5 * pLevel))) * PlayerViewModel.Player.Critical() * PlayerViewModel.Player.ThiefDamagePenalty + PlayerViewModel.Player.BonusŁowcy - EnemyViewModel.Enemy.Defence);
                            HP2 -= Math.Max(PlayerViewModel.Player.BonusŁowcy, damage);
                        }
                        else
                        {
                            int damage = (int)Math.Max(0, (PlayerViewModel.Player.Attack + rnd.Next(1, (int)(5 * pLevel))) * PlayerViewModel.Player.Critical() * PlayerViewModel.Player.ThiefDamagePenalty - EnemyViewModel.Enemy.Defence);
                            HP2 -= damage;
                        }
                    }
                }
            }

            return HP2;
        }

        private void SaveBtnPlayer_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string filter = "Json files (*.json)|*.json";
            saveFileDialog1.Filter = filter;

            if (saveFileDialog1.ShowDialog() == true)
            {
                filter = saveFileDialog1.FileName;
                Session.SavePlayerData(filter, PlayerViewModel.Player);
            }
        }

        private void SaveBtnEnemy_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string filter = "Json files (*.json)|*.json";
            saveFileDialog1.Filter = filter;

            if (saveFileDialog1.ShowDialog() == true)
            {
                filter = saveFileDialog1.FileName;
                Session.SavePlayerData(filter, EnemyViewModel.Enemy);
            }
        }

        private void LoadBtnPlayer_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string filter = "Json files (*.json)|*.json";
            openFileDialog1.Filter = filter;

            if (openFileDialog1.ShowDialog() == true)
            {
                filter = openFileDialog1.FileName;
                Session.LoadPlayerData(filter, PlayerViewModel.Player);
            }
        }

        private void LoadBtnEnemy_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string filter = "Json files (*.json)|*.json";
            openFileDialog1.Filter = filter;

            if (openFileDialog1.ShowDialog() == true)
            {
                filter = openFileDialog1.FileName;
                Session.LoadPlayerData(filter, EnemyViewModel.Enemy);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(PlayerViewModel.Player.PlayerInicjatywaBase.ToString() +
                            Environment.NewLine + PlayerViewModel.Player.PlayerInicjatywa.ToString() +
                            Environment.NewLine + EnemyViewModel.Enemy.EnemyInicjatywaBase.ToString() +
                            Environment.NewLine + EnemyViewModel.Enemy.EnemyInicjatywa.ToString());
        }
    }
}