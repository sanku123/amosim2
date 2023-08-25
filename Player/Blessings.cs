using AmoSim2.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AmoSim2.Player
{
    public partial class Model : ViewModelBase
    {

        [JsonIgnore]
        public double Modificator
        {
            get
            {
                switch (Race)
                {
                    case "Wampir" when VampireBlessActive:
                        return 1.5;
                    case "Człowiek" when Class == "Paladyn":
                        return 2.5;
                    default:
                        return 1;
                }
            }
        }

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

        private bool? _vampireBlessActive;
        [JsonIgnore]
        public bool VampireBlessActive
        {
            get { return (_vampireBlessActive ?? false); }
            set
            {
                _vampireBlessActive = value;
                OnPropertyChanged();
            }
        }

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

        public List<string> Blessings => new List<String> { "", "Siła", "Wytrzymałość", "Zręczność", "Szybkość", "Inteligencja", "Siła Woli", "Trafienie", "Uniki" };

        public void ResetBlessings()
        {
            StrengthBlessActive = false;
            ToughnessBlessActive = false;
            AgilityBlessActive = false;
            SpeedBlessActive = false;
            InteligenceBlessActive = false;
            WillPowerBlessActive = false;
            TrafienieBlessActive = false;
            UnikiBlessActive = false;
        }

        private string _selectedBlessing;

        public string SelectedBlessing
        {
            get => _selectedBlessing;
            set
            {
                ResetBlessings();

                if (value != _selectedBlessing)
                {
                    _selectedBlessing = value;
                    OnPropertyChanged();
                    SelectedBlessingChanged(_selectedBlessing);
                }
            }
        }

        private void SelectedBlessingChanged(string selectedBlessing)
        {
            ResetBlessings();

            switch (selectedBlessing)
            {
                case "Siła":
                    StrengthBlessActive = true;
                    break;

                case "Wytrzymałość":
                    ToughnessBlessActive = true;
                    break;

                case "Zręczność":
                    AgilityBlessActive = true;
                    break;

                case "Szybkość":
                    SpeedBlessActive = true;
                    break;

                case "Inteligencja":
                    InteligenceBlessActive = true;
                    break;

                case "Siła Woli":
                    WillPowerBlessActive = true;
                    break;

                case "Trafienie":
                    TrafienieBlessActive = true;
                    break;

                case "Uniki":
                    UnikiBlessActive = true;
                    break;

                case "":
                    ResetBlessings();
                    break;

                default:
                    ResetBlessings();
                    break;
            }
        }
    }
}