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
                    UpdateProgressBar(); // Call method to update ProgressBar when time taken changes
                }
            }
        }
        private void UpdateProgressBar()
        {
            // Update the progress directly based on the time taken
            // Example: assuming the simulation takes 100 seconds
            double totalSimulationTime = 100; // Adjust according to your scenario
            double progressPercentage = Math.Min((TimeTakenInSeconds / totalSimulationTime) * 100, 100);
            ProgressValue = progressPercentage;
        }
        private void ReportProgress(double value)
        {
            ProgressValue = value;
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
            WinCount = 0;
            LostCount = 0;
            DrawCount = 0;

            int iterations = 20000;

            // Reset progress value
            ProgressValue = 0;

            // Capture the start time
            startTime = DateTime.Now;

            // Run simulation in a background worker

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (sender, e) =>
            {

                for (int i = 0; i < iterations; i++)
                {
                    BeginFastSimulation();

                    // Report progress
                    double progress = ((double)i / iterations) * 100;
                    worker.ReportProgress((int)progress);
                }
            };
            worker.ProgressChanged += (sender, e) =>
            {
                // Update progress bar
                ReportProgress(e.ProgressPercentage);
            };
            worker.RunWorkerCompleted += (sender, e) =>
            {
                // Calculate duration after the background worker has completed
                TimeSpan duration = DateTime.Now - startTime;

                // Convert duration to seconds
                double seconds = duration.TotalSeconds;

                // Update TimeTakenInSeconds property
                TimeTakenInSeconds = seconds;

                // Simulation completed, update UI or do any post-processing
                // Calculate percentages and update UI
                double winPercentage = (double)WinCount / iterations * 100;
                double lostPercentage = (double)LostCount / iterations * 100;
                double drawPercentage = (double)DrawCount / iterations * 100;

                // Update properties
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

            int playerHealthPoints = (int)player.HP;
            int enemyHealthPoints = (int)enemy.HP;

            bool playerGoesFirst = player.BattleSpeed > enemy.BattleSpeed;

            for (int i = 1; i <= 24; i++)
            {
                if (playerGoesFirst)
                {
                    enemyHealthPoints = PerformAttack(enemyHealthPoints, player, enemy);
                    if (enemyHealthPoints < 1)
                    {
                        WinCount += 1;
                        return;
                    }

                    playerHealthPoints = PerformAttack(playerHealthPoints, enemy, player);
                    if (playerHealthPoints < 1)
                    {
                        LostCount += 1;
                        return;
                    }
                }
                else
                {
                    playerHealthPoints = PerformAttack(playerHealthPoints, enemy, player);
                    if (playerHealthPoints < 1)
                    {
                        LostCount += 1;
                        return;
                    }

                    enemyHealthPoints = PerformAttack(enemyHealthPoints, player, enemy);
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


        private int PerformAttack(int targetHP, Model attacker, Model defender)
        {
            for (int i = 0; i < attacker.PlayerInicjatywa && (targetHP > 0); i++)
            {
                if (attacker.PlayerHitChance < rnd.Next(1, 101))
                    continue;

                if (defender.BlockChance >= rnd.Next(1, 101))
                    continue;

                int damage = CalculateDamage(attacker, defender);
                targetHP -= damage;
            }

            return targetHP;
        }

        private int CalculateDamage(Model attacker, Model defender)
        {
            int baseDamage = (int)(attacker.Attack + rnd.Next(1, (int)(5 * attacker.Level)));

            int bonusDamage = 0;
            if (attacker.Class == "Łowca" && defender.Race != "Jaszczuroczłek")
                bonusDamage = attacker.BonusŁowcy;
            else if (attacker.Class == "Czarnoksiężnik")
                bonusDamage = attacker.WarlockPoisonDamage;

            int calculatedDamage = (int)Math.Max(0, baseDamage * attacker.Critical() * attacker.ThiefDamagePenalty - defender.Defence);
            return calculatedDamage + bonusDamage;
        }
    }

}
