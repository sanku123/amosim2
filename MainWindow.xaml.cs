using AmoSim2.Player;
using AmoSim2.ViewModel;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Win32;
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
        public PlayerViewModel PlayerViewModel => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        public EnemyViewModel EnemyViewModel => ServiceLocator.Current.GetInstance<EnemyViewModel>();

        private Random rnd = new Random();

        public double WinCount { get; set; }

        public double LostCount { get; set; }

        public double DrawCount { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MyWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Session.SavePlayerData(PlayerViewModel.Player);
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Session.LoadPlayerData(Main.Player);
            //Session.LoadBattleData(BattleResultsSource.Results);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(PlayerViewModel.Player.HP.ToString());
            //MessageBox.Show(EnemyViewModel.Enemy.HP.ToString());

            BeginSimulation();

            WinCount = 0;
            LostCount = 0;
            DrawCount = 0;

            int iteracje = 1000;

            for (int i = 0; i < iteracje; i++)
            {
                BeginFastSimulation();
            }

            Win_Tb.Text = (WinCount / iteracje * 100).ToString() + "%";
            Lost_Tb.Text = (LostCount / iteracje * 100).ToString() + "%";
            Draw_Tb.Text = (DrawCount / iteracje * 100).ToString() + "%";
        }

        private void BeginSimulation()
        {
            scrollBattle.Height = 560;
            BattleLog.Text = String.Empty;

            int HP1 = (int)PlayerViewModel.Player.HP;
            int HP2 = (int)EnemyViewModel.Enemy.HP;

            for (int i = 1; i <= 24; i++)
            {
                BattleLog.Inlines.Add("Tura: " + i.ToString() + Environment.NewLine);
                for (int x = 0; x < PlayerViewModel.Player.PlayerInicjatywa && (HP2 > 0); x++)
                {
                    if (PlayerViewModel.Player.PlayerHitChance >= rnd.Next(1, 101))
                    {
                        if (EnemyViewModel.Enemy.BlockChance >= rnd.Next(1, 101))
                        {
                            MsgAtakOdparty();
                        }
                        else
                        {
                            if (PlayerViewModel.Player.Class == "Łowca" && EnemyViewModel.Enemy.Race != "Jaszczuroczłek")
                            {
                                int damage = (int)Math.Max(PlayerViewModel.Player.BonusŁowcy, (PlayerViewModel.Player.Attack + rnd.Next(1, (int)(5 * PlayerViewModel.Player.Level))) * PlayerViewModel.Player.Critical() * PlayerViewModel.Player.ThiefDamagePenalty + PlayerViewModel.Player.BonusŁowcy - EnemyViewModel.Enemy.Defence);
                                HP2 -= Math.Max(PlayerViewModel.Player.BonusŁowcy, damage);
                                MsgAtak(HP2, damage);
                            }
                            else
                            {
                                int damage = (int)Math.Max(0, (PlayerViewModel.Player.Attack + rnd.Next(1, (int)(5 * PlayerViewModel.Player.Level))) * PlayerViewModel.Player.Critical() * PlayerViewModel.Player.ThiefDamagePenalty - EnemyViewModel.Enemy.Defence);
                                HP2 -= damage;
                                MsgAtak(HP2, damage);
                            }
                        }
                    }
                    else
                    {
                        MsgAtakNietrafiony();
                    }
                }

                if (HP2 < 1)
                {
                    WinCount += 1;
                    BattleLog.Inlines.Add(Environment.NewLine);
                    BattleLog.Inlines.Add(PlayerViewModel.Player.Nickname + " zwycięża! ");
                }

                for (int y = 0; y < EnemyViewModel.Enemy.EnemyInicjatywa && (HP1 > 0); y++)
                {
                    if (EnemyViewModel.Enemy.EnemyHitChance >= rnd.Next(1, 101))
                    {
                        if (PlayerViewModel.Player.BlockChance >= rnd.Next(1, 101))
                        {
                            MsgAtakOdpartyXP();
                        }
                        else
                        {
                            if (EnemyViewModel.Enemy.Race == "Łowca" && PlayerViewModel.Player.Race != "Jaszczuroczłek")
                            {
                                int damage = (int)Math.Max(EnemyViewModel.Enemy.BonusŁowcy, (EnemyViewModel.Enemy.Attack + rnd.Next(1, (int)(5 * EnemyViewModel.Enemy.Level))) * EnemyViewModel.Enemy.Critical() * EnemyViewModel.Enemy.ThiefDamagePenalty + EnemyViewModel.Enemy.BonusŁowcy - PlayerViewModel.Player.Defence);
                                HP1 -= damage;
                                MsgAtakXP(HP1, damage);
                            }
                            else
                            {
                                int damage = (int)Math.Max(0, (EnemyViewModel.Enemy.Attack + rnd.Next(1, (int)(5 * EnemyViewModel.Enemy.Level))) * EnemyViewModel.Enemy.Critical() * EnemyViewModel.Enemy.ThiefDamagePenalty - PlayerViewModel.Player.Defence);
                                HP1 -= damage;
                                MsgAtakXP(HP1, damage);
                            }
                        }
                    }
                    else
                    {
                        MsgAtakNietrafionyXP();
                    }
                }

                if (HP1 < 1)
                {
                    LostCount += 1;
                    BattleLog.Inlines.Add(Environment.NewLine);
                    BattleLog.Inlines.Add(EnemyViewModel.Enemy.Nickname + " zwycięża! ");
                    break;
                }

                if (i == 24)
                {
                    DrawCount += 1;
                    BattleLog.Inlines.Add(Environment.NewLine);
                    BattleLog.Inlines.Add("Walka nierozstrzygnięta! ");
                    break;
                }
                BattleLog.Inlines.Add(Environment.NewLine);
            }
        }

        private void BeginFastSimulation()
        {
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
            for (int y = 0; y < EnemyViewModel.Enemy.EnemyInicjatywa && (HP1 > 0); y++)
            {
                if (EnemyViewModel.Enemy.EnemyHitChance >= rnd.Next(1, 101))
                {
                    if (PlayerViewModel.Player.BlockChance >= rnd.Next(1, 101))
                    {
                        continue;
                    }
                    else
                    {
                        if (EnemyViewModel.Enemy.Class == "Łowca" && PlayerViewModel.Player.Race != "Jaszczuroczłek")
                        {
                            int damage = (int)Math.Max(EnemyViewModel.Enemy.BonusŁowcy, (EnemyViewModel.Enemy.Attack + rnd.Next(1, (int)(5 * EnemyViewModel.Enemy.Level))) * EnemyViewModel.Enemy.Critical() * EnemyViewModel.Enemy.ThiefDamagePenalty + EnemyViewModel.Enemy.BonusŁowcy - PlayerViewModel.Player.Defence);
                            HP1 -= damage;
                        }
                        else
                        {
                            int damage = (int)Math.Max(0, (EnemyViewModel.Enemy.Attack + rnd.Next(1, (int)(5 * EnemyViewModel.Enemy.Level))) * EnemyViewModel.Enemy.Critical() * EnemyViewModel.Enemy.ThiefDamagePenalty - PlayerViewModel.Player.Defence);
                            HP1 -= damage;
                        }
                    }
                }
            }

            return HP1;
        }

        private int PerformPlayerAttackMulti(int HP1, int HP2)
        {
            for (int x = 0; x < PlayerViewModel.Player.PlayerInicjatywa && (HP1 > 0); x++)
            {
                if (PlayerViewModel.Player.PlayerHitChance >= rnd.Next(1, 101))
                {
                    if (EnemyViewModel.Enemy.BlockChance >= rnd.Next(1, 101))
                    {
                        continue;
                    }
                    else
                    {
                        if (PlayerViewModel.Player.Class == "Łowca" && EnemyViewModel.Enemy.Race != "Jaszczuroczłek")
                        {
                            int damage = (int)Math.Max(PlayerViewModel.Player.BonusŁowcy, (PlayerViewModel.Player.Attack + rnd.Next(1, (int)(5 * PlayerViewModel.Player.Level))) * PlayerViewModel.Player.Critical() * PlayerViewModel.Player.ThiefDamagePenalty + PlayerViewModel.Player.BonusŁowcy - EnemyViewModel.Enemy.Defence);
                            HP2 -= Math.Max(PlayerViewModel.Player.BonusŁowcy, damage);
                        }
                        else
                        {
                            int damage = (int)Math.Max(0, (PlayerViewModel.Player.Attack + rnd.Next(1, (int)(5 * PlayerViewModel.Player.Level))) * PlayerViewModel.Player.Critical() * PlayerViewModel.Player.ThiefDamagePenalty - EnemyViewModel.Enemy.Defence);
                            HP2 -= damage;
                        }
                    }
                }
            }

            return HP2;
        }

        private void MsgAtakNietrafionyXP()
        {
            BattleLog.Inlines.Add(PlayerViewModel.Player.Nickname);
            BattleLog.Inlines.Add(new Run(" uniknął ataku ") { Foreground = Brushes.Green });
            BattleLog.Inlines.Add(EnemyViewModel.Enemy.Nickname);
            BattleLog.Inlines.Add(Environment.NewLine);
        }

        private void MsgAtakXP(int HP1, int damage)
        {
            BattleLog.Inlines.Add(EnemyViewModel.Enemy.Nickname);
            BattleLog.Inlines.Add(new Run(" atakuje ") { Foreground = Brushes.Red });
            BattleLog.Inlines.Add(PlayerViewModel.Player.Nickname);
            BattleLog.Inlines.Add(new Run(" i zadaje ") { Foreground = Brushes.Red });
            BattleLog.Inlines.Add(damage.ToString());
            BattleLog.Inlines.Add(new Run(" obrażeń! (") { Foreground = Brushes.Red });
            BattleLog.Inlines.Add(new Run(HP1.ToString()) { Foreground = Brushes.Red });
            BattleLog.Inlines.Add(new Run(" zostało)") { Foreground = Brushes.Red });
            BattleLog.Inlines.Add(Environment.NewLine);
        }

        private void MsgAtakKrytycznyXP(int HP1, int damage)
        {
            BattleLog.Inlines.Add(EnemyViewModel.Enemy.Nickname);
            BattleLog.Inlines.Add(new Run(" w przypływie szału walki atakuje ") { Foreground = Brushes.Red });
            BattleLog.Inlines.Add(PlayerViewModel.Player.Nickname);
            BattleLog.Inlines.Add(new Run(" i zadaje ") { Foreground = Brushes.Red });
            BattleLog.Inlines.Add(damage.ToString());
            BattleLog.Inlines.Add(new Run(" obrażeń! (") { Foreground = Brushes.Red });
            BattleLog.Inlines.Add(new Run(HP1.ToString()) { Foreground = Brushes.Red });
            BattleLog.Inlines.Add(new Run(" zostało)") { Foreground = Brushes.Red });
            BattleLog.Inlines.Add(Environment.NewLine);
        }

        private void MsgAtakOdpartyXP()
        {
            BattleLog.Inlines.Add(EnemyViewModel.Enemy.Nickname);
            BattleLog.Inlines.Add(new Run(" atakuje ") { Foreground = Brushes.Red });
            BattleLog.Inlines.Add(PlayerViewModel.Player.Nickname);
            BattleLog.Inlines.Add(new Run(", lecz ten odpiera atak! ") { Foreground = Brushes.Red });
            BattleLog.Inlines.Add(Environment.NewLine);
        }

        private void MsgAtakNietrafiony()
        {
            BattleLog.Inlines.Add(EnemyViewModel.Enemy.Nickname);
            BattleLog.Inlines.Add(new Run(" uniknął ataku ") { Foreground = Brushes.Red });
            BattleLog.Inlines.Add(PlayerViewModel.Player.Nickname);
            BattleLog.Inlines.Add(Environment.NewLine);
        }

        private void MsgAtak(int HP2, int damage)
        {
            BattleLog.Inlines.Add(PlayerViewModel.Player.Nickname);
            BattleLog.Inlines.Add(new Run(" atakuje ") { Foreground = Brushes.Green });
            BattleLog.Inlines.Add(EnemyViewModel.Enemy.Nickname);
            BattleLog.Inlines.Add(new Run(" i zadaje ") { Foreground = Brushes.Green });
            BattleLog.Inlines.Add(damage.ToString());
            BattleLog.Inlines.Add(new Run(" obrażeń! (") { Foreground = Brushes.Green });
            BattleLog.Inlines.Add(new Run(HP2.ToString()) { Foreground = Brushes.Green });
            BattleLog.Inlines.Add(new Run(" zostało)") { Foreground = Brushes.Green });
            BattleLog.Inlines.Add(Environment.NewLine);
        }

        private void MsgAtakKrytyczny(int HP2, int damage)
        {
            BattleLog.Inlines.Add(PlayerViewModel.Player.Nickname);
            BattleLog.Inlines.Add(new Run(" w przypływie szału walki atakuje ") { Foreground = Brushes.Green });
            BattleLog.Inlines.Add(EnemyViewModel.Enemy.Nickname);
            BattleLog.Inlines.Add(new Run(" i zadaje ") { Foreground = Brushes.Green });
            BattleLog.Inlines.Add(damage.ToString());
            BattleLog.Inlines.Add(new Run(" obrażeń! (") { Foreground = Brushes.Green });
            BattleLog.Inlines.Add(new Run(HP2.ToString()) { Foreground = Brushes.Green });
            BattleLog.Inlines.Add(new Run(" zostało)") { Foreground = Brushes.Green });
            BattleLog.Inlines.Add(Environment.NewLine);
        }

        private void MsgAtakOdparty()
        {
            BattleLog.Inlines.Add(PlayerViewModel.Player.Nickname);
            BattleLog.Inlines.Add(new Run(" atakuje ") { Foreground = Brushes.Green });
            BattleLog.Inlines.Add(EnemyViewModel.Enemy.Nickname);
            BattleLog.Inlines.Add(new Run(", lecz ten odpiera atak! ") { Foreground = Brushes.Green });
            BattleLog.Inlines.Add(Environment.NewLine);
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
    }
}