using AmoSim2.ViewModel;
using Newtonsoft.Json;

namespace AmoSim2.Player
{
    public partial class Model : ViewModelBase
    {
        [JsonIgnore]
        public double Modificator => Class == "Paladyn" ? 2.5 : 1;

        [JsonIgnore]
        public double StrengthBless => StrengthBlessActive ? 6 * Level * Modificator : 0;

        [JsonIgnore]
        public double ToughnessBless => ToughnessBlessActive ? 6 * Level * Modificator : 0;

        [JsonIgnore]
        public double AgilityBless => AgilityBlessActive ? 6 * Level * Modificator : 0;

        [JsonIgnore]
        public double SpeedBless => SpeedBlessActive ? 6 * Level * Modificator : 0;

        [JsonIgnore]
        public double InteligenceBless => InteligenceBlessActive ? 6 * Level * Modificator : 0;

        [JsonIgnore]
        public double WillPowerBless => WillPowerBlessActive ? 6 * Level * Modificator : 0;

        [JsonIgnore]
        public double TrafienieBless => TrafienieBlessActive ? 6 * Level * Modificator / 1.4 : 0;

        [JsonIgnore]
        public double UnikiBless => UnikiBlessActive ? 6 * Level * Modificator / 1.4 : 0;

        [JsonIgnore]
        public bool StrengthBlessActive { get; set; }

        [JsonIgnore]
        public bool ToughnessBlessActive { get; set; }

        [JsonIgnore]
        public bool AgilityBlessActive { get; set; }

        [JsonIgnore]
        public bool SpeedBlessActive { get; set; }

        [JsonIgnore]
        public bool InteligenceBlessActive { get; set; }

        [JsonIgnore]
        public bool WillPowerBlessActive { get; set; }

        [JsonIgnore]
        public bool TrafienieBlessActive { get; set; }

        [JsonIgnore]
        public bool UnikiBlessActive { get; set; }
    }
}