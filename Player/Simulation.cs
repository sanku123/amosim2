using System;
using AmoSim2.ViewModel;
using CommonServiceLocator;
using Newtonsoft.Json;

namespace AmoSim2.Player
{

    public partial class Model : ViewModelBase
    {

        [JsonIgnore]
        public PlayerViewModel PlayerViewModel => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        [JsonIgnore]
        public double PlayerHitChance => CalculateHitChance(PlayerViewModel.Player.HitAbility, PlayerViewModel.Enemy.EvasionFull);

        [JsonIgnore]
        public double EnemyHitChance => CalculateHitChance(PlayerViewModel.Enemy.HitAbility, PlayerViewModel.Player.EvasionFull);

        private double CalculateHitChance(double attackerAbility, double defenderEvasion)
        {
            double val = Math.Round(((Math.Log10(attackerAbility + 200) - Math.Log10(defenderEvasion + 200)) * 500) + 50, 2);
            return Math.Min(val, 98); 
        }
        public double PlayerInicjatywaBase => Math.Round(PlayerViewModel.Player.BattleSpeed / PlayerViewModel.Enemy.BattleSpeed, 3);

        public double EnemyInicjatywaBase => Math.Round(PlayerViewModel.Enemy.BattleSpeed / PlayerViewModel.Player.BattleSpeed, 3);

        [JsonIgnore]
        public double PlayerInicjatywa => GetInicjatywa(PlayerInicjatywaBase);

        [JsonIgnore]
        public double EnemyInicjatywa => GetInicjatywa(EnemyInicjatywaBase);

        private double GetInicjatywa(double baseValue)
        {
            return baseValue >= 4.2 ? 5 :
                   baseValue >= 3.2 ? 4 :
                   baseValue >= 2.2 ? 3 :
                   baseValue >= 1.15 ? 2 : 1;
        } 
    }
}