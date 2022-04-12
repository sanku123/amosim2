using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmoSim2.Player;
using CommonServiceLocator;
using Newtonsoft.Json;

namespace AmoSim2.Player
{
    public partial class Model
    {
        [JsonIgnore]
        public EnemyViewModel EnemyViewModel => ServiceLocator.Current.GetInstance<EnemyViewModel>();

        [JsonIgnore]
        public double PlayerInicjatywaBase => BattleSpeed / EnemyViewModel.Enemy.BattleSpeed;

        [JsonIgnore]
        public double EnemyInicjatywaBase => EnemyViewModel.Enemy.BattleSpeed / BattleSpeed;

        [JsonIgnore]
        public double PlayerInicjatywa
        {
            get
            {
                double val = BattleSpeed / EnemyViewModel.Enemy.BattleSpeed;
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
                double val = EnemyViewModel.Enemy.BattleSpeed / BattleSpeed;
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
                double val = Math.Round((((Math.Log10((HitAbility + 200)) - Math.Log10((EnemyViewModel.Enemy.EvasionFull + 200))) * 500) + 50));
                if (val < 2) return 2;
                if (val > 98) return 98;
                else return val;
            }
        }

        [JsonIgnore]
        public double EnemyHitChance
        {
            get
            {
                double val = Math.Round((((Math.Log10((EnemyViewModel.Enemy.HitAbility + 200)) - Math.Log10((EvasionFull + 200))) * 500) + 50));
                if (val < 2) return 2;
                if (val > 98) return 98;
                else return val;
            }
        }
    }
}