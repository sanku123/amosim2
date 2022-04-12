using AmoSim2.Others;
using GalaSoft.MvvmLight.Command;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace AmoSim2.Player
{
    public class PlayerViewModel : CommandViewModel, INotifyPropertyChanged
    {
        private static PlayerViewModel _instance = new PlayerViewModel();

        public static PlayerViewModel Instance
        { get { return _instance; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public Model Player { get; set; }

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
            Player.StrengthTalizman = 1;
            Player.ToughnessTalizman = 1;
            Player.AgilityTalizman = 1;
            Player.SpeedTalizman = 1;
        }

        public void ResetSzata()
        {
            Player.KrasnoludzkaSzata = 0;
            Player.HobbickaSzata = 0;
        }

        public void ResetBlessings()
        {
            Player.StrengthBlessActive = false;
            Player.ToughnessBlessActive = false;
            Player.AgilityBlessActive = false;
            Player.SpeedBlessActive = false;
            Player.InteligenceBlessActive = false;
            Player.WillPowerBlessActive = false;
            Player.TrafienieBlessActive = false;
            Player.UnikiBlessActive = false;
        }

        public void ResetKostury()
        {
            Player.KosturDMG = 0;
            Player.KosturDEF = 0;
            Player.KosturSpeed = 0;
        }

        public void ResetSpells()
        {
            Player.CzarDMG = 1;
            Player.CzarSpeed2 = 1;
            Player.CzarDEF = 1;
            Player.CzarSpeed = 1;
        }

        public bool IsSelected { get; set; }

        public PlayerViewModel()
        {
            Player = new Model();

            Talismans = new List<string>() { "Siła + Wytka", "Siła + Szybkość", "Zręka + Wytka", "Zręka + Szybkość", "Wytka + Szybkość", "brak" };

            Blessings = new List<string>() { "Siła", "Wytrzymałość", "Zręczność", "Szybkość", "Inteligencja", "Siła Woli", "Trafienie", "Uniki", "brak" };

            Classes = new List<string>() { "Wojownik", "Paladyn", "Barbarzyńca", "Mag", "Złodziej", "Łowca", "Czarnoksiężnik" };

            Races = new List<string>() { "Człowiek", "Krasnolud", "Elf", "Jaszczuroczłek", "Hobbit", "Gnom", "Wampir", "Wilkołak" };

            Kostury = new List<string>() { "Laska", "Kostur", "Kantynówka", "brak" };

            Czary = new List<string>() { "1.5 SW", "1.1 SW 1.4 Speed", "1.5 INT", "1.4 INT 1.1 Speed", "brak" };

            Robes = new List<string>() { "Krasnoludzka", "Hobbicka", "brak" };

            Player.Nickname = "Gracz";
            Player.Race = "";
            Player.Class = "";
            Player.Level = 100;
            Player.HP = 5555;
            Player.MP = 500;

            Player.BaseStrength = 3000;
            Player.BaseAgility = 4;
            Player.BaseInteligence = 4;
            Player.BaseSpeed = 4000;
            Player.BaseWillPower = 4;
            Player.BaseToughness = 4;

            Player.CombatSkill = 0.01;
            Player.EvasionSkill = 0.01;

            Player.Vampirism = 0.01;
            Player.WeaponSpeed = 0.01;
            Player.WeaponDMG = 100;
            Player.Robbery = 0.01;
            Player.EQdefence = 0.01;
            Player.EvasionLegs = 10;
            Player.EvasionArmor = 25;

            Player.StrengthBlessActive = false;
            Player.ToughnessBlessActive = false;
            Player.AgilityBlessActive = false;
            Player.SpeedBlessActive = false;
            Player.InteligenceBlessActive = false;
            Player.WillPowerBlessActive = false;
            Player.TrafienieBlessActive = false;
            Player.UnikiBlessActive = false;

            Player.KosturDEF = 0;
            Player.KosturDMG = 0;
            Player.KosturSpeed = 0;

            Player.CzarDEF = 1;
            Player.CzarDMG = 1;
            Player.CzarSpeed = 1;
            Player.CzarSpeed2 = 1;

            Player.StrengthTalizman = 1;
            Player.ToughnessTalizman = 1;
            Player.AgilityTalizman = 1;
            Player.SpeedTalizman = 1;

            Player.KrasnoludzkaSzata = 0;
            Player.HobbickaSzata = 0;

            ////

            SelectedTalismansChangedCommand = new DelegateCommand<string>((selectedTalismans) =>
            {
                ResetTalisman();
                if (selectedTalismans == "Siła + Wytka")
                {
                    Player.StrengthTalizman = 1.5;
                    Player.ToughnessTalizman = 1.5;
                }
                else if (selectedTalismans == "Siła + Szybkość")
                {
                    Player.StrengthTalizman = 1.5;
                    Player.SpeedTalizman = 1.5;
                }
                else if (selectedTalismans == "Zręka + Wytka")
                {
                    Player.AgilityTalizman = 1.5;
                    Player.ToughnessTalizman = 1.5;
                }
                else if (selectedTalismans == "Zręka + Szybkość")
                {
                    Player.AgilityTalizman = 1.5;
                    Player.SpeedTalizman = 1.5;
                }
                else if (selectedTalismans == "Wytka + Szybkość")
                {
                    Player.ToughnessTalizman = 1.5;
                    Player.SpeedTalizman = 1.5;
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
                        Player.StrengthBlessActive = true;
                        break;

                    case "Wytrzymałość":
                        Player.ToughnessBlessActive = true;
                        break;

                    case "Zręczność":
                        Player.AgilityBlessActive = true;
                        break;

                    case "Szybkość":
                        Player.SpeedBlessActive = true;
                        break;

                    case "Inteligencja":
                        Player.InteligenceBlessActive = true;
                        break;

                    case "Siła Woli":
                        Player.WillPowerBlessActive = true;
                        break;

                    case "Trafienie":
                        Player.TrafienieBlessActive = true;
                        break;

                    case "Uniki":
                        Player.UnikiBlessActive = true;
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
                Player.Class = selectedClass;

                if (Player.Class == "Mag")
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
                Player.Race = selectedRace;
            });
            SelectedRobesChangedCommand = new DelegateCommand<string>((selectedRobe) =>
            {
                ResetSzata();

                if (selectedRobe == "Krasnoludzka")
                {
                    Player.KrasnoludzkaSzata = 2000;
                }
                else if (selectedRobe == "Hobbicka")
                {
                    Player.HobbickaSzata = 2000;
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
                    Player.KosturDEF = 10000;
                }
                else if (selectedKostur == "Kostur")
                {
                    Player.KosturDMG = 5000;
                    Player.KosturDEF = 5000;
                }
                else if (selectedKostur == "Kantynówka")
                {
                    Player.KosturSpeed = 5000;
                    Player.KosturDEF = 5000;
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
                    Player.CzarDEF = 1.5;
                }
                else if (selectedCzar == "1.1 SW 1.4 Speed")
                {
                    Player.CzarDEF = 1.1;
                    Player.CzarSpeed = 1.4;
                }
                else if (selectedCzar == "1.5 INT")
                {
                    Player.CzarDMG = 1.5;
                }
                else if (selectedCzar == "1.4 INT 1.1 Speed")
                {
                    Player.CzarDMG = 1.4;
                    Player.CzarSpeed2 = 1.1;
                }
                else if (selectedCzar == "brak")
                {
                    ResetSpells();
                }
            });
        }
    }
}