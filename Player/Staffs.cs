using Newtonsoft.Json;
using System.Collections.Generic;

namespace AmoSim2.Player
{
    public partial class Model
    {
        [JsonIgnore]
        public List<string> Staffs => new List<string>
        {
            "",
            "Laska",
            "Kostur",
            "Kantynówka"
        };

        [JsonIgnore] public double StaffDamage { get; set; }
        [JsonIgnore] public double StaffDefence { get; set; }
        [JsonIgnore] public double StaffSpeed { get; set; }

        private string _selectedStaff;
        public string SelectedStaff
        {
            get => _selectedStaff;
            set
            {
                ResetStaffs();

                if (value != _selectedStaff)
                {
                    _selectedStaff = value;
                    OnPropertyChanged();
                    SelectedStaffChanged(_selectedStaff);
                }
            }
        }
        private void SelectedStaffChanged(string selectedKostur)
        {
            switch (selectedKostur)
            {
                case "Laska":
                    StaffDefence = 10000;
                    break;

                case "Kostur":
                    StaffDamage = 5000;
                    StaffDefence = 5000;
                    break;

                case "Kantynówka":
                    StaffSpeed = 5000;
                    StaffDefence = 5000;
                    break;

                case "":
                    ResetStaffs();
                    break;
            }
        }
        public void ResetStaffs()
        {
            StaffDamage = 0;
            StaffDefence = 0;
            StaffSpeed = 0;
        }
    }
}