using AmoSim2.ViewModel;
using System.Collections.Generic;

namespace AmoSim2.Player
{
    public class Talisman : ViewModelBase
    {
        public List<Talisman> Talismans { get; set; } = new List<Talisman>();

        public Talisman()
        {
            Talismans.Add(new Talisman() { AttrType = "Strength", Modificator = 1.5 });
        }

        public string AttrType { get; set; }

        public double Modificator { get; set; }
    }
}