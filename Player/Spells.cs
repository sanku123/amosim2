using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AmoSim2.Player
{
    public partial class Model
    {
        [JsonIgnore]
        public double CzarDMG { get; set; }

        [JsonIgnore]
        public double CzarDEF { get; set; }

        [JsonIgnore]
        public double CzarSpeed_1_1 { get; set; }

        [JsonIgnore]
        public double CzarSpeed_1_4 { get; set; }

        public List<string> Czary1 => new List<String> { "", "1.5 x Willpower", "1.1 x Willpower 1.4 x Szybkość" };

        public List<string> Czary2 => new List<String> { "", "1.5 x Inteligencja", "1.4 x Inteligencja 1.1 x Szybkość" };

        public void ResetSpells1()
        {
            CzarDEF = 1;
            CzarSpeed_1_4 = 0;
        }

        public void ResetSpells2()
        {
            CzarDMG = 1;
            CzarSpeed_1_1 = 0;
        }

        private void SelectedCzar1Changed(string selectedCzar1)
        {
            ResetSpells1();

            if (selectedCzar1 == "1.5 x Willpower")
            {
                CzarDEF = 1.5;
            }
            else if (selectedCzar1 == "1.1 x Willpower 1.4 x Szybkość")
            {
                CzarDEF = 1.1;
                CzarSpeed_1_4 = 0.4;
            }
            else if (selectedCzar1 == "")
            {
                ResetSpells1();
            }
        }

        private void SelectedCzar2Changed(string selectedCzar2)
        {
            ResetSpells2();

            if (selectedCzar2 == "1.5 x Inteligencja")
            {
                CzarDMG = 1.5;
            }
            else if (selectedCzar2 == "1.4 x Inteligencja 1.1 x Szybkość")
            {
                CzarDMG = 1.4;
                CzarSpeed_1_1 = 0.1;
            }
            else if (selectedCzar2 == "")
            {
                ResetSpells2();
            }
        }

        private string _selectedCzar1;

        public string SelectedCzar1
        {
            get => _selectedCzar1;
            set
            {
                ResetSpells1();

                if (value != _selectedCzar1)
                {
                    _selectedCzar1 = value;
                    OnPropertyChanged();
                    SelectedCzar1Changed(_selectedCzar1);
                }
            }
        }

        private string _selectedCzar2;

        public string SelectedCzar2
        {
            get => _selectedCzar2;
            set
            {
                ResetSpells2();

                if (value != _selectedCzar2)
                {
                    _selectedCzar2 = value;
                    OnPropertyChanged();
                    SelectedCzar2Changed(_selectedCzar2);
                }
            }
        }
    }
}