using Newtonsoft.Json;

namespace AmoSim2.Player
{
    public partial class Model
    {
        [JsonIgnore]
        public double KosturDMG { get; set; }

        [JsonIgnore]
        public double KosturDEF { get; set; }

        [JsonIgnore]
        public double KosturSpeed { get; set; }
    }
}