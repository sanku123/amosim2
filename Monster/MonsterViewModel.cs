using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmoSim2.Player;
using AmoSim2.ViewModel;
using CommonServiceLocator;
using Prism.Mvvm;

namespace AmoSim2.Monster
{
    public class MonsterViewModel : ViewModelBase
    {
        public MonsterModel Monster { get; set; }

        public MonsterViewModel()
        {

        }
        
        public void ResetHealth(MonsterModel Monster)
        {
            Monster.Health = Monster.MaxHealth;
        }
    }
}
