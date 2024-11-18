using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AmoSim2.Player
{
    public partial class Model
    {
        [JsonIgnore]
        public List<string> FirstSpellSlot => new List<String>
        {
            "",
            "1.5 x Siła Woli",
            "1.1 x Siła Woli 1.4 x Szybkość"
        };
        [JsonIgnore]
        public List<string> SecondSpellSlot => new List<String>
        {
            "",
            "1.5 x Inteligencja",
            "1.4 x Inteligencja 1.1 x Szybkość"
        };

        [JsonIgnore]
        public double SpellDamage { get; set; }

        [JsonIgnore]
        public double SpellDefence { get; set; }

        [JsonIgnore]
        public double SpellSpeed_1_1 { get; set; }

        [JsonIgnore]
        public double SpellSpeed_1_4 { get; set; }


        private string _selectedFirstSpellSlot;
        public string SelectedFirstSpellSlot
        {
            get => _selectedFirstSpellSlot;
            set
            {
                ResetFirstSpellSlot();

                if (value != _selectedFirstSpellSlot)
                {
                    _selectedFirstSpellSlot = value;
                    OnPropertyChanged();
                    SelectedSpellFirstSlotChanged(_selectedFirstSpellSlot);
                }
            }
        }

        private string _selectedSecondSpellSlot;
        public string SelectedSecondSpellSlot
        {
            get => _selectedSecondSpellSlot;
            set
            {
                ResetSecondSpellSlot();

                if (value != _selectedSecondSpellSlot)
                {
                    _selectedSecondSpellSlot = value;
                    OnPropertyChanged();
                    SelectedSecondSpellSlotChanged(_selectedSecondSpellSlot);
                }
            }
        }

        public void ResetFirstSpellSlot()
        {
            SpellDefence = 1;
            SpellSpeed_1_4 = 0;
        }

        public void ResetSecondSpellSlot()
        {
            SpellDamage = 1;
            SpellSpeed_1_1 = 0;
        }

        private void SelectedSpellFirstSlotChanged(string selectedFirstSpellSlot)
        {
            ResetFirstSpellSlot();

            if (selectedFirstSpellSlot == "1.5 x Siła Woli")
            {
                SpellDefence = 1.5;
            }
            else if (selectedFirstSpellSlot == "1.1 x Siła Woli 1.4 x Szybkość")
            {
                SpellDefence = 1.1;
                SpellSpeed_1_4 = 0.4;
            }
            else if (selectedFirstSpellSlot == "")
            {
                ResetFirstSpellSlot();
            }
        }

        private void SelectedSecondSpellSlotChanged(string selectedSecondSpellSlot)
        {
            ResetSecondSpellSlot();

            if (selectedSecondSpellSlot == "1.5 x Inteligencja")
            {
                SpellDamage = 1.5;
            }
            else if (selectedSecondSpellSlot == "1.4 x Inteligencja 1.1 x Szybkość")
            {
                SpellDamage = 1.4;
                SpellSpeed_1_1 = 0.1;
            }
            else if (selectedSecondSpellSlot == "")
            {
                ResetSecondSpellSlot();
            }
        }       
    }
}