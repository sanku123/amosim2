using AmoSim2.Others;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace AmoSim2.Player
{
    public class EnemyViewModel : CommandViewModel, INotifyPropertyChanged
    {
        private static EnemyViewModel _instance = new EnemyViewModel();

        public static EnemyViewModel Instance
        { get { return _instance; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public Model Enemy { get; set; }

        public List<string> Classes { get; set; }

        public List<string> Races { get; set; }

        public List<string> Talismans { get; set; }

        public List<string> Blessings { get; set; }

        public List<string> Robes { get; set; }

        public List<string> Kostury { get; set; }

        public List<string> Czary { get; set; }

        public DelegateCommand<string> SelectedTalismansChangedCommand { get; set; }

        public DelegateCommand<string> SelectedBlessingsChangedCommand { get; set; }

        public DelegateCommand<string> SelectedClassesChangedCommand { get; set; }

        public DelegateCommand<string> SelectedRacesChangedCommand { get; set; }

        public DelegateCommand<string> SelectedRobesChangedCommand { get; set; }

        public DelegateCommand<string> SelectedKosturyChangedCommand { get; set; }

        public DelegateCommand<string> SelectedCzaryChangedCommand { get; set; }

        public bool MageControlsVisibility { get; set; }

        public bool MeleeControlsVisibility { get; set; }

        public void ResetTalisman()
        {
            Enemy.StrengthTalizman = 1;
            Enemy.ToughnessTalizman = 1;
            Enemy.AgilityTalizman = 1;
            Enemy.SpeedTalizman = 1;
        }

        public void ResetSzata()
        {
            Enemy.KrasnoludzkaSzata = 0;
            Enemy.HobbickaSzata = 0;
        }

        public void ResetBlessings()
        {
            Enemy.StrengthBlessActive = false;
            Enemy.ToughnessBlessActive = false;
            Enemy.AgilityBlessActive = false;
            Enemy.SpeedBlessActive = false;
            Enemy.InteligenceBlessActive = false;
            Enemy.WillPowerBlessActive = false;
            Enemy.TrafienieBlessActive = false;
            Enemy.UnikiBlessActive = false;
        }

        public void ResetKostury()
        {
            Enemy.KosturDMG = 0;
            Enemy.KosturDEF = 0;
            Enemy.KosturSpeed = 0;
        }

        public void ResetSpells()
        {
            Enemy.CzarDMG = 1;
            Enemy.CzarSpeed2 = 1;
            Enemy.CzarDEF = 1;
            Enemy.CzarSpeed = 1;
        }

        public bool IsSelected { get; set; }

        public EnemyViewModel()
        {
            Enemy = new Model();

            Classes = new List<string>() { "Wojownik", "Paladyn", "Barbarzyńca", "Mag", "Złodziej", "Łowca", "Czarnoksiężnik" };

            Races = new List<string>() { "Człowiek", "Krasnolud", "Elf", "Jaszczuroczłek", "Hobbit", "Gnom", "Wampir", "Wilkołak" };

            Talismans = new List<string>() { "Siła + Wytka", "Siła + Szybkość", "Zręka + Wytka", "Zręka + Szybkość", "Wytka + Szybkość", "brak" };

            Blessings = new List<string>() { "Siła", "Wytrzymałość", "Zręczność", "Szybkość", "Inteligencja", "Siła Woli", "Trafienie", "Uniki", "brak" };

            Robes = new List<string>() { "Krasnoludzka", "Hobbicka", "brak" };

            Kostury = new List<string>() { "Laska", "Kostur", "Kantynówka", "brak" };

            Czary = new List<string>() { "1.5 SW", "1.1 SW 1.4 Speed", "1.5 INT", "1.4 INT 1.1 Speed", "brak" };

            SelectedTalismansChangedCommand = new DelegateCommand<string>((selectedTalismans) =>
            {
                ResetTalisman();
                if (selectedTalismans == "Siła + Wytka")
                {
                    Enemy.StrengthTalizman = 1.5;
                    Enemy.ToughnessTalizman = 1.5;
                }
                else if (selectedTalismans == "Siła + Szybkość")
                {
                    Enemy.StrengthTalizman = 1.5;
                    Enemy.SpeedTalizman = 1.5;
                }
                else if (selectedTalismans == "Zręka + Wytka")
                {
                    Enemy.AgilityTalizman = 1.5;
                    Enemy.ToughnessTalizman = 1.5;
                }
                else if (selectedTalismans == "Zręka + Szybkość")
                {
                    Enemy.AgilityTalizman = 1.5;
                    Enemy.SpeedTalizman = 1.5;
                }
                else if (selectedTalismans == "Wytka + Szybkość")
                {
                    Enemy.ToughnessTalizman = 1.5;
                    Enemy.SpeedTalizman = 1.5;
                }
                else if (selectedTalismans == "brak")
                {
                    ResetTalisman();
                }
            });

            SelectedBlessingsChangedCommand = new DelegateCommand<string>((selectedBlessings) =>
            {
                ResetBlessings();

                switch (selectedBlessings)
                {
                    case "Siła":
                        Enemy.StrengthBlessActive = true;
                        break;

                    case "Wytrzymałość":
                        Enemy.ToughnessBlessActive = true;
                        break;

                    case "Zręczność":
                        Enemy.AgilityBlessActive = true;
                        break;

                    case "Szybkość":
                        Enemy.SpeedBlessActive = true;
                        break;

                    case "Inteligencja":
                        Enemy.InteligenceBlessActive = true;
                        break;

                    case "Siła Woli":
                        Enemy.WillPowerBlessActive = true;
                        break;

                    case "Trafienie":
                        Enemy.TrafienieBlessActive = true;
                        break;

                    case "Uniki":
                        Enemy.UnikiBlessActive = true;
                        break;

                    case "brak":
                        ResetBlessings();
                        break;

                    default:
                        ResetBlessings();
                        break;
                }
            });

            SelectedClassesChangedCommand = new DelegateCommand<string>((selectedClass) =>
            {
                Enemy.Class = selectedClass;

                if (Enemy.Class == "Mag")
                {
                    MageControlsVisibility = true;
                    MeleeControlsVisibility = false;
                    ResetTalisman();
                }
                else
                {
                    MageControlsVisibility = false;
                    MeleeControlsVisibility = true;
                    ResetKostury();
                    ResetSpells();
                    ResetSzata();
                }
            });
            SelectedRacesChangedCommand = new DelegateCommand<string>((selectedRace) =>
            {
                Enemy.Race = selectedRace;
            });
            SelectedRobesChangedCommand = new DelegateCommand<string>((selectedRobe) =>
            {
                ResetSzata();

                if (selectedRobe == "Krasnoludzka")
                {
                    Enemy.KrasnoludzkaSzata = 2000;
                }
                else if (selectedRobe == "Hobbicka")
                {
                    Enemy.HobbickaSzata = 2000;
                }
                else if (selectedRobe == "brak")
                {
                    ResetSzata();
                }
            });
            SelectedKosturyChangedCommand = new DelegateCommand<string>((selectedKostur) =>
            {
                ResetKostury();

                if (selectedKostur == "Laska")
                {
                    Enemy.KosturDEF = 10000;
                }
                else if (selectedKostur == "Kostur")
                {
                    Enemy.KosturDMG = 5000;
                    Enemy.KosturDEF = 5000;
                }
                else if (selectedKostur == "Kantynówka")
                {
                    Enemy.KosturSpeed = 5000;
                    Enemy.KosturDEF = 5000;
                }
                else if (selectedKostur == "brak")
                {
                    ResetKostury();
                }
            });
            SelectedCzaryChangedCommand = new DelegateCommand<string>((selectedCzar) =>
            {
                ResetSpells();

                if (selectedCzar == "1.5 SW")
                {
                    Enemy.CzarDEF = 1.5;
                }
                else if (selectedCzar == "1.1 SW 1.4 Speed")
                {
                    Enemy.CzarDEF = 1.1;
                    Enemy.CzarSpeed = 1.4;
                }
                else if (selectedCzar == "1.5 INT")
                {
                    Enemy.CzarDMG = 1.5;
                }
                else if (selectedCzar == "1.4 INT 1.1 Speed")
                {
                    Enemy.CzarDMG = 1.4;
                    Enemy.CzarSpeed2 = 1.1;
                }
                else if (selectedCzar == "brak")
                {
                    ResetSpells();
                }
            });
            Enemy.Nickname = "Przeciwnik";
            Enemy.Race = "";
            Enemy.Class = "";
            Enemy.Level = 100;
            Enemy.HP = 3000;
            Enemy.MP = 500;

            Enemy.BaseStrength = 16;
            Enemy.BaseAgility = 4;
            Enemy.BaseInteligence = 4;
            Enemy.BaseSpeed = 4;
            Enemy.BaseWillPower = 4;
            Enemy.BaseToughness = 5000;

            Enemy.CombatSkill = 0.01;
            Enemy.EvasionSkill = 0.01;

            Enemy.Vampirism = 0.01;
            Enemy.WeaponSpeed = 0.01;
            Enemy.WeaponDMG = 100;
            Enemy.Robbery = 0.01;
            Enemy.EQdefence = 0.01;
            Enemy.EvasionLegs = 10;
            Enemy.EvasionArmor = 25;

            Enemy.StrengthBlessActive = false;
            Enemy.ToughnessBlessActive = false;
            Enemy.AgilityBlessActive = false;
            Enemy.SpeedBlessActive = false;
            Enemy.InteligenceBlessActive = false;
            Enemy.WillPowerBlessActive = false;
            Enemy.TrafienieBlessActive = false;
            Enemy.UnikiBlessActive = false;

            Enemy.KosturDEF = 0;
            Enemy.KosturDMG = 0;
            Enemy.KosturSpeed = 0;

            Enemy.CzarDEF = 1;
            Enemy.CzarDMG = 1;
            Enemy.CzarSpeed = 1;
            Enemy.CzarSpeed2 = 1;

            Enemy.StrengthTalizman = 1;
            Enemy.ToughnessTalizman = 1;
            Enemy.AgilityTalizman = 1;
            Enemy.SpeedTalizman = 1;

            Enemy.KrasnoludzkaSzata = 0;
            Enemy.HobbickaSzata = 0;
        }
    }
}