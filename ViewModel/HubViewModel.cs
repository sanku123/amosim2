using AmoSim2.Others;
using AmoSim2.Player;
using CommonServiceLocator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmoSim2.ViewModel
{
    public class HubViewModel : CommandViewModel
    {
        public static HubViewModel Instance { get; } = new HubViewModel();
        public PlayerViewModel PlayerViewModel => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        public EnemyViewModel EnemyViewModel => ServiceLocator.Current.GetInstance<EnemyViewModel>();

        private double _playerInicjatywaBase;
        public double PlayerInicjatywaBase
        {
            get
            {
                return PlayerViewModel.Player.BattleSpeed / EnemyViewModel.Enemy.BattleSpeed;
            }
            set
            {
                if (value != _playerInicjatywaBase)
                {
                    _playerInicjatywaBase = value;
                    OnPropertyChanged();
                }
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
                if (value != _enemyInicjatywaBase)
                {
                    _enemyInicjatywaBase = value;
                    OnPropertyChanged();
                }
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
        public HubViewModel()
        {
            
        }
    }
}
