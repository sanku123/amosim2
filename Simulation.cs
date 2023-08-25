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
                return PlayerViewModel.Player.BattleSpeed / EnemyViewModel.Enemy.BattleSpeed;
            }
            set
            {
                _playerInicjatywaBase = value;
                OnPropertyChanged("EnemyInicjatywaBase");
            }
        }

        private double _enemyInicjatywaBase;
        public double EnemyInicjatywaBase
        {
            get
            {
                return EnemyViewModel.Enemy.BattleSpeed / PlayerViewModel.Player.BattleSpeed;
            }
            set
            {
                _enemyInicjatywaBase = value;
                OnPropertyChanged("PlayerInicjatywaBase");
            }
        }
        [JsonIgnore]
        public double PlayerInicjatywa
        {
            get
            {
                double val = PlayerInicjatywaBase;
                if (val >= 4.2) return 5;
                else if (val >= 3.2 && val < 4.2) return 4;
                else if (val >= 2.2 && val < 3.2) return 3;
                else if (val >= 1.15 && val < 2.2) return 2;
                else return 1;
            }
        }

        [JsonIgnore]
        public double EnemyInicjatywa
        {
            get
            {
                double val = EnemyInicjatywaBase;
                if (val >= 4.2) return 5;
                else if (val >= 3.2 && val < 4.2) return 4;
                else if (val >= 2.2 && val < 3.2) return 3;
                else if (val >= 1.15 && val < 2.2) return 2;
                else return 1;
            }
        }

        [JsonIgnore]
        public double PlayerHitChance
        {
            get
            {
                double val = Math.Round(((Math.Log10(PlayerViewModel.Player.HitAbility + 200) - Math.Log10(EnemyViewModel.Enemy.EvasionFull + 200)) * 500) + 50, 2);
                if (val > 98) return 98;
                else return val;
            }
        }

        [JsonIgnore]
        public double EnemyHitChance
        {
            get
            {
                double val = Math.Round(((Math.Log10(EnemyViewModel.Enemy.HitAbility + 200) - Math.Log10(PlayerViewModel.Player.EvasionFull + 200)) * 500) + 50, 2);
                if (val > 98) return 98;
                else return val;
            }
        }
    }
}