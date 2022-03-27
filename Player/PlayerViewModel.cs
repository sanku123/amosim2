using AmoSim2.Battle;
using AmoSim2.Others;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AmoSim2.Player
{
    public class PlayerViewModel : CommandViewModel, INotifyPropertyChanged
    {
        public BattleModel Battle;

        public event PropertyChangedEventHandler PropertyChanged;

        public PlayerModel Player { get; set; }

        public ObservableCollection<BattleResults> BattleData { get; set; }

        //public ICommand MyCommand { get; set; }
        public Command MyCommand { get; } = new Command((pObj, pVM) =>
           {
           }
        );

        public void ResetBlessings()
        {
            //Player.StrengthBless = 0;
            //Player.ToughnessBless = 0;
            Player.AgilityBless = 0;
            Player.SpeedBless = 0;
            Player.InteligenceBless = 0;
            Player.WillPowerBless = 0;
            Player.TrafienieBless = 0;
            Player.UnikiBless = 0;
        }

        public PlayerViewModel()
        {
            Player = new PlayerModel();
            Player.Nickname = "Vratimir Bezarith";
            Player.Race = "";
            Player.Class = "";
            Player.Barbarian = false;
            Player.Level = 100;
            Player.HP = 200;
            Player.MP = 500;

            Player.BaseStrength = 16;
            Player.BaseAgility = 4;
            Player.BaseInteligence = 4;
            Player.BaseSpeed = 4;
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

            Player.StrengthBless = 0;
            Player.ToughnessBless = 0;
            Player.AgilityBless = 0;
            Player.SpeedBless = 0;
            Player.InteligenceBless = 0;
            Player.WillPowerBless = 0;
            Player.TrafienieBless = 0;
            Player.UnikiBless = 0;

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
        }
    }
}