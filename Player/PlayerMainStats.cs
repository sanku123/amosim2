namespace AmoSim2.Player
{
    public partial class PlayerModel
    {
        public double BaseAgility { get; set; }

        public double BaseStrength { get; set; }

        public double BaseSpeed { get; set; }

        public double BaseToughness { get; set; }

        public double BaseInteligence { get; set; }

        public double BaseWillPower { get; set; }

        public double Strength => BaseStrength * StrengthTalizman + StrengthBless;

        public double Toughness => BaseToughness * ToughnessTalizman + ToughnessBless;

        public double Agility => BaseAgility * AgilityTalizman + AgilityBless;

        public double Speed => BaseSpeed * SpeedTalizman + SpeedBless;

        public double Inteligence => BaseInteligence + InteligenceBless + KosturDMG;

        public double WillPower => BaseWillPower + +WillPowerBless + KosturDEF;
    }
}