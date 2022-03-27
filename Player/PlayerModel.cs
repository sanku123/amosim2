using AmoSim2.Battle;
using AmoSim2.Monster;
using CommonServiceLocator;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace AmoSim2.Player
{
    public partial class PlayerModel : INotifyPropertyChanged
    {
        public readonly Random random = new Random();
        public BattleModel Battle;
        public MonsterViewModel _monster => ServiceLocator.Current.GetInstance<MonsterViewModel>();

        public string AttrType { get; set; }

        public string Nickname { get; set; }

        public string Race { get; set; }

        public string Class { get; set; }

        public bool Barbarian { get; set; }

        public double KrasnoludzkaSzata { get; set; }

        public double HobbickaSzata { get; set; }

        public Page SlideFrame { get; set; }

        public double Level { get; set; }

        public double HP { get; set; }

        public double HPmax { get; set; }

        public double MP { get; set; }

        public double CombatSkill { get; set; }

        public double EvasionSkill { get; set; }

        public double Vampirism { get; set; }

        public double WeaponDMG { get; set; }

        public double WeaponSpeed { get; set; }

        public double Robbery { get; set; }

        public double EQdefence { get; set; }

        public double EvasionLegs { get; set; }

        public double EvasionArmor { get; set; }

        [JsonIgnore]
        public double HitAbility

        {
            get
            {
                if (new[] { Strength, Agility, Inteligence }.Max() == Strength)
                {
                    return (double)Math.Round((0.25 + (Level / 5800)) * Strength + CombatSkill + Level + (Vampirism / 2), 2);
                }
                else if (new[] { Strength, Agility, Inteligence }.Max() == Agility)
                {
                    return (double)Math.Round((0.25 + (Level / 1950)) * Agility + CombatSkill + Level + (Vampirism / 2), 2);
                }
                else if (new[] { Strength, Agility, Inteligence }.Max() == Inteligence)
                {
                    return (double)Math.Round((0.25 + (Level / 1420)) * Inteligence + CombatSkill + Level + (Vampirism / 2), 2);
                }
                return (double)Math.Round((0.25 + (Level / 5800)) * Strength + (0.25 + (Level / 1420)) * Inteligence + (0.25 + (Level / 1950)) * Agility + CombatSkill + Level + (Vampirism / 2), 2);
            }
        }

        [JsonIgnore]
        public double Attack
        {
            get
            {
                if (new[] { Strength, Agility, Inteligence }.Max() == Strength)
                {
                    return Strength + WeaponDMG;
                }
                else if (new[] { Strength, Agility, Inteligence }.Max() == Inteligence)
                {
                    return Inteligence;
                }
                else return Agility * 0.8 + WeaponDMG;
            }
        }

        public double Critical()
        {
            double chance = Convert.ToDouble(CritChance) * 2;
            double P1CritValue = random.Next(1, 201);
            if ((chance >= random.Next(1, 201)) && (10 <= random.Next(1, 201)))
            {
                if ((P1CritValue >= 1) && (P1CritValue <= 50)) return 1.1;
                else if ((P1CritValue >= 51) && (P1CritValue <= 100)) return 1.15;
                else if ((P1CritValue >= 101) && (P1CritValue <= 140)) return 1.2;
                else if ((P1CritValue >= 141) && (P1CritValue <= 170)) return 1.3;
                else if ((P1CritValue >= 171) && (P1CritValue <= 186)) return 1.4;
                else if ((P1CritValue >= 187) && (P1CritValue <= 198)) return 1.5;
                else if (P1CritValue >= 199) return 99999;
            }
            return 1;
        }

        [JsonIgnore]
        public double Defence => Toughness + EQdefence;

        [JsonIgnore]
        public double EvasionFull
        {
            get
            {
                double temp_evasion = (0.45 * Speed + EvasionSkill + Level) - ((0.45 * Speed + EvasionSkill + Level) * 25 / 100);
                return Math.Round(temp_evasion - (temp_evasion * 10 / 100) + (0.45 * WeaponSpeed));
            }
        }

        [JsonIgnore]
        public double BattleSpeed
        {
            get
            {
                return Level + WeaponSpeed + (0.45 * Speed);
            }
        }

        [JsonIgnore]
        public double CritChance => (double)Math.Min(25, (Math.Floor(((Math.Sqrt((CombatSkill * 0.0655737704918033) + 1) - 1) / 2) * 2)) / 2);

        [JsonIgnore]
        public double EnergyGain => (double)(10 + (Level * 0.3) - 0.3);

        [JsonIgnore]
        public double Noclegownia => 7.2 * EnergyGain;

        [JsonIgnore]
        public double Studnia => Level;

        [JsonIgnore]
        public double Akademia => Math.Round(5 + Math.Ceiling(Convert.ToDouble(Level / 10)) + 0.3, 2);

        [JsonIgnore]
        public double LevelFactor
        {
            get
            {
                if (Level <= 49) return 1;
                else if (Level <= 99) return 2;
                else if (Level <= 149) return 4;
                else if (Level <= 199) return 5;
                else if (Level <= 249) return 8;
                else if (Level <= 299) return 9;
                else if (Level <= 349) return 11;
                return 1;
            }
        }

        private void ResetHealth()
        {
            HP = HPmax;
        }

        // COMBAT ROUND //

        public double AverageSkill { get; set; }

        public double AverageEvasion { get; set; }

        public double Win { get; set; }

        public double Lost { get; set; }

        public string Mob { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public double AveragePD { get; set; }

        [JsonIgnore]
        public double DailyEnergy

        {
            get
            {
                double enkaNaReset = 10 + (Level * 0.3 - 0.3);
                return (enkaNaReset * 24) * 0.3 + Level + (enkaNaReset * 24);
            }
        }

        [JsonIgnore]
        public double ExpGranica
        {
            get { return Level * Level * 50 * LevelFactor; }
        }

        [JsonIgnore]
        public double EnergiaLevel
        {
            get { return Math.Max(0, Math.Round(ExpGranica / AveragePD + Lost)); }
        }

        [JsonIgnore]
        public double SumaUBuniki
        {
            get { return Math.Round(AverageSkill + AverageEvasion, 4); }
        }

        [JsonIgnore]
        public double UBdziennie
        {
            get { return AverageSkill * DailyEnergy; }
        }

        [JsonIgnore]
        public double UnikiDziennie
        {
            get { return AverageEvasion * DailyEnergy; }
        }

        [JsonIgnore]
        public double UBlevel
        {
            get { return Math.Round(AverageSkill * EnergiaLevel); }
        }

        [JsonIgnore]
        public double UnikiLevel
        {
            get { return Math.Round(AverageEvasion * EnergiaLevel); }
        }

        [JsonIgnore]
        public double SumaUBunikiLevel
        {
            get { return Math.Round(SumaUBuniki * EnergiaLevel); }
        }

        [JsonIgnore]
        public double Koszt
        {
            get { return Math.Round(EnergiaLevel / SumaUBunikiLevel, 4); }
        }
    }
}