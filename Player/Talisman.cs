using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

        public List<string> Talismans => new List<String> { "", "Siła + Wytka", "Siła + Szybkość", "Zręka + Wytka", "Zręka + Szybkość", "Wytka + Szybkość" };

        public void ResetTalisman()
        {
            StrengthTalizman = 1;
            ToughnessTalizman = 1;
            AgilityTalizman = 1;
            SpeedTalizman = 1;
        }

        private void SelectedTalismanChanged(string selectedTalisman)
        {
            ResetTalisman();
            if (selectedTalisman == "Siła + Wytka")
            {
                StrengthTalizman = 1.5;
                ToughnessTalizman = 1.5;
            }
            else if (selectedTalisman == "Siła + Szybkość")
            {
                StrengthTalizman = 1.5;
                SpeedTalizman = 1.5;
            }
            else if (selectedTalisman == "Zręka + Wytka")
            {
                AgilityTalizman = 1.5;
                ToughnessTalizman = 1.5;
            }
            else if (selectedTalisman == "Zręka + Szybkość")
            {
                AgilityTalizman = 1.5;
                SpeedTalizman = 1.5;
            }
            else if (selectedTalisman == "Wytka + Szybkość")
            {
                ToughnessTalizman = 1.5;
                SpeedTalizman = 1.5;
            }
            else if (selectedTalisman == "")
            {
                ResetTalisman();
            }
        }

        private string _selectedTalisman;

        public string SelectedTalisman
        {
            get => _selectedTalisman;
            set
            {
                ResetTalisman();

                if (value != _selectedTalisman)
                {
                    _selectedTalisman = value;
                    OnPropertyChanged();
                    SelectedTalismanChanged(_selectedTalisman);
                }
            }
        }
    }
}