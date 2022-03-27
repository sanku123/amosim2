using AmoSim2.Monster;
using AmoSim2.Player;
using AmoSim2.ViewModel;
using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmoSim2.Battle
{
    public class BattleModel : ViewModelBase
    {
        public PlayerViewModel _player => ServiceLocator.Current.GetInstance<PlayerViewModel>();
        public BattleViewModel _battle => ServiceLocator.Current.GetInstance<BattleViewModel>();
        public MonsterViewModel _monster => ServiceLocator.Current.GetInstance<MonsterViewModel>();

        private int _iterations;
        public int Iterations
        {
            get { return _iterations; }
            set
            {
                _iterations = value;

                OnPropertyChanged();
            }
        }
        public readonly Random random = new Random();
        public BattleModel Battle { get; set; }

        public BattleModel(MonsterModel Monster)
        {
            double iteracje = _battle.Battle.Iterations;
            int CombatSkill = 0;
            double EvasionSkill = 0;
            double BattleWin = 0;
            double BattleLost = 0;
            double playerhitchance = Monster.PlayerHitChance;
            double monsterhitchance = Monster.MonsterHitChance;


            for (int i = 0; i < iteracje; i++)
            {
                for (int x = 0; x < 24; x++)
                {
                    for (int a = 0; a < 5 && (Monster.Health > 0) && (_player.Player.HP > 0); a++)
                    {
                        if (monsterhitchance < random.Next(1, 101))
                        {
                            EvasionSkill += 1;
                        }
                    }
                    for (int p = 0; p < 1 && (Monster.Health > 0); p++)
                    {
                        if (playerhitchance >= random.Next(1, 101))
                        {
                            double damage = Math.Round((_player.Player.Attack + random.Next(1, (int)(5 * _player.Player.Level + 1))) * _player.Player.Critical() - Monster.Defence);
                            if (damage > 0) CombatSkill += 1;
                            Monster.Health -= Math.Max(0, damage);
                        }
                    }
                    if (Monster.Health < 1)
                    {
                        BattleWin += 1;
                        break;
                    }
                }
                _monster.ResetHealth(Monster);
            }
            _player.Player.AveragePD = AveragePD(Monster, BattleWin, iteracje);
            _player.Player.AverageSkill = AverageSkill(CombatSkill, iteracje);
            _player.Player.AverageEvasion = AverageEvasion(EvasionSkill, iteracje);
            _player.Player.Win = Wygrane(BattleWin, BattleLost, iteracje);
            _player.Player.Mob = Monster.Name;
        }

        public BattleModel()
        {
        }

        public double AveragePD(MonsterModel monster, double battleWin, double iteracje)
        {
            return Math.Min(2, monster.Level / _player.Player.Level) * monster.Experience * battleWin / iteracje;
        }
        public int EnergiaLevel(MonsterModel monster)
        {
            return (int)(_player.Player.ExpGranica / AverageExperience(monster) * _player.Player.Win);
        }
        public double Wygrane(double battleWin, double battleLost, double iteracje)
        {
            return Math.Round((battleWin - battleLost) / iteracje * 100, 2);
        }
        public double AverageSkill(double combatSkill, double iteracje)
        {
            return Math.Round(combatSkill / 100 / iteracje, 4);
        }
        public double AverageEvasion(double evasionSkill, double iteracje)
        {
            return Math.Round(CalculateEvasion(evasionSkill) / iteracje, 4);
        }
        public double AverageExperience(MonsterModel monster)
        {
            return Math.Round(Math.Min(2, monster.Level / _player.Player.Level) * monster.Experience * 1.05 * 1.1);
        }
        public double CalculateEvasion(double EvasionSkill)
        {
            double evasion = _player.Player.EvasionSkill;

            if (evasion <= 10) return EvasionSkill /= 250;
            else if (evasion <= 20) return EvasionSkill /= 260;
            else if (evasion <= 50) return EvasionSkill /= 270;
            else if (evasion <= 100) return EvasionSkill /= 280;
            else if (evasion <= 200) return EvasionSkill /= 290;
            else if (evasion <= 500) return EvasionSkill /= 300;
            else if (evasion <= 1000) return EvasionSkill /= 320;
            else if (evasion <= 2000) return EvasionSkill /= 340;
            else if (evasion <= 5000) return EvasionSkill /= 360;
            else if (evasion <= 10000) return EvasionSkill /= 380;
            else if (evasion <= 20000) return EvasionSkill /= 400;
            else if (evasion <= 40000) return EvasionSkill /= 450;
            else if (evasion <= 50000) return EvasionSkill /= 500;
            else if (evasion <= 60000) return EvasionSkill /= 530;
            else return EvasionSkill /= 560;
        }
    }
}
