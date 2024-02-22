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
        public List<string> Robes => new List<String> { "", "Krasnoludzka", "Hobbicka" };

        [JsonIgnore]
        public double HobbickaSzata
        {
            get
            {
                switch (SelectedRobe)
                {
                    case "Hobbicka" when Race == "Hobbit":
                        return 3000;
                    case "Hobbicka":
                        return 2000;
                    default:
                        return 0;
                }
            }
        }

        public double KrasnoludzkaSzata
        {
            get
            {
                switch (SelectedRobe)
                {
                    case "Krasnoludzka" when Race == "Krasnolud":
                        return 4000;
                    case "Krasnoludzka":
                        return 2000;
                    default:
                        return 0;
                } 
            }
        }

        private string _selectedRobe;

        public string SelectedRobe
        {
            get => _selectedRobe;
            set
            {
                if (value != _selectedRobe)
                {
                    _selectedRobe = value;
                    OnPropertyChanged();
                }
            }
        }    
    }
}