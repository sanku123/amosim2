﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmoSim2.Player;
using AmoSim2.ViewModel;
using CommonServiceLocator;
using Newtonsoft.Json;
using PropertyChanged;

namespace AmoSim2.Player
{
    
    public partial class Model : ViewModelBase
    {
        [JsonIgnore]
        public EnemyViewModel EnemyViewModel => ServiceLocator.Current.GetInstance<EnemyViewModel>();

        [JsonIgnore]
        public PlayerViewModel PlayerViewModel => ServiceLocator.Current.GetInstance<PlayerViewModel>();


        private double _playerInicjatywaBase;
        public double PlayerInicjatywaBase
        {
            get
            {
                return Math.Round(PlayerViewModel.Player.BattleSpeed / EnemyViewModel.Enemy.BattleSpeed, 3);
            }
            set
            {
                _playerInicjatywaBase = value;
                OnPropertyChanged();
            }
        }

        private double _enemyInicjatywaBase;
        public double EnemyInicjatywaBase
        {
            get
            {
                return Math.Round(EnemyViewModel.Enemy.BattleSpeed / PlayerViewModel.Player.BattleSpeed, 3);
            }
            set
            {
                _enemyInicjatywaBase = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        public double PlayerInicjatywa => GetInicjatywa(PlayerInicjatywaBase);

        [JsonIgnore]
        public double EnemyInicjatywa => GetInicjatywa(EnemyInicjatywaBase);

        private double GetInicjatywa(double baseValue)
        {
            if (baseValue >= 4.2) return 5;
            else if (baseValue >= 3.2) return 4;
            else if (baseValue >= 2.2) return 3;
            else if (baseValue >= 1.15) return 2;
            else return 1;
        }


        [JsonIgnore]
        public double PlayerHitChance => CalculateHitChance(PlayerViewModel.Player.HitAbility, EnemyViewModel.Enemy.EvasionFull);

        [JsonIgnore]
        public double EnemyHitChance => CalculateHitChance(EnemyViewModel.Enemy.HitAbility, PlayerViewModel.Player.EvasionFull);

        private double CalculateHitChance(double attackerAbility, double defenderEvasion)
        {
            double val = Math.Round(((Math.Log10(attackerAbility + 200) - Math.Log10(defenderEvasion + 200)) * 500) + 50, 2);
            return Math.Min(val, 98); // Ensure the value does not exceed 98
        }
    }
}