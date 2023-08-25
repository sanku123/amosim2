using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmoSim2.Player
{
    public partial class Model
    {
        public string Race { get; set; }

        public List<string> Rasy => new List<String> { "", "Człowiek", "Krasnolud", "Elf", "Jaszczuroczłek", "Hobbit", "Gnom", "Wampir", "Wilkołak" };
    }
}