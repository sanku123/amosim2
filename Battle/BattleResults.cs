using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmoSim2.Battle
{
    public class BattleResults
    {
        public double Attack { get; set; }
        public double AverageSkill { get; set; }
        public double AverageEvasion { get; set; }
        public double AveragePD { get; set; }

        public double Win { get; set; }
        public double Lost { get; set; }
        public string Mob { get; set; }
        public double SumaUBuniki { get; set; }
        public double SumaUBunikiLevel { get; set; }

        public double Energy { get; set; }
        public double UBlevel { get; set; }
        public double UnikiLevel { get; set; }
    }
}
