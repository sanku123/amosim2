using AmoSim2.Battle;
using Prism.Commands;
using System.Windows.Input;

namespace AmoSim2.Player
{
    public class EnemyViewModel
    {
        public BattleModel Battle;
        public PlayerModel Player { get; set; }

        public PlayerModel Enemy { get; set; }

        public ICommand MyCommand { get; set; }

        public void OnRectangleClicked()
        {
            Enemy.Level += 1;
        }

        public EnemyViewModel()
        {
            MyCommand = new DelegateCommand(OnRectangleClicked);

            Enemy = new PlayerModel();
            Enemy.Race = "";
            Enemy.Class = "";
            Enemy.Barbarian = false;
            Enemy.Level = 100;
            Enemy.HP = 200;
            Enemy.MP = 500;

            Enemy.BaseStrength = 16;
            Enemy.BaseAgility = 4;
            Enemy.BaseInteligence = 4;
            Enemy.BaseSpeed = 4;
            Enemy.BaseWillPower = 4;
            Enemy.BaseToughness = 4;

            Enemy.CombatSkill = 0.01;
            Enemy.EvasionSkill = 0.01;

            Enemy.Vampirism = 0.01;
            Enemy.WeaponSpeed = 0.01;
            Enemy.WeaponDMG = 100;
            Enemy.Robbery = 0.01;
            Enemy.EQdefence = 0.01;
            Enemy.EvasionLegs = 10;
            Enemy.EvasionArmor = 25;

            //Enemy.Modificator = 1;
            Enemy.StrengthBless = 0;
            Enemy.ToughnessBless = 0;
            Enemy.AgilityBless = 0;
            Enemy.SpeedBless = 0;
            Enemy.InteligenceBless = 0;
            Enemy.WillPowerBless = 0;
            Enemy.TrafienieBless = 0;
            Enemy.UnikiBless = 0;

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