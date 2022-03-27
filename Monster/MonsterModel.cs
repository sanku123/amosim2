using AmoSim2.Player;
using AmoSim2.ViewModel;
using CommonServiceLocator;
using System;

namespace AmoSim2.Monster
{
    public class MonsterModel : ViewModelBase
    {
        public PlayerViewModel _player => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        public string Name { get; set; }

        public double Level { get; set; }

        public double Attack { get; set; }

        public double Defence { get; set; }

        public double Health { get; set; }

        public double MaxHealth { get; set; }

        public double Agility { get; set; }

        public double Experience { get; set; }

        public double MonsterHitChance
        {
            get
            {
                double chance = Math.Round((((Math.Log10((Agility + 200)) - Math.Log10((_player.Player.EvasionFull + 200))) * 500) + 50));
                if (chance > 98) chance = 98;
                else if (chance < 2) chance = 2;
                return chance;
            }
        }

        public double PlayerHitChance
        {
            get
            {
                double chance = Math.Round((((Math.Log10((_player.Player.HitAbility + 200)) - Math.Log10((Agility + 200))) * 500) + 50));
                if (chance > 98) chance = 98;
                else if (chance < 2) chance = 2;
                return chance;
            }
        }

        public MonsterModel(string name, double level, double attack, double defence, double health, double maxHealth, double agility, double experience)
        {
            Name = name;
            Level = level;
            Attack = attack;
            Defence = defence;
            Health = health;
            MaxHealth = maxHealth;
            Agility = agility;
            Experience = experience;
        }

        public MonsterModel(MonsterModel m)
        {
            Name = m.Name;
            Level = m.Level;
            Attack = m.Attack;
            Defence = m.Defence;
            Health = m.Health;
            MaxHealth = m.MaxHealth;
            Agility = m.Agility;
            Experience = m.Experience;
        }
    }
}