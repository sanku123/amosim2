using AmoSim2.ViewModel;
using Newtonsoft.Json;

namespace AmoSim2.Player
{
    public partial class Model : ViewModelBase
    {
        public double BaseAgility { get; set; }

        public double BaseStrength { get; set; }

        public double BaseSpeed { get; set; }

        public double BaseToughness { get; set; }

        public double BaseInteligence { get; set; }

        public double BaseWillPower { get; set; }

        [JsonIgnore]
        public double Strength => BaseStrength * StrengthTalizman + StrengthBless;

        [JsonIgnore]
        public double Toughness => BaseToughness * ToughnessTalizman + ToughnessBless;

        [JsonIgnore]
        public double Agility => BaseAgility * AgilityTalizman + AgilityBless;

        [JsonIgnore]
        public double Speed => BaseSpeed * SpeedTalizman + SpeedBless + KosturSpeed;

        [JsonIgnore]
        public double Inteligence => BaseInteligence + InteligenceBless + KosturDMG;

        [JsonIgnore]
        public double WillPower => BaseWillPower + +WillPowerBless + KosturDEF;
    }
}