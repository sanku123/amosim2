using AmoSim2.Monster;
using AmoSim2.Others;
using AmoSim2.ViewModel;
using CommonServiceLocator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace AmoSim2.Player
{
    public partial class Model : ViewModelBase
    {
        [JsonIgnore]
        public readonly Random random = new Random();

        [JsonIgnore]
        public MonsterViewModel _monster => ServiceLocator.Current.GetInstance<MonsterViewModel>();

        public string Nickname { get; set; }

        public string Race { get; set; }

        public string Class { get; set; }

        [JsonIgnore]
        public string Build => Race + " " + Class;

        [JsonIgnore]
        public bool Barbarian
        {
            get
            {
                if (Class == "Barbarzyńca") return true;
                return false;
            }
        }

        public double KrasnoludzkaSzata { get; set; }

        public double HobbickaSzata { get; set; }

        [JsonIgnore]
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
                    return (double)Math.Round((0.25 + (Level / 5800)) * Strength + CombatSkill + TrafienieBless + Level + (Vampirism * (Level / 320)), 2);
                }
                else if (new[] { Strength, Agility, Inteligence }.Max() == Agility)
                {
                    return (double)Math.Round((0.25 + (Level / 1950)) * Agility + CombatSkill + TrafienieBless + Level + (Vampirism * (Level / 320)), 2);
                }
                else if (new[] { Strength, Agility, Inteligence }.Max() == Inteligence)
                {
                    return (double)Math.Round((0.25 + (Level / 1420)) * Inteligence + CombatSkill + TrafienieBless + Level + (Vampirism * (Level / 320)), 2);
                }
                return (double)Math.Round((0.25 + (Level / 5800)) * Strength + (0.25 + (Level / 1420)) * Inteligence + (0.25 + (Level / 1950)) * Agility + CombatSkill + TrafienieBless + Level + (Vampirism * (Level / 320)), 2);
            }
        }

        [JsonIgnore]
        public double Attack
        {
            get
            {
                if (new[] { Strength, Agility, Inteligence }.Max() == Strength)
                {
                    return Strength + WeaponDMG + HumanAttackBonus + DwarfAttackBonus;
                }
                else if (new[] { Strength, Agility, Inteligence }.Max() == Inteligence)
                {
                    return Inteligence * CzarDMG;
                }
                else return Agility * 0.8 + WeaponDMG;
            }
        }

        [JsonIgnore]
        public double ThiefDamagePenalty => Class == "Złodziej" ? 0.85 : 1;

        public double Critical()
        {
            int thief = random.Pick(new Dictionary<int, double>
            {
                { 110, 25 },
                { 115, 25 },
                { 120, 15 },
                { 130, 15 },
                { 140, 6 },
                { 150, 6 },
                { 160, 5 },
                { 9999, 3 }
            });

            int nonThief = random.Pick(new Dictionary<int, double>
            {
                { 110, 25 },
                { 115, 20 },
                { 120, 20 },
                { 130, 15 },
                { 140, 8 },
                { 150, 6 },
                { 160, 5 },
                { 9999, 1 }
            });

            int chance = Convert.ToInt32(CritChance) * 2;
            if ((chance >= random.Next(1, 201)) && (10 <= random.Next(1, 201)))
            {
                if (Class == "Złodziej" && Archer) return ((double)thief + 10) / 100;
                if (Class == "Złodziej") return (double)thief / 100;
                if (Archer) return ((double)nonThief + 10) / 100;
                return (double)nonThief / 100;
            }
            return 1;
        }

        [JsonIgnore]
        public double Defence
        {
            get
            {
                if (Class == "Mag")
                {
                    return WillPower + KrasnoludzkaSzata * CzarDEF + Math.Max(0, Math.Floor((Level - 200) / 2) * 15);
                }
                return Toughness + EQdefence + KrasnoludzkaSzata + BarbarianDefence + HumanDefenceFromEQ;
            }
        }

        [JsonIgnore]
        public double EvasionFull
        {
            get
            {
                double temp_evasion = 0.45 * Speed + Level + EvasionSkill + UnikiBless + (0.45 * WeaponSpeed) + EvasionArmor + EvasionLegs + HobbickaSzata + ThiefEvasion + MageEvasion;
                return Math.Round(temp_evasion, 2);
            }
        }

        [JsonIgnore]
        public double BattleSpeed => Level + WeaponSpeed + Speed * CzarSpeed * CzarSpeed2;

        [JsonIgnore]
        public bool Archer
        {
            get
            {
                if (new[] { Strength, Agility, Inteligence }.Max() == Agility) return true;
                return false;
            }
        }

        [JsonIgnore]
        public double ThiefEvasion
        {
            get
            {
                switch (Class)
                {
                    case "Złodziej" when Race == "Hobbit":
                        return 3 * Robbery;

                    case "Złodziej" when Race == "Wampir":
                        return Robbery;

                    case "Złodziej" when Race != "Hobbit" && Race != "Wampir":
                        return 2 * Robbery;

                    default:
                        return 0;
                }
            }
        }

        public double MageEvasion
        {
            get
            {
                switch (Class)
                {
                    case "Mag":
                        return 5 * Level;

                    case "Czarnoksiężnik":
                        return 3 * Level;

                    default:
                        return 0;
                }
            }
        }

        [JsonIgnore]
        public double BarbarianDefence
        {
            get
            {
                if (Class != "Barbarzyńca")
                {
                    return 0;
                }

                double val = 0;

                if (Level >= 1 && Level <= 100)
                {
                    val = 6 * Level;
                }
                else if (Level >= 101 && Level <= 200)
                {
                    val = 600 + (8 * (Level - 100));
                }
                else if (Level >= 201 && Level <= 300)
                {
                    val = 1400 + (10 * (Level - 200));
                }
                else if (Level >= 301)
                {
                    val = 2400 + (14 * (Level - 300));
                }

                return val;
            }
        }

        [JsonIgnore]
        public double HumanDefenceFromEQ => Race == "Człowiek" ? EQdefence * 1.1 : 0;

        [JsonIgnore]
        public double HumanAttackBonus => Race == "Człowiek" ? WeaponDMG * 1.05 : 0;

        [JsonIgnore]
        public double DwarfAttackBonus => Race == "Krasnolud" && Class == "Wojownik" ? WeaponDMG * 1.5 : 0;

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

        [JsonIgnore]
        public int BonusŁowcy => Class == "Łowca" ? (int)(Level / 2) : 0;

        [JsonIgnore]
        public double BlockChance
        {
            get
            {
                switch (Class)
                {
                    case "Wojownik" when Class != "Mag":
                        return Math.Floor(Level / 15);

                    case "Barbarzyńca" when Class == "Mag":
                        return Math.Floor(Level / 10);

                    default:
                        return 0;
                }
            }
        }

        private void ResetHealth()
        {
            HP = HPmax;
        }

        // COMBAT ROUND //
        public event PropertyChangedEventHandler PropertyChanged;

        [JsonIgnore]
        public double AverageSkill { get; set; }

        [JsonIgnore]
        public double AverageEvasion { get; set; }

        [JsonIgnore]
        public double Win { get; set; }

        [JsonIgnore]
        public double Lost { get; set; }

        [JsonIgnore]
        public string Mob { get; set; }

        [JsonIgnore]
        public double AveragePD { get; set; }

        [JsonIgnore]
        public double DailyEnergy

        {
            get
            {
                double enkaNaReset = 10 + (Level * 0.3 - 0.3);
                return enkaNaReset * 24 * 0.3 + Level + (enkaNaReset * 24);
            }
        }

        [JsonIgnore]
        public double ExpGranica => Level * Level * 50 * LevelFactor;

        [JsonIgnore]
        public double EnergiaLevel => Math.Max(0, Math.Round(ExpGranica / AveragePD + Lost));

        [JsonIgnore]
        public double SumaUBuniki => Math.Round(AverageSkill + AverageEvasion, 4);

        [JsonIgnore]
        public double UBdziennie => AverageSkill * DailyEnergy;

        [JsonIgnore]
        public double UnikiDziennie => AverageEvasion * DailyEnergy;

        [JsonIgnore]
        public double UBlevel => Math.Round(AverageSkill * EnergiaLevel);

        [JsonIgnore]
        public double UnikiLevel => Math.Round(AverageEvasion * EnergiaLevel);

        [JsonIgnore]
        public double SumaUBunikiLevel => Math.Round(SumaUBuniki * EnergiaLevel);

        [JsonIgnore]
        public double Koszt => Math.Round(EnergiaLevel / SumaUBunikiLevel, 4);
    }
}