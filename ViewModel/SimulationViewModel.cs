﻿using AmoSim2.Others;
using AmoSim2.Player;
using CommonServiceLocator;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using RelayCommand = AmoSim2.Others.RelayCommand;

namespace AmoSim2.ViewModel
{
    public class SimulationViewModel : CommandViewModel
    {
        public PlayerViewModel PlayerViewModel => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        private readonly Random rnd = new Random();
        private DateTime startTime;

        private double _progressValue;
        public double ProgressValue
        {
            get { return _progressValue; }
            set { _progressValue = value; OnPropertyChanged(); }
        }


        private double _timeTakenInSeconds;
        public double TimeTakenInSeconds
        {
            get { return _timeTakenInSeconds; }
            set
            {
                if (_timeTakenInSeconds != value)
                {
                    _timeTakenInSeconds = value;
                    OnPropertyChanged(nameof(TimeTakenInSeconds));
                    UpdateProgressBar(); 
                }
            }
        }
        private void UpdateProgressBar()
        {
            double totalSimulationTime = 100; 
            double progressPercentage = Math.Min((TimeTakenInSeconds / totalSimulationTime) * 100, 100);
            ProgressValue = progressPercentage;
        }
        private void ReportProgress(double value)
        {
            ProgressValue = value;
        }
        private int _averageRounds;
        public int AverageRounds
        {
            get { return _averageRounds; }
            set { _averageRounds = value; OnPropertyChanged(); }
        }

        private int _winCount;
        public int WinCount
        {
            get { return _winCount; }
            set { _winCount = value; OnPropertyChanged(); }
        }

        private int _lostCount;
        public int LostCount
        {
            get { return _lostCount; }
            set { _lostCount = value; OnPropertyChanged(); }
        }

        private int _drawCount;
        public int DrawCount
        {
            get { return _drawCount; }
            set { _drawCount = value; OnPropertyChanged(); }
        }

        private string _winPercentageText;
        public string WinPercentageText
        {
            get { return _winPercentageText; }
            set { _winPercentageText = value; OnPropertyChanged(); }
        }

        private string _lostPercentageText;
        public string LostPercentageText
        {
            get { return _lostPercentageText; }
            set { _lostPercentageText = value; OnPropertyChanged(); }
        }

        private string _drawPercentageText;
        public string DrawPercentageText
        {
            get { return _drawPercentageText; }
            set { _drawPercentageText = value; OnPropertyChanged(); }
        }

        public ICommand StartSimulationCommand { get; }

        public SimulationViewModel()
        {
            StartSimulationCommand = new RelayCommand(StartSimulation);
        }

        private void StartSimulation(object parameter)
        {
            AverageRounds = 0;
            WinCount = 0;
            LostCount = 0;
            DrawCount = 0;

            int iterations = 20000;

            ProgressValue = 0;
            startTime = DateTime.Now;

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (sender, e) =>
            {

                for (int i = 0; i < iterations; i++)
                {
                    BeginFastSimulation();
                    double progress = ((double)i / iterations) * 100;
                    worker.ReportProgress((int)progress);
                }
                AverageRounds /= iterations;
            };
            worker.ProgressChanged += (sender, e) =>
            {
                ReportProgress(e.ProgressPercentage);
            };
            worker.RunWorkerCompleted += (sender, e) =>
            {
                TimeSpan duration = DateTime.Now - startTime;

                double seconds = duration.TotalSeconds;

                TimeTakenInSeconds = seconds;

                double winPercentage = (double)WinCount / iterations * 100;
                double lostPercentage = (double)LostCount / iterations * 100;
                double drawPercentage = (double)DrawCount / iterations * 100;

                WinPercentageText = winPercentage.ToString("0.00") + "%";
                LostPercentageText = lostPercentage.ToString("0.00") + "%";
                DrawPercentageText = drawPercentage.ToString("0.00") + "%";
            };
            worker.RunWorkerAsync();
        }

        private void BeginFastSimulation()
        {
            var player = PlayerViewModel.Player;
            var enemy = PlayerViewModel.Enemy;

            int playerHealthPoints = (int)player.HP + (int)player.HP_Bonus;
            int enemyHealthPoints = (int)enemy.HP + (int)enemy.HP_Bonus;

            PlayerViewModel.Player.PlayerHitChance = Math.Max(PlayerViewModel.Player.PlayerHitChance, 2);

            bool playerGoesFirst = player.BattleSpeed > enemy.BattleSpeed;

            for (int i = 1; i <= 24; i++)
            {
                AverageRounds++;
                if (playerGoesFirst)
                {
                    enemyHealthPoints = PerformPlayerAttack(enemyHealthPoints, player, enemy);
                    if (enemyHealthPoints < 1)
                    {
                        WinCount += 1;
                        return;
                    }

                    playerHealthPoints = PerformEnemyAttack(playerHealthPoints, enemy, player);
                    if (playerHealthPoints < 1)
                    {
                        LostCount += 1;
                        return;
                    }
                }
                else
                {
                    playerHealthPoints = PerformEnemyAttack(playerHealthPoints, enemy, player);
                    if (playerHealthPoints < 1)
                    {
                        LostCount += 1;
                        return;
                    }

                    enemyHealthPoints = PerformPlayerAttack(enemyHealthPoints, player, enemy);
                    if (enemyHealthPoints < 1)
                    {
                        WinCount += 1;
                        return;
                    }
                }

                if (i == 24)
                {
                    DrawCount += 1;
                    return;
                }
            }
        }

        private int PerformPlayerAttack(int targetHP, Model player, Model enemy)
        {
            double hitChance = Math.Max(player.PlayerHitChance, 2);
            for (int i = 0; i < player.PlayerInicjatywa && (targetHP > 0); i++)
            {
                if (hitChance < rnd.Next(1, 101))
                    continue;

                if (enemy.BlockChance >= rnd.Next(1, 101))
                    continue;

                int damage = CalculateDamage(player, enemy);

                if (damage > 0 && player.Class == "Czarnoksiężnik" || player.Class == "Mag" && enemy.Race == "Jaszczuroczłek")
                    damage = (int)(damage * 0.95);

                targetHP -= damage;
            }

            return targetHP;
        }

        private int PerformEnemyAttack(int targetHP, Model enemy, Model player)
        {
            double hitChance = Math.Max(enemy.EnemyHitChance, 2);

            for (int i = 0; i < enemy.EnemyInicjatywa && (targetHP > 0); i++)
            {
                if (hitChance < rnd.Next(1, 101))
                    continue;

                if (player.BlockChance >= rnd.Next(1, 101))
                    continue;

                int damage = CalculateDamage(enemy, player);

                if (damage > 0 && enemy.Class == "Czarnoksiężnik" || enemy.Class == "Mag" && player.Race == "Jaszczuroczłek")
                    damage = (int)(damage * 0.95);

                targetHP -= damage;
            }

            return targetHP;
        }
        private int CalculateDamage(Model attacker, Model defender)
        {
            int baseDamage = (int)(attacker.Attack + rnd.Next(1, (int)(5 * attacker.Level)));
            double warlockDefenceBreak = 0;

            int bonusDamage = 0;
            if (attacker.Class == "Łowca" && defender.Race != "Jaszczuroczłek")
                bonusDamage = attacker.BonusŁowcy;
            else if (attacker.Class == "Czarnoksiężnik")
                bonusDamage = attacker.WarlockPoisonDamage;

            if (attacker.Class == "Czarnoksiężnik" && defender.Race == "Krasnolud")
                warlockDefenceBreak = defender.Defence - (int)(defender.Defence * (1 - (Math.Floor(attacker.Level/50)/100)));
            else if (attacker.Class == "Czarnoksiężnik")
                warlockDefenceBreak = defender.Defence - (int)(defender.Defence * (1 - (Math.Floor(attacker.Level/25)/100)));

            int calculatedDamage = (int)Math.Max(0, baseDamage * attacker.Critical() * attacker.ThiefDamagePenalty - defender.Defence - warlockDefenceBreak);

            return calculatedDamage + bonusDamage;
        }
    }

}
