using AmoSim2.ViewModel;
using System.ComponentModel;

namespace AmoSim2.Player
{
    public partial class PlayerModel
    {
        public double Modificator
        {
            get
            {
                if (Class == "Paladyn")
                {
                    return 2.5;
                }
                return 1;
            }
        }

        public double StrengthBless { get; set; }

        public double ToughnessBless { get; set; }

        public double AgilityBless { get; set; }

        public double SpeedBless { get; set; }

        public double InteligenceBless { get; set; }

        public double WillPowerBless { get; set; }

        public double TrafienieBless { get; set; }

        public double UnikiBless { get; set; }
    }
}