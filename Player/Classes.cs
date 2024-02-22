using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmoSim2.Player
{
    public partial class Model
    {
        public List<string> Klasy => new List<String> { "", "Wojownik", "Paladyn", "Barbarzyńca", "Mag", "Złodziej", "Łowca", "Czarnoksiężnik" };

        private string _class;

        public string Class
        {
            get => _class;
            set
            {
                _class = value;
                switch (_class)
                {
                    case "Mag":
                    case "Czarnoksiężnik":
                        MageControlsVisibility = true;
                        MeleeControlsVisibility = false;
                        break;
                    case "":
                        MageControlsVisibility = false;
                        MeleeControlsVisibility = false;
                        break;
                    default:
                        MageControlsVisibility = false;
                        MeleeControlsVisibility = true;
                        ResetKostury();
                        ResetSpells1();
                        ResetSpells2();
                        ResetSzata();
                        break;
                }
            }
        }
    }
}