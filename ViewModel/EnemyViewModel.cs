using AmoSim2.Others;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace AmoSim2.Player
{
    public class EnemyViewModel : CommandViewModel
    {
        public static EnemyViewModel Instance { get; } = new EnemyViewModel();

        public Model Enemy { get; set; }

        public EnemyViewModel()
        {
            Enemy = new Model
            {
                Nickname = "Przeciwnik",
                Race = "",
                Class = "",
                Level = 1,
                HP = 100,
                MP = 100,

                BaseStrength = 4,
                BaseAgility = 4,
                BaseInteligence = 4,
                BaseSpeed = 4,
                BaseWillPower = 4,
                BaseToughness = 4,

                CombatSkill = 0,
                EvasionSkill = 0,

                Vampirism = 0,
                WeaponSpeed = 0,
                WeaponDMG = 0,
                Robbery = 0,
                EQdefence = 0,
                EQevasion = 0,

                KosturDEF = 0,
                KosturDMG = 0,
                KosturSpeed = 0,

                CzarDMG = 1,
                CzarDEF = 1,
                CzarSpeed_1_1 = 0,
                CzarSpeed_1_4 = 0,

                StrengthBlessActive = false,
                ToughnessBlessActive = false,
                AgilityBlessActive = false,
                SpeedBlessActive = false,
                InteligenceBlessActive = false,
                WillPowerBlessActive = false,
                TrafienieBlessActive = false,
                UnikiBlessActive = false,
                MageControlsVisibility = false,
                MeleeControlsVisibility = false
            };
        }
    }
}