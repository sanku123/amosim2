﻿using Newtonsoft.Json;
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
        public List<string> Races => new List<String> 
        {   "", 
            "Człowiek", 
            "Krasnolud", 
            "Elf", 
            "Jaszczuroczłek", 
            "Hobbit", 
            "Gnom", 
            "Wampir", 
            "Wilkołak" 
        };

        public string Race { get; set; }

        private bool? _racialEnemyActive;
        [JsonIgnore] public bool RacialEnemyActive { get => (_racialEnemyActive ?? false); set { _racialEnemyActive = value; OnPropertyChanged(); } }
    }
}