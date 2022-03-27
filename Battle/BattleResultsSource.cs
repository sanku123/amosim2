using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmoSim2.Battle
{
    public static class BattleResultsSource
    {
        public static ObservableCollection<BattleResults> Results = new ObservableCollection<BattleResults>();
    }
}
