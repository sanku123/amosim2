using Newtonsoft.Json;

namespace AmoSim2.Player
{
    public partial class Model
    {
        [JsonIgnore]
        public double CzarDMG { get; set; }

        [JsonIgnore]
        public double CzarDEF { get; set; }

        [JsonIgnore]
        public double CzarSpeed { get; set; }

        [JsonIgnore]
        public double CzarSpeed2 { get; set; }
    }
}