using Newtonsoft.Json;
using System.Collections.Generic;

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

        public void ResetKostury()
        {
            KosturDMG = 0;
            KosturDEF = 0;
            KosturSpeed = 0;
        }

        public List<string> Kostury => new List<string> { "", "Laska", "Kostur", "Kantynówka" };

        private void SelectedKosturChanged(string selectedKostur)
        {
            switch (selectedKostur)
            {
                case "Laska":
                    KosturDEF = 10000;
                    break;

                case "Kostur":
                    KosturDMG = 5000;
                    KosturDEF = 5000;
                    break;

                case "Kantynówka":
                    KosturSpeed = 5000;
                    KosturDEF = 5000;
                    break;

                case "":
                    ResetKostury();
                    break;
            }
        }

        private string _selectedKostur;

        public string SelectedKostur
        {
            get => _selectedKostur;
            set
            {
                ResetKostury();

                if (value != _selectedKostur)
                {
                    _selectedKostur = value;
                    OnPropertyChanged();
                    SelectedKosturChanged(_selectedKostur);
                }
            }
        }
    }
}