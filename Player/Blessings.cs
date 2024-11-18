using AmoSim2.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AmoSim2.Player
{
    public partial class Model : ViewModelBase
    {
        [JsonIgnore]
        public List<string> Blessings { get; } = new List<string>
        {
            "",
            "Siła",
            "Wytrzymałość",
            "Zręczność",
            "Szybkość",
            "Inteligencja",
            "Siła Woli",
            "Trafienie",
            "Uniki"
        };
        
        [JsonIgnore] public bool StrengthBlessActive { get; set; }
        [JsonIgnore] public bool ToughnessBlessActive { get; set; }
        [JsonIgnore] public bool AgilityBlessActive { get; set; }
        [JsonIgnore] public bool SpeedBlessActive { get; set; }
        [JsonIgnore] public bool InteligenceBlessActive { get; set; }
        [JsonIgnore] public bool WillPowerBlessActive { get; set; }
        [JsonIgnore] public bool HitAbilityBlessActive { get; set; }
        [JsonIgnore] public bool EvasionBlessActive { get; set; }

        [JsonIgnore] public double StrengthBless => CalculateBless(StrengthBlessActive);
        [JsonIgnore] public double ToughnessBless => CalculateBless(ToughnessBlessActive);
        [JsonIgnore] public double AgilityBless => CalculateBless(AgilityBlessActive);
        [JsonIgnore] public double SpeedBless => CalculateBless(SpeedBlessActive);
        [JsonIgnore] public double InteligenceBless => CalculateBless(InteligenceBlessActive);
        [JsonIgnore] public double WillPowerBless => CalculateBless(WillPowerBlessActive);
        [JsonIgnore] public double HitAbilityBless => CalculateSpecialBless(HitAbilityBlessActive);
        [JsonIgnore] public double EvasionBless => CalculateSpecialBless(EvasionBlessActive);


        private const double BlessMultiplier = 6;

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
                case "Siła": StrengthBlessActive = true; break;
                case "Wytrzymałość": ToughnessBlessActive = true; break;
                case "Zręczność": AgilityBlessActive = true; break;
                case "Szybkość": SpeedBlessActive = true; break;
                case "Inteligencja": InteligenceBlessActive = true; break;
                case "Siła Woli": WillPowerBlessActive = true; break;
                case "Trafienie": HitAbilityBlessActive = true; break;
                case "Uniki": EvasionBlessActive = true; break;
            }
        }

        [JsonIgnore]
        public double Modificator
        {
            get
            {
                return Race == "Wampir" && VampireBlessActive ? 1.5 :
                       Race == "Człowiek" && Class == "Paladyn" ? 2.5 :
                       1;
            }
        }

        private bool? _vampireBlessActive;
        [JsonIgnore] public bool VampireBlessActive { get => (_vampireBlessActive ?? false); set { _vampireBlessActive = value; OnPropertyChanged(); } }

        private double CalculateBless(bool isActive) => isActive ? BlessMultiplier * Level * Modificator : 0;
        private double CalculateSpecialBless(bool isActive) => isActive ? BlessMultiplier * Level * Modificator / 1.4 : 0;
        public void ResetBlessings()
        {
            StrengthBlessActive =
            ToughnessBlessActive =
            AgilityBlessActive =
            SpeedBlessActive =
            InteligenceBlessActive =
            WillPowerBlessActive =
            HitAbilityBlessActive =
            EvasionBlessActive = false;
        }


        

        


        

        

        
    }

}