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
        
        public EnemyViewModel EnemyViewModel => ServiceLocator.Current.GetInstance<EnemyViewModel>();


        public EnemyViewModel enemy = new EnemyViewModel();


        private readonly Random rnd = new Random();

        public double WinCount { get; set; }

        public double LostCount { get; set; }

        public double DrawCount { get; set; }

        public MainWindow()
        {
            InitializeComponent();
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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var player = PlayerViewModel.Player;
            var enemy = EnemyViewModel.Enemy;

            PlayerInicjatywaBaseLbl.Content = player.PlayerInicjatywaBase.ToString();
            PlayerInicjatywaLbl.Content = player.PlayerInicjatywa.ToString();
            EnemyInicjatywaBaseLbl.Content = enemy.EnemyInicjatywaBase.ToString();
            EnemyInicjatywaLbl.Content = enemy.EnemyInicjatywa.ToString();
            PlayerTrafienieLbl.Content = player.PlayerHitChance.ToString();
            EnemyTrafienieLbl.Content = enemy.EnemyHitChance.ToString();
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
            var player = PlayerViewModel.Player;
            var enemy = EnemyViewModel.Enemy;

            int enemyLevel = (int)Math.Max(1, enemy.Level);
            double enemyHitChance = Math.Max(2, enemy.EnemyHitChance);

            for (int y = 0; y < enemy.EnemyInicjatywa && (HP1 > 0); y++)
            {
                if (enemyHitChance >= rnd.Next(1, 101))
                {
                    if (player.BlockChance >= rnd.Next(1, 101))
                    {
                        continue;
                    }
                    else
                    {
                        if (enemy.Class == "Łowca" && player.Race != "Jaszczuroczłek")
                        {
                            int damage = (int)Math.Max(enemy.BonusŁowcy, (enemy.Attack + rnd.Next(1, (int)(5 * enemyLevel))) * enemy.Critical() * enemy.ThiefDamagePenalty + enemy.BonusŁowcy - player.Defence);
                            HP1 -= damage;
                        }
                        else if (enemy.Class == "Czarnoksiężnik")
                        {
                            int damage = (int)Math.Max(enemy.WarlockPoisonDamage, (enemy.Attack + rnd.Next(1, (int)(5 * enemyLevel))) * enemy.Critical() * enemy.ThiefDamagePenalty + enemy.WarlockPoisonDamage - player.Defence);
                            HP1 -= damage;
                        }
                        else
                        {
                            int damage = (int)Math.Max(0, (enemy.Attack + rnd.Next(1, (int)(5 * enemyLevel))) * enemy.Critical() * enemy.ThiefDamagePenalty - player.Defence);
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
                            HP2 -= damage;
                        }
                        else if (PlayerViewModel.Player.Class == "Czarnoksiężnik")
                        {
                            int damage = (int)Math.Max(PlayerViewModel.Player.WarlockPoisonDamage, (PlayerViewModel.Player.Attack + rnd.Next(1, (int)(5 * pLevel))) * PlayerViewModel.Player.Critical() * PlayerViewModel.Player.ThiefDamagePenalty + PlayerViewModel.Player.WarlockPoisonDamage - EnemyViewModel.Enemy.Defence);
                            HP2 -= damage;
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
    }
}