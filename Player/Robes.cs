using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmoSim2.Player
{
    public partial class Model
    {
        [JsonIgnore]
        public double HobbickaSzata
        {
            get
            {
                if (SelectedRobe == "Hobbicka" && Race == "Hobbit")
                {
                    return 3000;
                }
                else if (SelectedRobe == "Hobbicka")
                {
                    return 2000;
                }
                return 0;
            }
        }

        public double KrasnoludzkaSzata
        {
            get
            {
                if (SelectedRobe == "Krasnoludzka" && Race == "Krasnolud")
                {
                    return 4000;
                }
                else if (SelectedRobe == "Krasnoludzka")
                {
                    return 2000;
                }
                return 0;
            }
        }

        public void ResetSzata()
        {
            //KrasnoludzkaSzata = 0;
            //HobbickaSzata = 0;
        }

        private string _selectedRobe;

        public string SelectedRobe
        {
            get => _selectedRobe;
            set
            {
                ResetKostury();

                if (value != _selectedRobe)
                {
                    _selectedRobe = value;
                    OnPropertyChanged();
                    SelectedRobeChanged(_selectedRobe);
                }
            }
        }

        private void SelectedRobeChanged(string selectedRobe)
        {
            ResetSzata();

            if (selectedRobe == "Krasnoludzka")
            {
                //KrasnoludzkaSzata = 2000;
            }
            else if (selectedRobe == "Hobbicka" && Race == "Hobbit")
            {
                //HobbickaSzata = 3000;
            }
            else if (selectedRobe == "Hobbicka" && Race != "Hobbit")
            {
                //HobbickaSzata = 2000;
            }
            else if (selectedRobe == "")
            {
                ResetSzata();
            }
        }

        public List<string> Robes => new List<String> { "", "Krasnoludzka", "Hobbicka" };
    }
}