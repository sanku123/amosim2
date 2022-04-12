using Newtonsoft.Json;

namespace AmoSim2.Player
{
    public partial class Model
    {
        [JsonIgnore]
        public double StrengthTalizman { get; set; }

        [JsonIgnore]
        public double ToughnessTalizman { get; set; }

        [JsonIgnore]
        public double AgilityTalizman { get; set; }

        [JsonIgnore]
        public double SpeedTalizman { get; set; }
    }
}