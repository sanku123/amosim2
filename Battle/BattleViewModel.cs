using AmoSim2.Monster;
using AmoSim2.Player;
using AmoSim2.ViewModel;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AmoSim2.Battle
{
    public class BattleViewModel : ViewModelBase
    {
        public PlayerViewModel Main => ServiceLocator.Current.GetInstance<PlayerViewModel>();

        public ObservableCollection<BattleResults> Results { get; set; } = new ObservableCollection<BattleResults>();
        public ICommand StartBattleCommand { get; private set; }

        public Random random;
        public MonsterViewModel Monster { get; set; }
        public PlayerModel Player { get; set; }
        public PlayerViewModel PlayerVM { get; set; }
        public BattleModel Battle { get; set; }

        public BattleViewModel()
        {
            MonsterModel monster = new MonsterModel("Bhaal", 400, 22000, 20000, 21000, 21000, 20000, 8450);

            StartBattleCommand = new DelegateCommand(this.OnStartBattleClick);

            Battle = new BattleModel();
            Battle.Iterations = 5000;
        }

  
        private void OnStartBattleClick()
        {
            var test = SimpleIoc.Default.GetInstance<PlayerViewModel>();
            Debug.WriteLine(test.Player.Class);
            Debug.WriteLine(test.Player.Level);


            MonsterModel monster = new MonsterModel("Bhaal", 400, 22000, 20000, 21000, 21000, 20000, 8450);
            double dmg = Main.Player.WeaponDMG;

            if (BattleResultsSource.Results.Count != 0)
            {
                BattleResultsSource.Results.Clear();
            }

            for (int i = 0; i < 20; i++)
            {
                _ = new BattleModel(monster);

                BattleResultsSource.Results.Add(new BattleResults()
                {
                    Attack = Main.Player.Attack,
                    AverageSkill = Main.Player.AverageSkill,
                    AverageEvasion = Main.Player.AverageEvasion,
                    Win = Main.Player.Win,
                    Energy = Main.Player.EnergiaLevel,
                    Mob = monster.Name,
                    UBlevel = Main.Player.UBlevel,
                    UnikiLevel = Main.Player.UnikiLevel,
                    SumaUBuniki = Main.Player.SumaUBuniki,
                    SumaUBunikiLevel = Main.Player.SumaUBunikiLevel,
                }); ;
                Main.Player.WeaponDMG -= 15;
            }
            Main.Player.WeaponDMG = dmg;
        }
        

    }
}
